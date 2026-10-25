using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class TechnicianAssignmentRecord
    {
        public int AssignmentID { get; set; }
        public string RequestID { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string Service { get; set; }
        public string Provider { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ServiceDate { get; set; }
        public int TechnicianID { get; set; }
        public DateTime AssignmentDate { get; set; }
        public string AssignmentStatus { get; set; }
    }

    public static class TechnicianAssignmentSqlService
    {
        public static bool HasAssignment(int requestID)
        {
            string query = @"
            SELECT COUNT(*)
            FROM Technician_Assignments
            WHERE REQ_ID = @REQ_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@REQ_ID",
                        requestID
                    );

                    int count =
                        Convert.ToInt32(
                            command.ExecuteScalar()
                        );

                    return count > 0;
                }
            }
        }

        public static int AssignTechnician(
            int requestID,
            int technicianID)
        {
            string checkRequestQuery = @"
            SELECT COUNT(*)
            FROM Service_Requests
            WHERE REQ_ID = @REQ_ID";

            string checkTechnicianQuery = @"
            SELECT COUNT(*)
            FROM Technicians
            WHERE TECH_ID = @TECH_ID";

            string insertQuery = @"
            INSERT INTO Technician_Assignments
            (
                REQ_ID,
                TECH_ID,
                A_Date,
                A_Status
            )
            OUTPUT INSERTED.Assignment_ID
            VALUES
            (
                @REQ_ID,
                @TECH_ID,
                @A_Date,
                @A_Status
            )";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlTransaction transaction =
                       connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command =
                               new SqlCommand(
                                   checkRequestQuery,
                                   connection,
                                   transaction))
                        {
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
                                    "Service request could not be found."
                                );
                            }
                        }

                        using (SqlCommand command =
                               new SqlCommand(
                                   checkTechnicianQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@TECH_ID",
                                technicianID
                            );

                            int count =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );

                            if (count == 0)
                            {
                                throw new Exception(
                                    "Technician could not be found."
                                );
                            }
                        }

                        if (HasAssignmentWithinTransaction(
                            connection,
                            transaction,
                            requestID))
                        {
                            throw new Exception(
                                "This service request has already been assigned to a technician."
                            );
                        }

                        int assignmentID;

                        using (SqlCommand command =
                               new SqlCommand(
                                   insertQuery,
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

                            command.Parameters.AddWithValue(
                                "@A_Date",
                                DateTime.Today
                            );

                            command.Parameters.AddWithValue(
                                "@A_Status",
                                "Assigned"
                            );

                            assignmentID =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );
                        }

                        transaction.Commit();

                        return assignmentID;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private static bool HasAssignmentWithinTransaction(
            SqlConnection connection,
            SqlTransaction transaction,
            int requestID)
        {
            string query = @"
            SELECT COUNT(*)
            FROM Technician_Assignments
            WHERE REQ_ID = @REQ_ID";

            using (SqlCommand command =
                   new SqlCommand(
                       query,
                       connection,
                       transaction))
            {
                command.Parameters.AddWithValue(
                    "@REQ_ID",
                    requestID
                );

                int count =
                    Convert.ToInt32(
                        command.ExecuteScalar()
                    );

                return count > 0;
            }
        }

        public static List<TechnicianAssignmentRecord>
            GetAssignmentsByTechnicianID(
                int technicianID)
        {
            List<TechnicianAssignmentRecord> assignments =
                new List<TechnicianAssignmentRecord>();

            string query = @"
            SELECT
                ta.Assignment_ID,
                ta.REQ_ID,
                c.CLIENT_ID,
                p.First_Name,
                p.Last_Name,
                s.Serv_Name,
                dsp.DSP_Name,
                l.Address_Line,
                ci.City_Name,
                pr.Prov_Name,
                sr.REQ_Date,
                sr.Service_Date,
                ta.TECH_ID,
                ta.A_Date,
                ta.A_Status
            FROM Technician_Assignments ta

            INNER JOIN Service_Requests sr
                ON ta.REQ_ID = sr.REQ_ID

            INNER JOIN Client_Drilling_Service_Providers cdsp
                ON sr.CDSP_ID = cdsp.CDSP_ID

            INNER JOIN Clients c
                ON cdsp.CLIENT_ID = c.CLIENT_ID

            INNER JOIN Persons p
                ON c.PersonID = p.PersonID

            INNER JOIN Services s
                ON sr.SERV_ID = s.SERV_ID

            INNER JOIN Drilling_Service_Providers dsp
                ON sr.DSP_ID = dsp.DSP_ID

            INNER JOIN Locations l
                ON sr.LOC_ID = l.LOC_ID

            INNER JOIN Cities ci
                ON l.CIT_ID = ci.CIT_ID

            INNER JOIN Provinces pr
                ON ci.PROV_ID = pr.PROV_ID

            WHERE ta.TECH_ID = @TECH_ID

            ORDER BY ta.A_Date DESC";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@TECH_ID",
                        technicianID
                    );

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assignments.Add(
                                new TechnicianAssignmentRecord
                                {
                                    AssignmentID =
                                        Convert.ToInt32(
                                            reader["Assignment_ID"]
                                        ),

                                    RequestID =
                                        "REQ" +
                                        Convert.ToInt32(
                                            reader["REQ_ID"]
                                        ).ToString("D3"),

                                    ClientID =
                                        "C" +
                                        Convert.ToInt32(
                                            reader["CLIENT_ID"]
                                        ).ToString("D3"),

                                    ClientName =
                                        reader["First_Name"].ToString() +
                                        " " +
                                        reader["Last_Name"].ToString(),

                                    Service =
                                        reader["Serv_Name"].ToString(),

                                    Provider =
                                        reader["DSP_Name"].ToString(),

                                    AddressLine1 =
                                        reader["Address_Line"] == DBNull.Value
                                            ? ""
                                            : reader["Address_Line"]
                                                .ToString(),

                                    City =
                                        reader["City_Name"].ToString(),

                                    Province =
                                        reader["Prov_Name"].ToString(),

                                    RequestDate =
                                        Convert.ToDateTime(
                                            reader["REQ_Date"]
                                        ),

                                    ServiceDate =
                                        reader["Service_Date"] == DBNull.Value
                                            ? DateTime.MinValue
                                            : Convert.ToDateTime(
                                                reader["Service_Date"]
                                            ),

                                    TechnicianID =
                                        Convert.ToInt32(
                                            reader["TECH_ID"]
                                        ),

                                    AssignmentDate =
                                        Convert.ToDateTime(
                                            reader["A_Date"]
                                        ),

                                    AssignmentStatus =
                                        reader["A_Status"].ToString()
                                }
                            );
                        }
                    }
                }
            }

            return assignments;
        }

        public static bool UpdateAssignmentStatus(
            int assignmentID,
            string status)
        {
            string query = @"
            UPDATE Technician_Assignments
            SET A_Status = @A_Status
            WHERE Assignment_ID = @Assignment_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@Assignment_ID",
                        assignmentID
                    );

                    command.Parameters.AddWithValue(
                        "@A_Status",
                        status
                    );

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public static int GetTechnicianIDByPersonID(int personID)
        {
            string query = @"
        SELECT TECH_ID
        FROM Technicians
        WHERE PersonID = @PersonID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@PersonID",
                        personID
                    );

                    object result =
                        command.ExecuteScalar();

                    if (result == null)
                    {
                        throw new Exception(
                            "Technician profile could not be found."
                        );
                    }

                    return Convert.ToInt32(result);
                }
            }
        }

        public static List<int> GetTechnicianIDsByDSP(int dspID)
        {
            List<int> technicianIDs =
                new List<int>();

            string query = @"
        SELECT TECH_ID
        FROM Technicians
        WHERE DSP_ID = @DSP_ID
        ORDER BY TECH_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@DSP_ID",
                        dspID
                    );

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            technicianIDs.Add(
                                Convert.ToInt32(
                                    reader["TECH_ID"]
                                )
                            );
                        }
                    }
                }
            }

            return technicianIDs;
        }
    
    public static List<Assignment> GetAllAssignments()
        {
            List<Assignment> assignments =
                new List<Assignment>();

            string query = @"
        SELECT
            Assignment_ID,
            REQ_ID,
            TECH_ID,
            A_Date,
            A_Status
        FROM Technician_Assignments
        ORDER BY A_Date DESC";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assignments.Add(
                                new Assignment
                                {
                                    AssignmentID =
                                        "A" +
                                        Convert.ToInt32(
                                            reader["Assignment_ID"]
                                        ).ToString("D3"),

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

                                    AssignmentDate =
                                        Convert.ToDateTime(
                                            reader["A_Date"]
                                        ),

                                    Status =
                                        reader["A_Status"].ToString()
                                }
                            );
                        }
                    }
                }
            }

            return assignments;
        }

        public static Assignment GetAssignmentByID(
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
        SELECT
            Assignment_ID,
            REQ_ID,
            TECH_ID,
            A_Date,
            A_Status
        FROM Technician_Assignments
        WHERE Assignment_ID = @Assignment_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@Assignment_ID",
                        assignmentNumber
                    );

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        return new Assignment
                        {
                            AssignmentID =
                                "A" +
                                Convert.ToInt32(
                                    reader["Assignment_ID"]
                                ).ToString("D3"),

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

                            AssignmentDate =
                                Convert.ToDateTime(
                                    reader["A_Date"]
                                ),

                            Status =
                                reader["A_Status"].ToString()
                        };
                    }
                }
            }
        }
    }
}

