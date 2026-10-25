using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class SiteAssessmentSqlService
    {
        public static void SaveAssessment(
       int requestID,
       int technicianID,
       DateTime assessmentDate,
       string siteCondition,
       string accessibility,
       string suitability,
       string notes)
        {
            string getAssignmentQuery = @"
            SELECT Assignment_ID
            FROM Technician_Assignments
            WHERE REQ_ID = @REQ_ID
            AND TECH_ID = @TECH_ID";

            string checkAssessmentQuery = @"
            SELECT COUNT(*)
            FROM Site_Visits SV
            INNER JOIN Technician_Assignments TA
                ON SV.ASSIGNMENT_ID = TA.Assignment_ID
            WHERE TA.REQ_ID = @REQ_ID
            AND TA.TECH_ID = @TECH_ID";

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
                @Soil_Type,
                @Water_Depth,
                @DRL_Status,
                @Remarks
            )";

            string insertSiteVisitQuery = @"
            INSERT INTO Site_Visits
            (
                DATA_ID,
                ASSIGNMENT_ID,
                V_Date
            )
            VALUES
            (
                @DATA_ID,
                @ASSIGNMENT_ID,
                @V_Date
            )";

            string updateAssignmentQuery = @"
            UPDATE Technician_Assignments
            SET A_Status = 'Assessment Completed'
            WHERE Assignment_ID = @ASSIGNMENT_ID";

            string updateRequestQuery = @"
            UPDATE Service_Requests
            SET REQ_Status = 'Assessment Completed'
            WHERE REQ_ID = @REQ_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlTransaction transaction =
                       connection.BeginTransaction())
                {
                    try
                    {
                        int assignmentID;

                        // 1. Find the technician assignment
                        using (SqlCommand command =
                               new SqlCommand(
                                   getAssignmentQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@REQ_ID",
                                requestID
                            );

                            command.Parameters.AddWithValue(
                                "@TECH_ID",
                                technicianID
                            );

                            object result =
                                command.ExecuteScalar();

                            if (result == null)
                            {
                                throw new Exception(
                                    "No technician assignment was found for this service request."
                                );
                            }

                            assignmentID =
                                Convert.ToInt32(result);
                        }

                        // 2. Prevent duplicate assessment
                        using (SqlCommand command =
                               new SqlCommand(
                                   checkAssessmentQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@REQ_ID",
                                requestID
                            );

                            command.Parameters.AddWithValue(
                                "@TECH_ID",
                                technicianID
                            );

                            int count =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );

                            if (count > 0)
                            {
                                throw new Exception(
                                    "A site assessment has already been recorded for this request."
                                );
                            }
                        }

                        // 3. Combine the assessment information
                        string remarks =
                            "Site Condition: " +
                            siteCondition +
                            Environment.NewLine +
                            "Accessibility: " +
                            accessibility +
                            Environment.NewLine +
                            "Assessment Notes: " +
                            notes;

                        // 4. Store the assessment data
                        int dataID;

                        using (SqlCommand command =
                               new SqlCommand(
                                   insertFieldDataQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@Soil_Type",
                                DBNull.Value
                            );

                            command.Parameters.AddWithValue(
                                "@Water_Depth",
                                DBNull.Value
                            );

                            command.Parameters.AddWithValue(
                                "@DRL_Status",
                                suitability
                            );

                            command.Parameters.AddWithValue(
                                "@Remarks",
                                remarks
                            );

                            dataID =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );
                        }

                        // 5. Create the Site Visit record
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
                                assessmentDate.Date
                            );

                            command.ExecuteNonQuery();
                        }

                        // 6. Update technician assignment
                        using (SqlCommand command =
                               new SqlCommand(
                                   updateAssignmentQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@ASSIGNMENT_ID",
                                assignmentID
                            );

                            command.ExecuteNonQuery();
                        }

                        // 7. Update service request
                        using (SqlCommand command =
                               new SqlCommand(
                                   updateRequestQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@REQ_ID",
                                requestID
                            );

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

        }

        public static SiteAssessment GetAssessmentByRequestID(
        string requestID)
        {
            if (string.IsNullOrWhiteSpace(requestID))
            {
                return null;
            }

            string numericPart =
                requestID
                    .Replace("REQ", "")
                    .Trim();

            if (!int.TryParse(
                    numericPart,
                    out int requestNumber))
            {
                return null;
            }

            string query = @"
        SELECT TOP 1
            SR.REQ_ID,
            TA.TECH_ID,
            SV.V_Date,
            FD.DRL_Status,
            FD.Remarks
        FROM Service_Requests SR
        INNER JOIN Technician_Assignments TA
            ON SR.REQ_ID = TA.REQ_ID
        INNER JOIN Site_Visits SV
            ON TA.Assignment_ID = SV.ASSIGNMENT_ID
        INNER JOIN Field_Datas FD
            ON SV.DATA_ID = FD.DATA_ID
        WHERE SR.REQ_ID = @REQ_ID
        ORDER BY SV.V_Date DESC";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@REQ_ID",
                        requestNumber
                    );

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        string siteCondition = "";
                        string accessibility = "";
                        string notes = "";

                        string remarks =
                            reader["Remarks"] == DBNull.Value
                                ? ""
                                : reader["Remarks"].ToString();

                        string[] lines =
                            remarks.Split(
                                new[] {
                            Environment.NewLine
                                },
                                StringSplitOptions.None
                            );

                        foreach (string line in lines)
                        {
                            if (line.StartsWith(
                                "Site Condition:",
                                StringComparison.OrdinalIgnoreCase))
                            {
                                siteCondition =
                                    line.Substring(
                                        "Site Condition:".Length
                                    ).Trim();
                            }
                            else if (line.StartsWith(
                                "Accessibility:",
                                StringComparison.OrdinalIgnoreCase))
                            {
                                accessibility =
                                    line.Substring(
                                        "Accessibility:".Length
                                    ).Trim();
                            }
                            else if (line.StartsWith(
                                "Assessment Notes:",
                                StringComparison.OrdinalIgnoreCase))
                            {
                                notes =
                                    line.Substring(
                                        "Assessment Notes:".Length
                                    ).Trim();
                            }
                        }

                        return new SiteAssessment
                        {
                            RequestID =
                                "REQ" +
                                Convert.ToInt32(
                                    reader["REQ_ID"]
                                ).ToString("D3"),

                            TechnicianID =
                                "TECH" +
                                Convert.ToInt32(
                                    reader["TECH_ID"]
                                ).ToString("D3"),

                            AssessmentDate =
                                Convert.ToDateTime(
                                    reader["V_Date"]
                                ),

                            SiteCondition =
                                siteCondition,

                            Accessibility =
                                accessibility,

                            Suitability =
                                reader["DRL_Status"].ToString(),

                            Notes =
                                notes
                        };
                    }
                }
            }
        }
    }
}