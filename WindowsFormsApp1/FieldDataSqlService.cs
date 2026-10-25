using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class FieldDataSqlService
    {
        public static bool SaveFieldData(
         int siteVisitID,
         string soilType,
         decimal waterDepth,
         string drillingStatus,
         string remarks)
        {
            string updateFieldDataQuery = @"
            UPDATE FD
            SET
                FD.Soil_Type = @Soil_Type,
                FD.Water_Depth = @Water_Depth,
                FD.DRL_Status = @DRL_Status,
                FD.Remarks = @Remarks
            FROM Field_Datas FD
            INNER JOIN Site_Visits SV
                ON FD.DATA_ID = SV.DATA_ID
            WHERE SV.SVID = @SVID";

            string updateAssignmentQuery = @"
            UPDATE TA
            SET A_Status = @A_Status
            FROM Technician_Assignments TA
            INNER JOIN Site_Visits SV
                ON TA.Assignment_ID = SV.ASSIGNMENT_ID
            WHERE SV.SVID = @SVID";

            string updateRequestQuery = @"
            UPDATE SR
            SET REQ_Status = @REQ_Status
            FROM Service_Requests SR
            INNER JOIN Technician_Assignments TA
                ON SR.REQ_ID = TA.REQ_ID
            INNER JOIN Site_Visits SV
                ON TA.Assignment_ID = SV.ASSIGNMENT_ID
            WHERE SV.SVID = @SVID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlTransaction transaction =
                       connection.BeginTransaction())
                {
                    try
                    {
                        int fieldDataRows;

                        using (SqlCommand command =
                               new SqlCommand(
                                   updateFieldDataQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@Soil_Type",
                                soilType
                            );

                            command.Parameters.AddWithValue(
                                "@Water_Depth",
                                waterDepth
                            );

                            command.Parameters.AddWithValue(
                                "@DRL_Status",
                                drillingStatus
                            );

                            command.Parameters.AddWithValue(
                                "@Remarks",
                                remarks
                            );

                            command.Parameters.AddWithValue(
                                "@SVID",
                                siteVisitID
                            );

                            fieldDataRows =
                                command.ExecuteNonQuery();
                        }

                        if (fieldDataRows == 0)
                        {
                            throw new Exception(
                                "No field data record was found for this site visit."
                            );
                        }

                        using (SqlCommand command =
                               new SqlCommand(
                                   updateAssignmentQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@A_Status",
                                drillingStatus
                            );

                            command.Parameters.AddWithValue(
                                "@SVID",
                                siteVisitID
                            );

                            command.ExecuteNonQuery();
                        }

                        using (SqlCommand command =
                               new SqlCommand(
                                   updateRequestQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@REQ_Status",
                                drillingStatus
                            );

                            command.Parameters.AddWithValue(
                                "@SVID",
                                siteVisitID
                            );

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public static bool CompleteRequest(
            int siteVisitID)
        {
            string query = @"
            UPDATE SR
            SET REQ_Status = 'Completed'
            FROM Service_Requests SR
            INNER JOIN Technician_Assignments TA
                ON SR.REQ_ID = TA.REQ_ID
            INNER JOIN Site_Visits SV
                ON TA.Assignment_ID = SV.ASSIGNMENT_ID
            WHERE SV.SVID = @SVID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@SVID",
                        siteVisitID
                    );

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
