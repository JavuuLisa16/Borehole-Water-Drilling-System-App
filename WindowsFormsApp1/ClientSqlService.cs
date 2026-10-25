using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ClientSqlService
    {
        public static ClientProfile GetClientByPersonID(int personID)
        {

            string query = @"
            SELECT
                c.CLIENT_ID,
                p.First_Name,
                p.Last_Name,
                p.Gender,
                p.Email,
                p.Phone_Number,
                p.Username,
                l.Address_Line,
                ci.City_Name,
                pr.Prov_Name
            FROM Clients c
            INNER JOIN Persons p
                ON c.PersonID = p.PersonID
            LEFT JOIN Locations l
                ON c.LOC_ID = l.LOC_ID
            LEFT JOIN Cities ci
                ON l.CIT_ID = ci.CIT_ID
            LEFT JOIN Provinces pr
                ON ci.PROV_ID = pr.PROV_ID
            WHERE c.PersonID = @PersonID";

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

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        string address =
                            reader["Address_Line"] == DBNull.Value
                                ? ""
                                : reader["Address_Line"].ToString();

                        string city =
                            reader["City_Name"] == DBNull.Value
                                ? ""
                                : reader["City_Name"].ToString();

                        string province =
                            reader["Prov_Name"] == DBNull.Value
                                ? ""
                                : reader["Prov_Name"].ToString();

                        string location = "";

                        if (!string.IsNullOrWhiteSpace(address))
                        {
                            location = address;
                        }

                        if (!string.IsNullOrWhiteSpace(city))
                        {
                            if (!string.IsNullOrWhiteSpace(location))
                                location += ", ";

                            location += city;
                        }

                        if (!string.IsNullOrWhiteSpace(province))
                        {
                            if (!string.IsNullOrWhiteSpace(location))
                                location += ", ";

                            location += province;
                        }

                        return new ClientProfile
                        {
                            ClientID =
                                "C" +
                                Convert.ToInt32(
                                    reader["CLIENT_ID"]
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

                            Location =
                                location
                        };
                    }
                }
            }
        }

        public static List<ClientProfile> GetAllClients()
        {
            List<ClientProfile> clients = new List<ClientProfile>();

            string query = @"
        SELECT
            C.CLIENT_ID,
            P.First_Name,
            P.Last_Name,
            P.Gender,
            P.Email,
            P.Phone_Number,
            P.Username,
            L.Address_Line,
            CI.City_Name,
            PR.Prov_Name
        FROM Clients C
        INNER JOIN Persons P
            ON C.PersonID = P.PersonID
        LEFT JOIN Locations L
            ON C.LOC_ID = L.LOC_ID
        LEFT JOIN Cities CI
            ON L.CIT_ID = CI.CIT_ID
        LEFT JOIN Provinces PR
            ON CI.PROV_ID = PR.PROV_ID
        ORDER BY C.CLIENT_ID";

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
                            string city =
                                reader["City_Name"] == DBNull.Value
                                    ? ""
                                    : reader["City_Name"].ToString();

                            string province =
                                reader["Prov_Name"] == DBNull.Value
                                    ? ""
                                    : reader["Prov_Name"].ToString();

                            string location = city;

                            if (!string.IsNullOrWhiteSpace(province))
                            {
                                if (!string.IsNullOrWhiteSpace(location))
                                    location += ", ";

                                location += province;
                            }

                            clients.Add(new ClientProfile
                            {
                                ClientID =
                                    reader["CLIENT_ID"].ToString(),

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

                                Location =
                                    location
                            });
                        }
                    }
                }
            }

            return clients;
        }

        public static ClientProfile GetClientByID(string clientID)
        {
            int numericClientID;

            if (!int.TryParse(clientID, out numericClientID))
            {
                if (clientID.StartsWith("C"))
                {
                    if (!int.TryParse(
                            clientID.Substring(1),
                            out numericClientID))
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
            C.CLIENT_ID,
            P.First_Name,
            P.Last_Name,
            P.Gender,
            P.Email,
            P.Phone_Number,
            P.Username,
            L.Address_Line,
            CI.City_Name,
            PR.Prov_Name
        FROM Clients C
        INNER JOIN Persons P
            ON C.PersonID = P.PersonID
        LEFT JOIN Locations L
            ON C.LOC_ID = L.LOC_ID
        LEFT JOIN Cities CI
            ON L.CIT_ID = CI.CIT_ID
        LEFT JOIN Provinces PR
            ON CI.PROV_ID = PR.PROV_ID
        WHERE C.CLIENT_ID = @CLIENT_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@CLIENT_ID",
                        numericClientID
                    );

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        string city =
                            reader["City_Name"] == DBNull.Value
                                ? ""
                                : reader["City_Name"].ToString();

                        string province =
                            reader["Prov_Name"] == DBNull.Value
                                ? ""
                                : reader["Prov_Name"].ToString();

                        string location = city;

                        if (!string.IsNullOrWhiteSpace(province))
                        {
                            if (!string.IsNullOrWhiteSpace(location))
                                location += ", ";

                            location += province;
                        }

                        return new ClientProfile
                        {
                            ClientID =
                                reader["CLIENT_ID"].ToString(),

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

                            Location =
                                location
                        };
                    }
                }
            }
        }

        public static void DeleteClient(string clientID)
        {
            int numericClientID;

            if (!int.TryParse(clientID, out numericClientID))
            {
                if (clientID.StartsWith("C"))
                {
                    if (!int.TryParse(
                            clientID.Substring(1),
                            out numericClientID))
                    {
                        throw new Exception("Invalid Client ID.");
                    }
                }
                else
                {
                    throw new Exception("Invalid Client ID.");
                }
            }

            string getPersonQuery = @"
        SELECT PersonID
        FROM Clients
        WHERE CLIENT_ID = @CLIENT_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                int personID;

                using (SqlCommand command =
                       new SqlCommand(getPersonQuery, connection))
                {
                    command.Parameters.AddWithValue(
                        "@CLIENT_ID",
                        numericClientID
                    );

                    object result = command.ExecuteScalar();

                    if (result == null)
                        throw new Exception("Client could not be found.");

                    personID = Convert.ToInt32(result);
                }

                string deleteQuery = @"
            DELETE FROM Clients
            WHERE CLIENT_ID = @CLIENT_ID";

                using (SqlCommand command =
                       new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue(
                        "@CLIENT_ID",
                        numericClientID
                    );

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void AddClient(
 ClientProfile client,
 string password)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password is required.");
            }

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

            // CLIENT_ID is generated automatically by SQL Server.
            string clientQuery = @"
   INSERT INTO Clients
   (
       PersonID,
       LOC_ID
   )
   VALUES
   (
       @PersonID,
       @LOC_ID
   );";

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
                        int personID;

                        // 1. Create the Person record.
                        using (SqlCommand command =
                               new SqlCommand(
                                   personQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@First_Name",
                                client.FirstName);

                            command.Parameters.AddWithValue(
                                "@Last_Name",
                                client.LastName);

                            command.Parameters.AddWithValue(
                                "@Gender",
                                string.IsNullOrWhiteSpace(client.Gender)
                                    ? (object)DBNull.Value
                                    : client.Gender);

                            command.Parameters.AddWithValue(
                                "@Email",
                                string.IsNullOrWhiteSpace(client.Email)
                                    ? (object)DBNull.Value
                                    : client.Email);

                            command.Parameters.AddWithValue(
                                "@Phone_Number",
                                string.IsNullOrWhiteSpace(client.Phone)
                                    ? (object)DBNull.Value
                                    : client.Phone);

                            command.Parameters.AddWithValue(
                                "@Username",
                                client.Username);

                            personID = Convert.ToInt32(
                                command.ExecuteScalar());
                        }


                        // 2. Find the city entered in txtLocation.
                        int cityID;

                        string cityQuery = @"
                            SELECT CIT_ID
                            FROM Cities
                            WHERE LTRIM(RTRIM(City_Name)) = @CityName;";

                        using (SqlCommand command =
                               new SqlCommand(
                                   cityQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@CityName",
                                client.Location.Trim());

                            using (SqlDataReader reader =
                                   command.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    throw new Exception(
                                        "City not found. Please check the city name.");
                                }

                                cityID = Convert.ToInt32(
                                    reader["CIT_ID"]);

                                if (reader.Read())
                                {
                                    throw new Exception(
                                        "Multiple cities have this name. " +
                                        "Please identify the correct province.");
                                }
                            }
                        }

                        // 3. Create a Locations record and get its LOC_ID.
                        int locationID;

                        string locationQuery = @"
                            INSERT INTO Locations
                            (
                                CIT_ID,
                                Address_Line
                            )
                            VALUES
                            (
                                @CIT_ID,
                                @Address_Line
                            );

                            SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        using (SqlCommand command =
                               new SqlCommand(
                                   locationQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@CIT_ID",
                                cityID);

                            command.Parameters.AddWithValue(
                                "@Address_Line",
                                "Not provided");

                            locationID = Convert.ToInt32(
                                command.ExecuteScalar());
                        }

                        // 4. Create the Client record.
                        // SQL Server generates CLIENT_ID automatically.
                        using (SqlCommand command =
                               new SqlCommand(
                                   clientQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@PersonID",
                                personID);

                            command.Parameters.AddWithValue(
                                "@LOC_ID",
                                locationID);

                            command.ExecuteNonQuery();
                        }
                        // 5. Create the login credentials.
                        using (SqlCommand command =
                               new SqlCommand(
                                   credentialQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@PersonID",
                                personID);

                            command.Parameters.AddWithValue(
                                "@Password",
                                password);

                            command.Parameters.AddWithValue(
                                "@Role_Name",
                                "Client");

                            command.ExecuteNonQuery();
                        }

                        // 6. Save all records permanently.
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(
                            "An error occurred while adding the client: " +
                            ex.Message);
                    }
                }
            }
        }

        public static void UpdateClient(
        string clientID,
        ClientProfile client)
        {
            int numericClientID;

            if (!int.TryParse(clientID, out numericClientID))
            {
                if (clientID.StartsWith("C"))
                {
                    if (!int.TryParse(
                            clientID.Substring(1),
                            out numericClientID))
                    {
                        throw new Exception("Invalid Client ID.");
                    }
                }
                else
                {
                    throw new Exception("Invalid Client ID.");
                }
            }

            string getPersonQuery = @"
        SELECT PersonID
        FROM Clients
        WHERE CLIENT_ID = @CLIENT_ID";

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
                        "@CLIENT_ID",
                        numericClientID
                    );

                    object result =
                        command.ExecuteScalar();

                    if (result == null)
                    {
                        throw new Exception(
                            "Client could not be found."
                        );
                    }

                    personID = Convert.ToInt32(result);
                }

                string updatePersonQuery = @"
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
                           updatePersonQuery,
                           connection))
                {
                    command.Parameters.AddWithValue(
                        "@First_Name",
                        client.FirstName
                    );

                    command.Parameters.AddWithValue(
                        "@Last_Name",
                        client.LastName
                    );

                    command.Parameters.AddWithValue(
                        "@Gender",
                        string.IsNullOrWhiteSpace(client.Gender)
                            ? (object)DBNull.Value
                            : client.Gender
                    );

                    command.Parameters.AddWithValue(
                        "@Email",
                        string.IsNullOrWhiteSpace(client.Email)
                            ? (object)DBNull.Value
                            : client.Email
                    );

                    command.Parameters.AddWithValue(
                        "@Phone_Number",
                        string.IsNullOrWhiteSpace(client.Phone)
                            ? (object)DBNull.Value
                            : client.Phone
                    );

                    command.Parameters.AddWithValue(
                        "@Username",
                        string.IsNullOrWhiteSpace(client.Username)
                            ? (object)DBNull.Value
                            : client.Username
                    );

                    command.Parameters.AddWithValue(
                        "@PersonID",
                        personID
                    );

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

        