using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class SiteVisitSqlService
    {
        public static int SaveSiteVisit(
        int requestID,
        int assignmentID,
        DateTime visitDate,
        string notes)
        {
            string checkAssignmentQuery = @"
            SELECT COUNT(*)
            FROM Technician_Assignments
            WHERE Assignment_ID = @Assignment_ID
            AND REQ_ID = @REQ_ID";

            string insertFieldDataQuery = @"
            INSERT INTO Field_Datas
            (
                Soil_Type,
                Water_Depth,
                DRL_Status,
                Remarks
            )
            OUTPUT INSERTED.DATA_ID
            VALUES
            (
                NULL,
                NULL,
                'Pending',
                @Remarks
            )";

            string insertSiteVisitQuery = @"
            INSERT INTO Site_Visits
            (
                DATA_ID,
                ASSIGNMENT_ID,
                V_Date
            )
            OUTPUT INSERTED.SVID
            VALUES
            (
                @DATA_ID,
                @ASSIGNMENT_ID,
                @V_Date
            )";

            string updateAssignmentQuery = @"
            UPDATE Technician_Assignments
            SET A_Status = 'Site Visit Completed'
            WHERE Assignment_ID = @Assignment_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlTransaction transaction =
                       connection.BeginTransaction())
                {
                    try
                    {
                        // Check that the assignment belongs to the request
                        using (SqlCommand command =
                               new SqlCommand(
                                   checkAssignmentQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@Assignment_ID",
                                assignmentID
                            );

                            command.Parameters.AddWithValue(
                                "@REQ_ID",
                                requestID
                            );

                            int count =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );

                            if (count == 0)
                            {
                                throw new Exception(
                                    "The technician assignment could not be found for this request."
                                );
                            }
                        }

                        // Store the visit notes in Field_Datas
                        int dataID;

                        using (SqlCommand command =
                               new SqlCommand(
                                   insertFieldDataQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@Remarks",
                                notes
                            );

                            dataID =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );
                        }

                        // Create the Site Visit
                        int siteVisitID;

                        using (SqlCommand command =
                               new SqlCommand(
                                   insertSiteVisitQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@DATA_ID",
                                dataID
                            );

                            command.Parameters.AddWithValue(
                                "@ASSIGNMENT_ID",
                                assignmentID
                            );

                            command.Parameters.AddWithValue(
                                "@V_Date",
                                visitDate.Date
                            );

                            siteVisitID =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );
                        }

                        // Update assignment status
                        using (SqlCommand command =
                               new SqlCommand(
                                   updateAssignmentQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@Assignment_ID",
                                assignmentID
                            );

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        return siteVisitID;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }


        public static string GetVisitIDForAssignment(
        string assignmentID)
        {
            if (string.IsNullOrWhiteSpace(assignmentID))
            {
                return null;
            }

            string numericPart =
                assignmentID
                    .Replace("ASS", "")
                    .Replace("A", "")
                    .Trim();

            if (!int.TryParse(
                    numericPart,
                    out int assignmentNumber))
            {
                return null;
            }

            string query = @"
        SELECT SVID
        FROM Site_Visits
        WHERE ASSIGNMENT_ID = @ASSIGNMENT_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@ASSIGNMENT_ID",
                        assignmentNumber
                    );

                    object result =
                        command.ExecuteScalar();

                    if (result == null)
                    {
                        return null;
                    }

                    return "V" +
                           Convert.ToInt32(result)
                               .ToString("D3");
                }
            }
        }

        public static SiteVisit GetVisitByID(string visitID)
        {
            if (string.IsNullOrWhiteSpace(visitID))
            {
                return null;
            }

            string numericPart = visitID
                .Replace("V", "")
                .Trim();

            if (!int.TryParse(numericPart, out int siteVisitID))
            {
                return null;
            }

            string query = @"
        SELECT
            SV.SVID,
            SV.ASSIGNMENT_ID,
            TA.REQ_ID,
            SV.V_Date
        FROM Site_Visits SV
        INNER JOIN Technician_Assignments TA
            ON SV.ASSIGNMENT_ID = TA.Assignment_ID
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

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        return new SiteVisit
                        {
                            VisitID =
                                "V" +
                                Convert.ToInt32(
                                    reader["SVID"]
                                ).ToString("D3"),

                            AssignmentID =
                                "ASS" +
                                Convert.ToInt32(
                                    reader["ASSIGNMENT_ID"]
                                ).ToString("D3"),

                            RequestID =
                                "REQ" +
                                Convert.ToInt32(
                                    reader["REQ_ID"]
                                ).ToString("D3"),

                            VisitDate =
                                Convert.ToDateTime(
                                    reader["V_Date"]
                                ),

                            Notes = ""
                        };
                    }
                }
            }
        }

        public static List<SiteVisit> GetVisitsByAssignmentID(
        string assignmentID)
        {
            List<SiteVisit> visits =
                new List<SiteVisit>();

            if (string.IsNullOrWhiteSpace(assignmentID))
            {
                return visits;
            }

            string numericPart =
                assignmentID
                    .Replace("ASS", "")
                    .Replace("A", "")
                    .Trim();

            if (!int.TryParse(
                    numericPart,
                    out int assignmentNumber))
            {
                return visits;
            }

            string query = @"
        SELECT
            SV.SVID,
            SV.ASSIGNMENT_ID,
            TA.REQ_ID,
            SV.V_Date,
            FD.Remarks
        FROM Site_Visits SV
        INNER JOIN Technician_Assignments TA
            ON SV.ASSIGNMENT_ID = TA.Assignment_ID
        LEFT JOIN Field_Datas FD
            ON SV.DATA_ID = FD.DATA_ID
        WHERE SV.ASSIGNMENT_ID = @ASSIGNMENT_ID
        ORDER BY SV.V_Date DESC";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@ASSIGNMENT_ID",
                        assignmentNumber
                    );

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            visits.Add(
                                new SiteVisit
                                {
                                    VisitID =
                                        "V" +
                                        Convert.ToInt32(
                                            reader["SVID"]
                                        ).ToString("D3"),

                                    AssignmentID =
                                        "A" +
                                        Convert.ToInt32(
                                            reader["ASSIGNMENT_ID"]
                                        ).ToString("D3"),

                                    RequestID =
                                        "REQ" +
                                        Convert.ToInt32(
                                            reader["REQ_ID"]
                                        ).ToString("D3"),

                                    VisitDate =
                                        Convert.ToDateTime(
                                            reader["V_Date"]
                                        ),

                                    Notes =
                                        reader["Remarks"] == DBNull.Value
                                            ? ""
                                            : reader["Remarks"].ToString()
                                }
                            );
                        }
                    }
                }
            }

            return visits;
        }

        public static List<SiteVisit> GetAllSiteVisits()
        {
            List<SiteVisit> visits = new List<SiteVisit>();

            string query = @"
        SELECT
            SV.SVID,
            SV.ASSIGNMENT_ID,
            TA.REQ_ID,
            SV.V_Date,
            FD.Remarks
        FROM Site_Visits SV
        INNER JOIN Technician_Assignments TA
            ON SV.ASSIGNMENT_ID = TA.Assignment_ID
        LEFT JOIN Field_Datas FD
            ON SV.DATA_ID = FD.DATA_ID
        ORDER BY SV.SVID DESC";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                using (SqlDataReader reader =
                       command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        visits.Add(new SiteVisit
                        {
                            VisitID = "V" +
                                Convert.ToInt32(
                                    reader["SVID"]).ToString("D3"),

                            RequestID = "REQ" +
                                Convert.ToInt32(
                                    reader["REQ_ID"]).ToString("D3"),

                            AssignmentID = "A" +
                                Convert.ToInt32(
                                    reader["ASSIGNMENT_ID"]).ToString("D3"),

                            VisitDate = Convert.ToDateTime(
                                reader["V_Date"]),

                            Notes = reader["Remarks"] == DBNull.Value
                                ? ""
                                : reader["Remarks"].ToString()
                        });
                    }
                }
            }

            return visits;
        }
    }
}
