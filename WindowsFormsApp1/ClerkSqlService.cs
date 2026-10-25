using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ClerkSqlService
    {
        public static List<ClerkProfile> GetAllClerks()
        {
            List<ClerkProfile> clerks = new List<ClerkProfile>();

            string query = @"
            SELECT
                C.CLERK_ID,
                P.First_Name,
                P.Last_Name,
                P.Gender,
                P.Email,
                P.Phone_Number,
                P.Username,
                DSP.DSP_Name,
                C.Emply_Date
            FROM Clerks C
            INNER JOIN Persons P
                ON C.PersonID = P.PersonID
            LEFT JOIN Drilling_Service_Providers DSP
                ON C.DSP_ID = DSP.DSP_ID
            ORDER BY C.CLERK_ID";

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
                            clerks.Add(new ClerkProfile
                            {
                                ClerkID =
                                    "CL" +
                                    Convert.ToInt32(
                                        reader["CLERK_ID"]
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
                                    reader["Username"] == DBNull.Value
                                        ? ""
                                        : reader["Username"].ToString(),

                                Organisation =
                                    reader["DSP_Name"] == DBNull.Value
                                        ? ""
                                        : reader["DSP_Name"].ToString(),

                                EmploymentDate =
                                    reader["Emply_Date"] == DBNull.Value
                                        ? DateTime.MinValue
                                        : Convert.ToDateTime(
                                            reader["Emply_Date"]
                                        )
                            });
                        }
                    }
                }
            }

            return clerks;
        }

        public static ClerkProfile GetClerkByID(string clerkID)
        {
            int numericClerkID;

            if (!int.TryParse(clerkID, out numericClerkID))
            {
                if (clerkID.StartsWith("CL"))
                {
                    if (!int.TryParse(
                            clerkID.Substring(2),
                            out numericClerkID))
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

            string query = @"
            SELECT
                C.CLERK_ID,
                P.First_Name,
                P.Last_Name,
                P.Gender,
                P.Email,
                P.Phone_Number,
                P.Username,
                DSP.DSP_Name,
                C.Emply_Date
            FROM Clerks C
            INNER JOIN Persons P
                ON C.PersonID = P.PersonID
            LEFT JOIN Drilling_Service_Providers DSP
                ON C.DSP_ID = DSP.DSP_ID
            WHERE C.CLERK_ID = @CLERK_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@CLERK_ID",
                        numericClerkID
                    );

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        return new ClerkProfile
                        {
                            ClerkID =
                                "CL" +
                                Convert.ToInt32(
                                    reader["CLERK_ID"]
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
                                reader["Username"] == DBNull.Value
                                    ? ""
                                    : reader["Username"].ToString(),

                            Organisation =
                                reader["DSP_Name"] == DBNull.Value
                                    ? ""
                                    : reader["DSP_Name"].ToString(),

                            EmploymentDate =
                                reader["Emply_Date"] == DBNull.Value
                                    ? DateTime.MinValue
                                    : Convert.ToDateTime(
                                        reader["Emply_Date"]
                                    )
                        };
                    }
                }
            }
        }

        public static void AddClerk(
    ClerkProfile clerk,
    string password)
        {
            int numericClerkID;

            if (!int.TryParse(clerk.ClerkID, out numericClerkID))
            {
                if (clerk.ClerkID.StartsWith("CL"))
                {
                    if (!int.TryParse(
                            clerk.ClerkID.Substring(2),
                            out numericClerkID))
                    {
                        throw new Exception(
                            "Invalid Clerk ID. Use a format such as CL001."
                        );
                    }
                }
                else
                {
                    throw new Exception(
                        "Invalid Clerk ID. Use a format such as CL001."
                    );
                }
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password is required.");
            }

            int dspID = GetDSPID(clerk.Organisation);

            if (dspID == 0)
            {
                throw new Exception(
                    "The selected organisation could not be found."
                );
            }

            string checkIDQuery = @"
SELECT COUNT(*)
FROM Clerks
WHERE CLERK_ID = @CLERK_ID";

            string personQuery = @"
INSERT INTO Persons
(
    First_Name,
    Last_Name,
    Gender,
    Email,
    Phone_Number,
    Username
)
VALUES
(
    @First_Name,
    @Last_Name,
    @Gender,
    @Email,
    @Phone_Number,
    @Username
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            string clerkQuery = @"
SET IDENTITY_INSERT Clerks ON;

INSERT INTO Clerks
(
    CLERK_ID,
    PersonID,
    DSP_ID,
    Emply_Date
)
VALUES
(
    @CLERK_ID,
    @PersonID,
    @DSP_ID,
    @Emply_Date
);

SET IDENTITY_INSERT Clerks OFF;";

            string credentialQuery = @"
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
);";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlTransaction transaction =
                       connection.BeginTransaction())
                {
                    try
                    {
                        // Check whether the Clerk ID already exists
                        using (SqlCommand command =
                               new SqlCommand(
                                   checkIDQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@CLERK_ID",
                                numericClerkID
                            );

                            int count =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );

                            if (count > 0)
                            {
                                throw new Exception(
                                    "A clerk with this ID already exists."
                                );
                            }
                        }

                        // Insert the Person
                        int personID;

                        using (SqlCommand command =
                               new SqlCommand(
                                   personQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@First_Name",
                                clerk.FirstName
                            );

                            command.Parameters.AddWithValue(
                                "@Last_Name",
                                clerk.LastName
                            );

                            command.Parameters.AddWithValue(
                                "@Gender",
                                string.IsNullOrWhiteSpace(clerk.Gender)
                                    ? (object)DBNull.Value
                                    : clerk.Gender
                            );

                            command.Parameters.AddWithValue(
                                "@Email",
                                string.IsNullOrWhiteSpace(clerk.Email)
                                    ? (object)DBNull.Value
                                    : clerk.Email
                            );

                            command.Parameters.AddWithValue(
                                "@Phone_Number",
                                string.IsNullOrWhiteSpace(clerk.Phone)
                                    ? (object)DBNull.Value
                                    : clerk.Phone
                            );

                            command.Parameters.AddWithValue(
                                "@Username",
                                clerk.Username
                            );

                            personID =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );
                        }

                        // Insert the Clerk
                        using (SqlCommand command =
                               new SqlCommand(
                                   clerkQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@CLERK_ID",
                                numericClerkID
                            );

                            command.Parameters.AddWithValue(
                                "@PersonID",
                                personID
                            );

                            command.Parameters.AddWithValue(
                                "@DSP_ID",
                                dspID
                            );

                            command.Parameters.AddWithValue(
                                "@Emply_Date",
                                clerk.EmploymentDate
                            );

                            command.ExecuteNonQuery();
                        }

                        // Insert the Credentials
                        using (SqlCommand command =
                               new SqlCommand(
                                   credentialQuery,
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
                                "Clerk"
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

        public static void UpdateClerk(
            string clerkID,
            ClerkProfile clerk)
        {
            int numericClerkID;

            if (!int.TryParse(clerkID, out numericClerkID))
            {
                if (clerkID.StartsWith("CL"))
                {
                    if (!int.TryParse(
                            clerkID.Substring(2),
                            out numericClerkID))
                    {
                        throw new Exception("Invalid Clerk ID.");
                    }
                }
                else
                {
                    throw new Exception("Invalid Clerk ID.");
                }
            }

            int dspID = GetDSPID(clerk.Organisation);

            if (dspID == 0)
            {
                throw new Exception(
                    "The selected organisation could not be found."
                );
            }

            string getPersonQuery = @"
            SELECT PersonID
            FROM Clerks
            WHERE CLERK_ID = @CLERK_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                int personID;

                using (SqlCommand command =
                       new SqlCommand(
                           getPersonQuery,
                           connection))
                {
                    command.Parameters.AddWithValue(
                        "@CLERK_ID",
                        numericClerkID
                    );

                    object result =
                        command.ExecuteScalar();

                    if (result == null)
                    {
                        throw new Exception(
                            "Clerk could not be found."
                        );
                    }

                    personID = Convert.ToInt32(result);
                }

                string personUpdate = @"
                UPDATE Persons
                SET
                    First_Name = @First_Name,
                    Last_Name = @Last_Name,
                    Gender = @Gender,
                    Email = @Email,
                    Phone_Number = @Phone_Number,
                    Username = @Username
                WHERE PersonID = @PersonID";

                using (SqlCommand command =
                       new SqlCommand(
                           personUpdate,
                           connection))
                {
                    command.Parameters.AddWithValue(
                        "@First_Name",
                        clerk.FirstName
                    );

                    command.Parameters.AddWithValue(
                        "@Last_Name",
                        clerk.LastName
                    );

                    command.Parameters.AddWithValue(
                        "@Gender",
                        string.IsNullOrWhiteSpace(clerk.Gender)
                            ? (object)DBNull.Value
                            : clerk.Gender
                    );

                    command.Parameters.AddWithValue(
                        "@Email",
                        string.IsNullOrWhiteSpace(clerk.Email)
                            ? (object)DBNull.Value
                            : clerk.Email
                    );

                    command.Parameters.AddWithValue(
                        "@Phone_Number",
                        string.IsNullOrWhiteSpace(clerk.Phone)
                            ? (object)DBNull.Value
                            : clerk.Phone
                    );

                    command.Parameters.AddWithValue(
                        "@Username",
                        clerk.Username
                    );

                    command.Parameters.AddWithValue(
                        "@PersonID",
                        personID
                    );

                    command.ExecuteNonQuery();
                }

                string clerkUpdate = @"
                UPDATE Clerks
                SET
                    DSP_ID = @DSP_ID,
                    Emply_Date = @Emply_Date
                WHERE CLERK_ID = @CLERK_ID";

                using (SqlCommand command =
                       new SqlCommand(
                           clerkUpdate,
                           connection))
                {
                    command.Parameters.AddWithValue(
                        "@DSP_ID",
                        dspID
                    );

                    command.Parameters.AddWithValue(
                        "@Emply_Date",
                        clerk.EmploymentDate
                    );

                    command.Parameters.AddWithValue(
                        "@CLERK_ID",
                        numericClerkID
                    );

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteClerk(string clerkID)
        {
            int numericClerkID;

            if (!int.TryParse(clerkID, out numericClerkID))
            {
                if (clerkID.StartsWith("CL"))
                {
                    if (!int.TryParse(
                            clerkID.Substring(2),
                            out numericClerkID))
                    {
                        throw new Exception("Invalid Clerk ID.");
                    }
                }
                else
                {
                    throw new Exception("Invalid Clerk ID.");
                }
            }

            string getPersonQuery = @"
            SELECT PersonID
            FROM Clerks
            WHERE CLERK_ID = @CLERK_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                int personID;

                using (SqlCommand command =
                       new SqlCommand(
                           getPersonQuery,
                           connection))
                {
                    command.Parameters.AddWithValue(
                        "@CLERK_ID",
                        numericClerkID
                    );

                    object result =
                        command.ExecuteScalar();

                    if (result == null)
                    {
                        throw new Exception(
                            "Clerk could not be found."
                        );
                    }

                    personID = Convert.ToInt32(result);
                }

                using (SqlTransaction transaction =
                       connection.BeginTransaction())
                {
                    try
                    {
                        string deleteCredentials = @"
                        DELETE FROM Credentials
                        WHERE PersonID = @PersonID";

                        using (SqlCommand command =
                               new SqlCommand(
                                   deleteCredentials,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@PersonID",
                                personID
                            );

                            command.ExecuteNonQuery();
                        }

                        string deleteClerk = @"
                        DELETE FROM Clerks
                        WHERE CLERK_ID = @CLERK_ID";

                        using (SqlCommand command =
                               new SqlCommand(
                                   deleteClerk,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@CLERK_ID",
                                numericClerkID
                            );

                            command.ExecuteNonQuery();
                        }

                        string deletePerson = @"
                        DELETE FROM Persons
                        WHERE PersonID = @PersonID";

                        using (SqlCommand command =
                               new SqlCommand(
                                   deletePerson,
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
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private static int GetDSPID(string organisation)
        {
            string query = @"
            SELECT DSP_ID
            FROM Drilling_Service_Providers
            WHERE DSP_Name = @DSP_Name";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@DSP_Name",
                        organisation
                    );

                    object result =
                        command.ExecuteScalar();

                    if (result == null)
                        return 0;

                    return Convert.ToInt32(result);
                }
            }
        }
    }
}

       