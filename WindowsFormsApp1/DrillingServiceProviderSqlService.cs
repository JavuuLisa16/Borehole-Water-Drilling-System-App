using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class DrillingServiceProviderSqlService
    {
        public static List<DrillingServiceProvider> GetAllProviders()
        {
            List<DrillingServiceProvider> providers =
                new List<DrillingServiceProvider>();

            string query = @"
            SELECT
                DSP.DSP_ID,
                DSP.DSP_Name,
                DSP.DSP_Email,
                DSP.DSP_Cell,
                DSP.Address_Line,
                C.City_Name
            FROM Drilling_Service_Providers DSP
            LEFT JOIN Locations L
                ON DSP.LOC_ID = L.LOC_ID
            LEFT JOIN Cities C
                ON L.CIT_ID = C.CIT_ID
            ORDER BY DSP.DSP_ID";

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
                            providers.Add(
                                new DrillingServiceProvider
                                {
                                    DSPID =
                                        "DSP" +
                                        Convert.ToInt32(
                                            reader["DSP_ID"]
                                        ).ToString("D3"),

                                    OrganisationName =
                                        reader["DSP_Name"].ToString(),

                                    Email =
                                        reader["DSP_Email"] == DBNull.Value
                                            ? ""
                                            : reader["DSP_Email"].ToString(),

                                    Phone =
                                        reader["DSP_Cell"] == DBNull.Value
                                            ? ""
                                            : reader["DSP_Cell"].ToString(),

                                    Address =
                                        reader["Address_Line"] == DBNull.Value
                                            ? ""
                                            : reader["Address_Line"].ToString(),

                                    Location =
                                        reader["City_Name"] == DBNull.Value
                                            ? ""
                                            : reader["City_Name"].ToString()
                                }
                            );
                        }
                    }
                }
            }

            return providers;
        }

        public static DrillingServiceProvider GetProviderByID(
            string dspID)
        {
            int numericDSPID;

            if (!int.TryParse(dspID, out numericDSPID))
            {
                if (dspID.StartsWith("DSP"))
                {
                    if (!int.TryParse(
                            dspID.Substring(3),
                            out numericDSPID))
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
                DSP.DSP_ID,
                DSP.DSP_Name,
                DSP.DSP_Email,
                DSP.DSP_Cell,
                DSP.Address_Line,
                C.City_Name
            FROM Drilling_Service_Providers DSP
            LEFT JOIN Locations L
                ON DSP.LOC_ID = L.LOC_ID
            LEFT JOIN Cities C
                ON L.CIT_ID = C.CIT_ID
            WHERE DSP.DSP_ID = @DSP_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@DSP_ID",
                        numericDSPID
                    );

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        return new DrillingServiceProvider
                        {
                            DSPID =
                                "DSP" +
                                Convert.ToInt32(
                                    reader["DSP_ID"]
                                ).ToString("D3"),

                            OrganisationName =
                                reader["DSP_Name"].ToString(),

                            Email =
                                reader["DSP_Email"] == DBNull.Value
                                    ? ""
                                    : reader["DSP_Email"].ToString(),

                            Phone =
                                reader["DSP_Cell"] == DBNull.Value
                                    ? ""
                                    : reader["DSP_Cell"].ToString(),

                            Address =
                                reader["Address_Line"] == DBNull.Value
                                    ? ""
                                    : reader["Address_Line"].ToString(),

                            Location =
                                reader["City_Name"] == DBNull.Value
                                    ? ""
                                    : reader["City_Name"].ToString()
                        };
                    }
                }
            }
        }

        public static void AddProvider(
            DrillingServiceProvider provider)
        {
            int numericDSPID;

            if (!int.TryParse(provider.DSPID, out numericDSPID))
            {
                if (provider.DSPID.StartsWith("DSP"))
                {
                    if (!int.TryParse(
                            provider.DSPID.Substring(3),
                            out numericDSPID))
                    {
                        throw new Exception(
                            "Invalid DSP ID. Use a format such as DSP001."
                        );
                    }
                }
                else
                {
                    throw new Exception(
                        "Invalid DSP ID. Use a format such as DSP001."
                    );
                }
            }

            string checkQuery = @"
            SELECT COUNT(*)
            FROM Drilling_Service_Providers
            WHERE DSP_ID = @DSP_ID";

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
                                   checkQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@DSP_ID",
                                numericDSPID
                            );

                            int count =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );

                            if (count > 0)
                            {
                                throw new Exception(
                                    "A provider with this ID already exists."
                                );
                            }
                        }

                        int locationID =
                            GetOrCreateLocationID(
                                connection,
                                transaction,
                                provider.Address,
                                provider.Location
                            );

                        string insertQuery = @"
                        SET IDENTITY_INSERT
                            Drilling_Service_Providers ON;

                        INSERT INTO Drilling_Service_Providers
                        (
                            DSP_ID,
                            LOC_ID,
                            DSP_Name,
                            DSP_Email,
                            Address_Line,
                            DSP_Cell
                        )
                        VALUES
                        (
                            @DSP_ID,
                            @LOC_ID,
                            @DSP_Name,
                            @DSP_Email,
                            @Address_Line,
                            @DSP_Cell
                        );

                        SET IDENTITY_INSERT
                            Drilling_Service_Providers OFF;";

                        using (SqlCommand command =
                               new SqlCommand(
                                   insertQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@DSP_ID",
                                numericDSPID
                            );

                            command.Parameters.AddWithValue(
                                "@LOC_ID",
                                locationID
                            );

                            command.Parameters.AddWithValue(
                                "@DSP_Name",
                                provider.OrganisationName
                            );

                            command.Parameters.AddWithValue(
                                "@DSP_Email",
                                string.IsNullOrWhiteSpace(provider.Email)
                                    ? (object)DBNull.Value
                                    : provider.Email
                            );

                            command.Parameters.AddWithValue(
                                "@Address_Line",
                                string.IsNullOrWhiteSpace(provider.Address)
                                    ? (object)DBNull.Value
                                    : provider.Address
                            );

                            command.Parameters.AddWithValue(
                                "@DSP_Cell",
                                string.IsNullOrWhiteSpace(provider.Phone)
                                    ? (object)DBNull.Value
                                    : provider.Phone
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

        public static void UpdateProvider(
            string dspID,
            DrillingServiceProvider provider)
        {
            int numericDSPID;

            if (!int.TryParse(dspID, out numericDSPID))
            {
                if (dspID.StartsWith("DSP"))
                {
                    if (!int.TryParse(
                            dspID.Substring(3),
                            out numericDSPID))
                    {
                        throw new Exception("Invalid DSP ID.");
                    }
                }
                else
                {
                    throw new Exception("Invalid DSP ID.");
                }
            }

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlTransaction transaction =
                       connection.BeginTransaction())
                {
                    try
                    {
                        int locationID =
                            GetOrCreateLocationID(
                                connection,
                                transaction,
                                provider.Address,
                                provider.Location
                            );

                        string updateQuery = @"
                        UPDATE Drilling_Service_Providers
                        SET
                            LOC_ID = @LOC_ID,
                            DSP_Name = @DSP_Name,
                            DSP_Email = @DSP_Email,
                            Address_Line = @Address_Line,
                            DSP_Cell = @DSP_Cell
                        WHERE DSP_ID = @DSP_ID";

                        using (SqlCommand command =
                               new SqlCommand(
                                   updateQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@LOC_ID",
                                locationID
                            );

                            command.Parameters.AddWithValue(
                                "@DSP_Name",
                                provider.OrganisationName
                            );

                            command.Parameters.AddWithValue(
                                "@DSP_Email",
                                string.IsNullOrWhiteSpace(provider.Email)
                                    ? (object)DBNull.Value
                                    : provider.Email
                            );

                            command.Parameters.AddWithValue(
                                "@Address_Line",
                                string.IsNullOrWhiteSpace(provider.Address)
                                    ? (object)DBNull.Value
                                    : provider.Address
                            );

                            command.Parameters.AddWithValue(
                                "@DSP_Cell",
                                string.IsNullOrWhiteSpace(provider.Phone)
                                    ? (object)DBNull.Value
                                    : provider.Phone
                            );

                            command.Parameters.AddWithValue(
                                "@DSP_ID",
                                numericDSPID
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

        public static void DeleteProvider(string dspID)
        {
            int numericDSPID;

            if (!int.TryParse(dspID, out numericDSPID))
            {
                if (dspID.StartsWith("DSP"))
                {
                    if (!int.TryParse(
                            dspID.Substring(3),
                            out numericDSPID))
                    {
                        throw new Exception("Invalid DSP ID.");
                    }
                }
                else
                {
                    throw new Exception("Invalid DSP ID.");
                }
            }

            string deleteQuery = @"
            DELETE FROM Drilling_Service_Providers
            WHERE DSP_ID = @DSP_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue(
                        "@DSP_ID",
                        numericDSPID
                    );

                    int rows =
                        command.ExecuteNonQuery();

                    if (rows == 0)
                    {
                        throw new Exception(
                            "Provider could not be found."
                        );
                    }
                }
            }
        }

        private static int GetOrCreateLocationID(
            SqlConnection connection,
            SqlTransaction transaction,
            string address,
            string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                throw new Exception(
                    "Provider location is required."
                );
            }

            string findCityQuery = @"
            SELECT TOP 1 CIT_ID
            FROM Cities
            WHERE City_Name = @City_Name";

            int cityID;

            using (SqlCommand command =
                   new SqlCommand(
                       findCityQuery,
                       connection,
                       transaction))
            {
                command.Parameters.AddWithValue(
                    "@City_Name",
                    location.Trim()
                );

                object result =
                    command.ExecuteScalar();

                if (result == null)
                {
                    throw new Exception(
                        "The selected provider location could not be found in the Cities table."
                    );
                }

                cityID = Convert.ToInt32(result);
            }

            string findLocationQuery = @"
            SELECT TOP 1 LOC_ID
            FROM Locations
            WHERE CIT_ID = @CIT_ID
              AND ISNULL(Address_Line, '') =
                  ISNULL(@Address_Line, '')";

            using (SqlCommand command =
                   new SqlCommand(
                       findLocationQuery,
                       connection,
                       transaction))
            {
                command.Parameters.AddWithValue(
                    "@CIT_ID",
                    cityID
                );

                command.Parameters.AddWithValue(
                    "@Address_Line",
                    string.IsNullOrWhiteSpace(address)
                        ? (object)DBNull.Value
                        : address
                );

                object result =
                    command.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
            }

            string insertLocationQuery = @"
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
                       insertLocationQuery,
                       connection,
                       transaction))
            {
                command.Parameters.AddWithValue(
                    "@CIT_ID",
                    cityID
                );

                command.Parameters.AddWithValue(
                    "@Address_Line",
                    string.IsNullOrWhiteSpace(address)
                        ? (object)DBNull.Value
                        : address
                );

                return Convert.ToInt32(
                    command.ExecuteScalar()
                );
            }
        }
    }
}

