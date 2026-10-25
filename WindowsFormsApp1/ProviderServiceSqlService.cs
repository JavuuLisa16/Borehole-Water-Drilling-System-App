using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ProviderServiceSqlService
    {
        public class ProviderInfoSqlRecord
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string Location { get; set; }
        }
        public static List<string> GetProviders()
        {
            List<string> providers = new List<string>();

            string query = @"
            SELECT DISTINCT DSP.DSP_Name
            FROM Drilling_Service_Providers DSP
            INNER JOIN Drilling_Providers_Services DPS
                ON DSP.DSP_ID = DPS.DSP_ID
            ORDER BY DSP.DSP_Name";

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
                                reader["DSP_Name"].ToString()
                            );
                        }
                    }
                }
            }

            return providers;
        }

        public static List<string> GetServicesForProvider(
            string providerName)
        {
            List<string> services =
                new List<string>();

            string query = @"
            SELECT DISTINCT S.Serv_Name
            FROM Drilling_Providers_Services DPS
            INNER JOIN Drilling_Service_Providers DSP
                ON DPS.DSP_ID = DSP.DSP_ID
            INNER JOIN Services S
                ON DPS.SERV_ID = S.SERV_ID
            WHERE DSP.DSP_Name = @ProviderName
            ORDER BY S.Serv_Name";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@ProviderName",
                        providerName
                    );

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            services.Add(
                                reader["Serv_Name"].ToString()
                            );
                        }
                    }
                }
            }

            return services;
        }

        public static decimal GetServicePrice(
            string providerName,
            string serviceName)
        {
            string query = @"
            SELECT DPS.Costs
            FROM Drilling_Providers_Services DPS
            INNER JOIN Drilling_Service_Providers DSP
                ON DPS.DSP_ID = DSP.DSP_ID
            INNER JOIN Services S
                ON DPS.SERV_ID = S.SERV_ID
            WHERE DSP.DSP_Name = @ProviderName
            AND S.Serv_Name = @ServiceName";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@ProviderName",
                        providerName
                    );

                    command.Parameters.AddWithValue(
                        "@ServiceName",
                        serviceName
                    );

                    object result =
                        command.ExecuteScalar();

                    if (result == null)
                    {
                        throw new Exception(
                            "Price information could not be found for the selected provider and service."
                        );
                    }

                    return Convert.ToDecimal(result);
                }
            }
        }
    
    public static ProviderInfoSqlRecord GetProviderInfo(
    string providerName)
        {
            string query = @"
        SELECT
            DSP.DSP_Name,
            DSP.DSP_Email,
            DSP.DSP_Cell,
            DSP.Address_Line,
            C.City_Name,
            P.Prov_Name
        FROM Drilling_Service_Providers DSP
        LEFT JOIN Locations L
            ON DSP.LOC_ID = L.LOC_ID
        LEFT JOIN Cities C
            ON L.CIT_ID = C.CIT_ID
        LEFT JOIN Provinces P
            ON C.PROV_ID = P.PROV_ID
        WHERE DSP.DSP_Name = @DSP_Name";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@DSP_Name",
                        providerName
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

                        return new ProviderInfoSqlRecord
                        {
                            Name =
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
                                location
                        };
                    }
                }
            }
        }
    }
}