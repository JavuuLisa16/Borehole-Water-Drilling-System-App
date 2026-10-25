using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ServiceRequestSqlService
    {
        public static ServiceRequest CreateRequest(
       int personID,
       string serviceName,
       string providerName,
       string addressLine1,
       string addressLine2,
       string cityName,
       DateTime serviceDate,
       string description)
        {
            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlTransaction transaction =
                       connection.BeginTransaction())
                {
                    try
                    {
                        // Get the client's CLIENT_ID
                        string clientQuery = @"
                        SELECT CLIENT_ID
                        FROM Clients
                        WHERE PersonID = @PersonID";

                        int clientID;

                        using (SqlCommand command =
                               new SqlCommand(clientQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@PersonID", personID);

                            object result = command.ExecuteScalar();

                            if (result == null)
                                throw new Exception(
                                    "The logged-in client could not be found."
                                );

                            clientID = Convert.ToInt32(result);
                        }

                        // Get DSP_ID
                        string providerQuery = @"
                        SELECT DSP_ID
                        FROM Drilling_Service_Providers
                        WHERE DSP_Name = @ProviderName";

                        int dspID;

                        using (SqlCommand command =
                               new SqlCommand(providerQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@ProviderName", providerName);

                            object result = command.ExecuteScalar();

                            if (result == null)
                                throw new Exception(
                                    "The selected drilling provider could not be found."
                                );

                            dspID = Convert.ToInt32(result);
                        }

                        // Get SERV_ID
                        string serviceQuery = @"
                        SELECT SERV_ID
                        FROM Services
                        WHERE Serv_Name = @ServiceName";

                        int servID;

                        using (SqlCommand command =
                               new SqlCommand(serviceQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@ServiceName", serviceName);

                            object result = command.ExecuteScalar();

                            if (result == null)
                                throw new Exception(
                                    "The selected service could not be found."
                                );

                            servID = Convert.ToInt32(result);
                        }

                        // Get CIT_ID
                        string cityQuery = @"
                        SELECT CIT_ID
                        FROM Cities
                        WHERE City_Name = @CityName";

                        int cityID;

                        using (SqlCommand command =
                               new SqlCommand(cityQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@CityName", cityName);

                            object result = command.ExecuteScalar();

                            if (result == null)
                                throw new Exception(
                                    "The selected city could not be found."
                                );

                            cityID = Convert.ToInt32(result);
                        }

                        // Create the location
                        string address = addressLine1;

                        if (!string.IsNullOrWhiteSpace(addressLine2))
                        {
                            address += Environment.NewLine + addressLine2;
                        }

                        string locationQuery = @"
                        INSERT INTO Locations
                        (
                            CIT_ID,
                            Address_Line
                        )
                        OUTPUT INSERTED.LOC_ID
                        VALUES
                        (
                            @CityID,
                            @AddressLine
                        )";

                        int locationID;

                        using (SqlCommand command =
                               new SqlCommand(locationQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@CityID", cityID);

                            command.Parameters.AddWithValue(
                                "@AddressLine", address);

                            locationID =
                                Convert.ToInt32(command.ExecuteScalar());
                        }

                        // Get or create the client/provider relationship
                        string cdspQuery = @"
                        SELECT CDSP_ID
                        FROM Client_Drilling_Service_Providers
                        WHERE CLIENT_ID = @ClientID
                        AND DSP_ID = @DSP_ID";

                        int cdspID;

                        using (SqlCommand command =
                               new SqlCommand(cdspQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@ClientID", clientID);

                            command.Parameters.AddWithValue(
                                "@DSP_ID", dspID);

                            object result = command.ExecuteScalar();

                            if (result != null)
                            {
                                cdspID = Convert.ToInt32(result);
                            }
                            else
                            {
                                string insertCDSP = @"
                                INSERT INTO Client_Drilling_Service_Providers
                                (
                                    CLIENT_ID,
                                    DSP_ID
                                )
                                OUTPUT INSERTED.CDSP_ID
                                VALUES
                                (
                                    @ClientID,
                                    @DSP_ID
                                )";

                                using (SqlCommand insertCommand =
                                       new SqlCommand(
                                           insertCDSP,
                                           connection,
                                           transaction))
                                {
                                    insertCommand.Parameters.AddWithValue(
                                        "@ClientID", clientID);

                                    insertCommand.Parameters.AddWithValue(
                                        "@DSP_ID", dspID);

                                    cdspID =
                                        Convert.ToInt32(
                                            insertCommand.ExecuteScalar());
                                }
                            }
                        }

                        // Create the service request
                        string requestQuery = @"
                        INSERT INTO Service_Requests
                        (
                            CDSP_ID,
                            DSP_ID,
                            SERV_ID,
                            LOC_ID,
                            REQ_Date,
                            REQ_Status,
                            Service_Date,
                            Description
                        )
                        OUTPUT INSERTED.REQ_ID
                        VALUES
                        (
                            @CDSP_ID,
                            @DSP_ID,
                            @SERV_ID,
                            @LOC_ID,
                            @REQ_Date,
                            @REQ_Status,
                            @Service_Date,
                            @Description
                        )";

                        int requestID;

                        using (SqlCommand command =
                               new SqlCommand(
                                   requestQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@CDSP_ID", cdspID);

                            command.Parameters.AddWithValue(
                                "@DSP_ID", dspID);

                            command.Parameters.AddWithValue(
                                "@SERV_ID", servID);

                            command.Parameters.AddWithValue(
                                "@LOC_ID", locationID);

                            command.Parameters.AddWithValue(
                                "@REQ_Date", DateTime.Today);

                            command.Parameters.AddWithValue(
                                "@REQ_Status", "Pending");

                            command.Parameters.AddWithValue(
                                "@Service_Date", serviceDate.Date);

                            command.Parameters.AddWithValue(
                                "@Description", description);

                            requestID =
                                Convert.ToInt32(
                                    command.ExecuteScalar());
                        }

                        transaction.Commit();

                        return new ServiceRequest
                        {
                            RequestID =
                                "REQ" + requestID.ToString("D3"),

                            ClientID =
                                "C" + clientID.ToString("D3"),

                            Service =
                                serviceName,

                            Provider =
                                providerName,

                            Addressline1 =
                                addressLine1,

                            Addressline2 =
                                addressLine2,

                            City =
                                cityName,

                            RequestDate =
                                DateTime.Today,

                            ServiceDate =
                                serviceDate.Date,

                            Description =
                                description,

                            Status =
                                "Pending"
                        };
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public static List<ServiceRequest> GetRequestsByPersonID(int personID)
        {
            List<ServiceRequest> requests = new List<ServiceRequest>();

            string query = @"
        SELECT
            sr.REQ_ID,
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
            sr.Description,
            sr.REQ_Status
        FROM Service_Requests sr
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
        WHERE c.PersonID = @PersonID
        ORDER BY sr.REQ_ID DESC";

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
                        while (reader.Read())
                        {
                            string fullAddress =
                                reader["Address_Line"].ToString();

                            string addressLine1 = fullAddress;
                            string addressLine2 = "";

                            string[] addressParts =
                                fullAddress.Split(
                                    new[] { "\r\n", "\n" },
                                    StringSplitOptions.None
                                );

                            if (addressParts.Length > 0)
                            {
                                addressLine1 = addressParts[0];
                            }

                            if (addressParts.Length > 1)
                            {
                                addressLine2 = addressParts[1];
                            }

                            requests.Add(new ServiceRequest
                            {
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

                                Addressline1 =
                                    addressLine1,

                                Addressline2 =
                                    addressLine2,

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

                                Description =
                                    reader["Description"] == DBNull.Value
                                        ? ""
                                        : reader["Description"].ToString(),

                                Status =
                                    reader["REQ_Status"].ToString()
                            });
                        }
                    }
                }
            }

            return requests;
        }
        public static ServiceRequest GetRequestByRequestID(string requestID)
        {
            if (!requestID.StartsWith("REQ") ||
                !int.TryParse(requestID.Substring(3), out int requestNumber))
            {
                return null;
            }

            string query = @"
        SELECT
            sr.REQ_ID,
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
            sr.Description,
            sr.REQ_Status
        FROM Service_Requests sr
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
        WHERE sr.REQ_ID = @RequestID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@RequestID",
                        requestNumber
                    );

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        string fullAddress =
                            reader["Address_Line"].ToString();

                        string addressLine1 = fullAddress;
                        string addressLine2 = "";

                        string[] addressParts =
                            fullAddress.Split(
                                new[] { "\r\n", "\n" },
                                StringSplitOptions.None
                            );

                        if (addressParts.Length > 0)
                        {
                            addressLine1 = addressParts[0];
                        }

                        if (addressParts.Length > 1)
                        {
                            addressLine2 = addressParts[1];
                        }

                        return new ServiceRequest
                        {
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

                            Addressline1 =
                                addressLine1,

                            Addressline2 =
                                addressLine2,

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

                            Description =
                                reader["Description"] == DBNull.Value
                                    ? ""
                                    : reader["Description"].ToString(),

                            Status =
                                reader["REQ_Status"].ToString()
                        };
                    }
                }
            }
        }
        public static bool CancelRequest(int requestID)
        {
            string query = @"
        UPDATE Service_Requests
        SET REQ_Status = 'Cancelled'
        WHERE REQ_ID = @RequestID
        AND REQ_Status = 'Pending'";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@RequestID",
                        requestID
                    );

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        public static List<ServiceRequest> GetAllRequests()
        {
            List<ServiceRequest> requests =
                new List<ServiceRequest>();

            string query = @"
        SELECT
            sr.REQ_ID,
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
            sr.Description,
            sr.REQ_Status
        FROM Service_Requests sr

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

        ORDER BY sr.REQ_Date DESC";

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
                            string fullAddress =
                                reader["Address_Line"] == DBNull.Value
                                    ? ""
                                    : reader["Address_Line"].ToString();

                            string[] addressParts = fullAddress.Split(
                                new[] { "\r\n", "\n" },
                                StringSplitOptions.None
                            );

                            string addressLine1 =
                                addressParts.Length > 0 ? addressParts[0] : "";

                            string addressLine2 =
                                addressParts.Length > 1 ? addressParts[1] : "";

                            requests.Add(new ServiceRequest
                            {
                                RequestID = "REQ" +
                                    Convert.ToInt32(reader["REQ_ID"]).ToString("D3"),

                                ClientID = "C" +
                                    Convert.ToInt32(reader["CLIENT_ID"]).ToString("D3"),

                                ClientName =
                                    reader["First_Name"].ToString() + " " +
                                    reader["Last_Name"].ToString(),

                                Service = reader["Serv_Name"].ToString(),
                                Provider = reader["DSP_Name"].ToString(),

                                Addressline1 = addressLine1,
                                Addressline2 = addressLine2,

                                City = reader["City_Name"].ToString(),
                                Province = reader["Prov_Name"].ToString(),

                                RequestDate = Convert.ToDateTime(reader["REQ_Date"]),

                                ServiceDate =
                                    reader["Service_Date"] == DBNull.Value
                                        ? DateTime.MinValue
                                        : Convert.ToDateTime(reader["Service_Date"]),

                                Description =
                                    reader["Description"] == DBNull.Value
                                        ? ""
                                        : reader["Description"].ToString(),

                                Status = reader["REQ_Status"].ToString()
                            });
                        }
                    }
                }
            }
            return requests;
        }

        public static bool UpdateRequestStatus(
        string requestID,
        string newStatus)
        {
            if (string.IsNullOrWhiteSpace(requestID))
                return false;

            // Convert REQ001 to the numeric database ID 1.
            string numericID = requestID.Trim();

            if (numericID.StartsWith(
                "REQ",
                StringComparison.OrdinalIgnoreCase))
            {
                numericID = numericID.Substring(3);
            }

            int requestNumber;

            if (!int.TryParse(numericID, out requestNumber))
                return false;

            string query = @"
        UPDATE Service_Requests
        SET REQ_Status = @Status
        WHERE REQ_ID = @RequestID";

            using (SqlConnection connection =
                DatabaseConnection.GetConnection())
            using (SqlCommand command =
                new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue(
                    "@Status", newStatus);

                command.Parameters.AddWithValue(
                    "@RequestID", requestNumber);

                connection.Open();

                return command.ExecuteNonQuery() == 1;
            }
        }
    }
}
