using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class TechnicianSqlService
    {
        public static List<Technician> GetAllTechnicians()
        {
            List<Technician> technicians =
                new List<Technician>();

            string query = @"
            SELECT
                T.TECH_ID,
                P.First_Name,
                P.Last_Name,
                P.Gender,
                P.Email,
                P.Phone_Number,
                P.Username,
                S.SP_Name,
                DSP.DSP_Name,

                CASE
                    WHEN EXISTS
                    (
                        SELECT 1
                        FROM Technician_Assignments TA
                        WHERE TA.TECH_ID = T.TECH_ID
                        AND TA.A_Status IN
                        (
                            'Assigned',
                            'Assessment Completed',
                            'In Progress',
                            'Site Visit Completed'
                        )
                    )
                    THEN 'Assigned'
                    ELSE 'Available'
                END AS Technician_Status

            FROM Technicians T

            INNER JOIN Persons P
                ON T.PersonID = P.PersonID

            LEFT JOIN Specialization S
                ON T.SP_ID = S.SP_ID

            LEFT JOIN Drilling_Service_Providers DSP
                ON T.DSP_ID = DSP.DSP_ID

            ORDER BY T.TECH_ID";

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
                            technicians.Add(
                                new Technician
                                {
                                    TechnicianID =
                                        "TECH" +
                                        Convert.ToInt32(
                                            reader["TECH_ID"]
                                        ).ToString("D3"),

                                    FirstName =
                                        reader["First_Name"].ToString(),

                                    LastName =
                                        reader["Last_Name"].ToString(),

                                    Gender =
                                        reader["Gender"] == DBNull.Value
                                            ? ""
                                            : reader["Gender"].ToString(),

                                    Email =
                                        reader["Email"] == DBNull.Value
                                            ? ""
                                            : reader["Email"].ToString(),

                                    Phone =
                                        reader["Phone_Number"] == DBNull.Value
                                            ? ""
                                            : reader["Phone_Number"].ToString(),

                                    Username =
                                        reader["Username"].ToString(),

                                    Specialisation =
                                        reader["SP_Name"] == DBNull.Value
                                            ? ""
                                            : reader["SP_Name"].ToString(),

                                    Provider =
                                        reader["DSP_Name"] == DBNull.Value
                                            ? ""
                                            : reader["DSP_Name"].ToString(),

                                    Status =
                                        reader["Technician_Status"].ToString()
                                }
                            );
                        }
                    }
                }
            }

            return technicians;
        }

        public static Technician GetTechnicianByID(
            string technicianID)
        {
            if (string.IsNullOrWhiteSpace(technicianID))
            {
                return null;
            }

            string numericPart =
                technicianID
                    .Replace("TECH", "")
                    .Trim();

            if (!int.TryParse(
                    numericPart,
                    out int technicianNumber))
            {
                return null;
            }

            foreach (Technician technician
                     in GetAllTechnicians())
            {
                if (technician.TechnicianID ==
                    "TECH" +
                    technicianNumber.ToString("D3"))
                {
                    return technician;
                }
            }

            return null;
        }

        public static bool TechnicianIDExists(
            string technicianID)
        {
            string numericPart =
                technicianID
                    .Replace("TECH", "")
                    .Trim();

            if (!int.TryParse(
                    numericPart,
                    out int technicianNumber))
            {
                return false;
            }

            string query = @"
            SELECT COUNT(*)
            FROM Technicians
            WHERE TECH_ID = @TECH_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@TECH_ID",
                        technicianNumber
                    );

                    return Convert.ToInt32(
                        command.ExecuteScalar()
                    ) > 0;
                }
            }
        }

        public static int AddTechnician(
            Technician technician, string password)
        {
            string getSpecializationQuery = @"
            SELECT SP_ID
            FROM Specialization
            WHERE SP_Name = @SP_Name";

            string getProviderQuery = @"
            SELECT DSP_ID
            FROM Drilling_Service_Providers
            WHERE DSP_Name = @DSP_Name";

            string checkUsernameQuery = @"
            SELECT COUNT(*)
            FROM Persons
            WHERE Username = @Username";

            string insertPersonQuery = @"
            INSERT INTO Persons
            (
                First_Name,
                Last_Name,
                Gender,
                Email,
                Phone_Number,
                Username
            )
            OUTPUT INSERTED.PersonID
            VALUES
            (
                @First_Name,
                @Last_Name,
                @Gender,
                @Email,
                @Phone_Number,
                @Username
            )";

            string insertTechnicianQuery = @"
            INSERT INTO Technicians
            (
                PersonID,
                SP_ID,
                DSP_ID,
                Hired_Date
            )
            OUTPUT INSERTED.TECH_ID
            VALUES
            (
                @PersonID,
                @SP_ID,
                @DSP_ID,
                @Hired_Date
            )";
            string insertCredentialQuery = @"
INSERT INTO Credentials
(
    PersonID,
    Password,
    Role_Name
)
VALUES
(
    @PersonID,
    @Password,
    @Role_Name
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
                        int specializationID;
                        int providerID;
                        int personID;
                        int technicianID;

                        using (SqlCommand command =
                               new SqlCommand(
                                   checkUsernameQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@Username",
                                technician.Username
                            );

                            int count =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );

                            if (count > 0)
                            {
                                throw new Exception(
                                    "A person with this username already exists."
                                );
                            }
                        }

                        using (SqlCommand command =
                               new SqlCommand(
                                   getSpecializationQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@SP_Name",
                                technician.Specialisation
                            );

                            object result =
                                command.ExecuteScalar();

                            if (result == null)
                            {
                                throw new Exception(
                                    "The selected specialization could not be found."
                                );
                            }

                            specializationID =
                                Convert.ToInt32(result);
                        }

                        using (SqlCommand command =
                               new SqlCommand(
                                   getProviderQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@DSP_Name",
                                technician.Provider
                            );

                            object result =
                                command.ExecuteScalar();

                            if (result == null)
                            {
                                throw new Exception(
                                    "The selected drilling service provider could not be found."
                                );
                            }

                            providerID =
                                Convert.ToInt32(result);
                        }

                        using (SqlCommand command =
                               new SqlCommand(
                                   insertPersonQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@First_Name",
                                technician.FirstName
                            );

                            command.Parameters.AddWithValue(
                                "@Last_Name",
                                technician.LastName
                            );

                            command.Parameters.AddWithValue(
                                "@Gender",
                                technician.Gender
                            );

                            command.Parameters.AddWithValue(
                                "@Email",
                                technician.Email
                            );

                            command.Parameters.AddWithValue(
                                "@Phone_Number",
                                technician.Phone
                            );

                            command.Parameters.AddWithValue(
                                "@Username",
                                technician.Username
                            );

                            personID =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );
                        }

                        using (SqlCommand command =
                               new SqlCommand(
                                   insertTechnicianQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@PersonID",
                                personID
                            );

                            command.Parameters.AddWithValue(
                                "@SP_ID",
                                specializationID
                            );

                            command.Parameters.AddWithValue(
                                "@DSP_ID",
                                providerID
                            );

                            command.Parameters.AddWithValue(
                                "@Hired_Date",
                                DateTime.Today
                            );

                            technicianID =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );
                        }

                        using (SqlCommand command =
       new SqlCommand(
           insertCredentialQuery,
           connection,
           transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@PersonID",
                                personID
                            );

                            command.Parameters.AddWithValue(
                                "@Password",
                                password
                            );

                            command.Parameters.AddWithValue(
                                "@Role_Name",
                                "Technician"
                            );

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        return technicianID;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static bool UpdateTechnician(
            string technicianID,
            Technician technician)
        {
            string numericPart =
                technicianID
                    .Replace("TECH", "")
                    .Trim();

            if (!int.TryParse(
                    numericPart,
                    out int technicianNumber))
            {
                return false;
            }

            string query = @"
            UPDATE P
            SET
                P.First_Name = @First_Name,
                P.Last_Name = @Last_Name,
                P.Gender = @Gender,
                P.Email = @Email,
                P.Phone_Number = @Phone_Number,
                P.Username = @Username
            FROM Persons P
            INNER JOIN Technicians T
                ON P.PersonID = T.PersonID
            WHERE T.TECH_ID = @TECH_ID;

            UPDATE T
            SET
                T.SP_ID =
                (
                    SELECT SP_ID
                    FROM Specialization
                    WHERE SP_Name = @SP_Name
                ),

                T.DSP_ID =
                (
                    SELECT DSP_ID
                    FROM Drilling_Service_Providers
                    WHERE DSP_Name = @DSP_Name
                )
            FROM Technicians T
            WHERE T.TECH_ID = @TECH_ID;";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@TECH_ID",
                        technicianNumber
                    );

                    command.Parameters.AddWithValue(
                        "@First_Name",
                        technician.FirstName
                    );

                    command.Parameters.AddWithValue(
                        "@Last_Name",
                        technician.LastName
                    );

                    command.Parameters.AddWithValue(
                        "@Gender",
                        technician.Gender
                    );

                    command.Parameters.AddWithValue(
                        "@Email",
                        technician.Email
                    );

                    command.Parameters.AddWithValue(
                        "@Phone_Number",
                        technician.Phone
                    );

                    command.Parameters.AddWithValue(
                        "@Username",
                        technician.Username
                    );

                    command.Parameters.AddWithValue(
                        "@SP_Name",
                        technician.Specialisation
                    );

                    command.Parameters.AddWithValue(
                        "@DSP_Name",
                        technician.Provider
                    );

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool DeleteTechnician(
            string technicianID)
        {
            string numericPart =
                technicianID
                    .Replace("TECH", "")
                    .Trim();

            if (!int.TryParse(
                    numericPart,
                    out int technicianNumber))
            {
                return false;
            }

            string findPersonQuery = @"
            SELECT PersonID
            FROM Technicians
            WHERE TECH_ID = @TECH_ID";

            string checkAssignmentsQuery = @"
            SELECT COUNT(*)
            FROM Technician_Assignments
            WHERE TECH_ID = @TECH_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlTransaction transaction =
                           connection.BeginTransaction())
                {
                    try
                    {
                        int personID;

                        using (SqlCommand command =
                               new SqlCommand(
                                   checkAssignmentsQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@TECH_ID",
                                technicianNumber
                            );

                            int assignmentCount =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );

                            if (assignmentCount > 0)
                            {
                                throw new Exception(
                                    "This technician cannot be deleted because they have assignment records."
                                );
                            }
                        }

                        using (SqlCommand command =
                               new SqlCommand(
                                   findPersonQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@TECH_ID",
                                technicianNumber
                            );

                            object result =
                                command.ExecuteScalar();

                            if (result == null)
                            {
                                transaction.Rollback();
                                return false;
                            }

                            personID =
                                Convert.ToInt32(result);
                        }

                        using (SqlCommand command =
                               new SqlCommand(
                                   "DELETE FROM Credentials WHERE PersonID = @PersonID",
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@PersonID",
                                personID
                            );

                            command.ExecuteNonQuery();
                        }

                        using (SqlCommand command =
                               new SqlCommand(
                                   "DELETE FROM Technicians WHERE TECH_ID = @TECH_ID",
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@TECH_ID",
                                technicianNumber
                            );

                            command.ExecuteNonQuery();
                        }

                        using (SqlCommand command =
                               new SqlCommand(
                                   "DELETE FROM Persons WHERE PersonID = @PersonID",
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@PersonID",
                                personID
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
    }
}

        