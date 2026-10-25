using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class LocationSqlService
    {
        public static List<string> GetProvinces()
        {
            List<string> provinces =
                new List<string>();

            string query = @"
            SELECT Prov_Name
            FROM Provinces
            ORDER BY Prov_Name";

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
                            provinces.Add(
                                reader["Prov_Name"].ToString()
                            );
                        }
                    }
                }
            }

            return provinces;
        }

        public static List<string> GetCitiesByProvince(
            string provinceName)
        {
            List<string> cities =
                new List<string>();

            string query = @"
            SELECT C.City_Name
            FROM Cities C
            INNER JOIN Provinces P
                ON C.PROV_ID = P.PROV_ID
            WHERE P.Prov_Name = @ProvinceName
            ORDER BY C.City_Name";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@ProvinceName",
                        provinceName
                    );

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cities.Add(
                                reader["City_Name"].ToString()
                            );
                        }
                    }
                }
            }

            return cities;
        }
    }
}
