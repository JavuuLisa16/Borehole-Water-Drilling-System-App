using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class CredentialSqlService
    {
        public static Credential FindByEmail(string email)
        {
            string query = @"
            SELECT
                c.PersonID,
                p.Username,
                c.Password,
                c.Role_Name,
                p.Email
            FROM Credentials c
            INNER JOIN Persons p
                ON c.PersonID = p.PersonID
            WHERE p.Email = @Email";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@Email",
                        email
                    );

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        return new Credential
                        {
                            PersonID =
                                Convert.ToInt32(
                                    reader["PersonID"]
                                ),

                            Username =
                                reader["Username"].ToString(),

                            Password =
                                reader["Password"].ToString(),

                            Role =
                                reader["Role_Name"].ToString(),

                            Email =
                                reader["Email"] == DBNull.Value
                                    ? ""
                                    : reader["Email"].ToString()
                        };
                    }
                }
            }
        }

        public static Credential FindByUsername(string username)
        {
            string query = @"
            SELECT
                c.PersonID,
                p.Username,
                c.Password,
                c.Role_Name,
                p.Email
            FROM Credentials c
            INNER JOIN Persons p
                ON c.PersonID = p.PersonID
            WHERE p.Username = @Username";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@Username",
                        username
                    );

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        return new Credential
                        {
                            PersonID =
                                Convert.ToInt32(
                                    reader["PersonID"]
                                ),

                            Username =
                                reader["Username"].ToString(),

                            Password =
                                reader["Password"].ToString(),

                            Role =
                                reader["Role_Name"].ToString(),

                            Email =
                                reader["Email"] == DBNull.Value
                                    ? ""
                                    : reader["Email"].ToString()
                        };
                    }
                }
            }
        }

        

        public static void UpdatePassword(
        int personID,
        string newPassword)
        {
            if (personID <= 0)
            {
                throw new Exception("Invalid PersonID.");
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new Exception("Password is required.");
            }

            string query = @"
        UPDATE Credentials
        SET Password = @Password
        WHERE PersonID = @PersonID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@Password",
                        newPassword
                    );

                    command.Parameters.AddWithValue(
                        "@PersonID",
                        personID
                    );

                    int rows =
                        command.ExecuteNonQuery();

                    if (rows == 0)
                    {
                        throw new Exception(
                            "Credentials could not be found."
                        );
                    }
                }
            }
        }
    }
}

