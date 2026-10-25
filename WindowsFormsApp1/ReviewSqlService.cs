using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ReviewSqlService
    {
        public static bool HasReview(string requestID)
        {
            if (!requestID.StartsWith("REQ") ||
                !int.TryParse(
                    requestID.Substring(3),
                    out int requestNumber))
            {
                return false;
            }

            string query = @"
            SELECT COUNT(*)
            FROM Reviews
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
                        requestNumber
                    );

                    int count =
                        Convert.ToInt32(
                            command.ExecuteScalar()
                        );

                    return count > 0;
                }
            }
        }

        public static Review CreateReview(
            string requestID,
            string clientID,
            string description,
            int rating)
        {
            if (!requestID.StartsWith("REQ") ||
                !int.TryParse(
                    requestID.Substring(3),
                    out int requestNumber))
            {
                throw new Exception(
                    "Invalid service request ID."
                );
            }

            if (!clientID.StartsWith("C") ||
                !int.TryParse(
                    clientID.Substring(1),
                    out int clientNumber))
            {
                throw new Exception(
                    "Invalid client ID."
                );
            }

            if (rating < 1 || rating > 5)
            {
                throw new Exception(
                    "Rating must be between 1 and 5."
                );
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new Exception(
                    "Please enter a review."
                );
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
                        // Make sure the request belongs to the client
                        string requestQuery = @"
                        SELECT
                            sr.REQ_Status,
                            c.CLIENT_ID
                        FROM Service_Requests sr
                        INNER JOIN Client_Drilling_Service_Providers cdsp
                            ON sr.CDSP_ID = cdsp.CDSP_ID
                        INNER JOIN Clients c
                            ON cdsp.CLIENT_ID = c.CLIENT_ID
                        WHERE sr.REQ_ID = @REQ_ID
                        AND c.CLIENT_ID = @CLIENT_ID";

                        string requestStatus = null;

                        using (SqlCommand command =
                               new SqlCommand(
                                   requestQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@REQ_ID",
                                requestNumber
                            );

                            command.Parameters.AddWithValue(
                                "@CLIENT_ID",
                                clientNumber
                            );

                            using (SqlDataReader reader =
                                   command.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    throw new Exception(
                                        "The service request could not be found for this client."
                                    );
                                }

                                requestStatus =
                                    reader["REQ_Status"].ToString();
                            }
                        }

                        if (requestStatus != "Completed")
                        {
                            throw new Exception(
                                "Only completed services can be reviewed."
                            );
                        }

                        // Prevent duplicate reviews
                        string duplicateQuery = @"
                        SELECT COUNT(*)
                        FROM Reviews
                        WHERE REQ_ID = @REQ_ID";

                        using (SqlCommand command =
                               new SqlCommand(
                                   duplicateQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@REQ_ID",
                                requestNumber
                            );

                            int count =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );

                            if (count > 0)
                            {
                                throw new Exception(
                                    "You have already submitted a review for this service."
                                );
                            }
                        }

                        string insertQuery = @"
                        INSERT INTO Reviews
                        (
                            REQ_ID,
                            CLIENT_ID,
                            Description,
                            Rating,
                            Rev_Status,
                            Rev_Date
                        )
                        OUTPUT INSERTED.Rev_ID
                        VALUES
                        (
                            @REQ_ID,
                            @CLIENT_ID,
                            @Description,
                            @Rating,
                            @Rev_Status,
                            @Rev_Date
                        )";

                        int reviewID;

                        using (SqlCommand command =
                               new SqlCommand(
                                   insertQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@REQ_ID",
                                requestNumber
                            );

                            command.Parameters.AddWithValue(
                                "@CLIENT_ID",
                                clientNumber
                            );

                            command.Parameters.AddWithValue(
                                "@Description",
                                description.Trim()
                            );

                            command.Parameters.AddWithValue(
                                "@Rating",
                                rating
                            );

                            command.Parameters.AddWithValue(
                                "@Rev_Status",
                                "Pending"
                            );

                            command.Parameters.AddWithValue(
                                "@Rev_Date",
                                DateTime.Today
                            );

                            reviewID =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );
                        }

                        transaction.Commit();

                        return new Review
                        {
                            ReviewID =
                                "REV" +
                                reviewID.ToString("D3"),

                            RequestID =
                                requestID,

                            ClientID =
                                clientID,

                            Description =
                                description.Trim(),

                            Rating =
                                rating,

                            ReviewDate =
                                DateTime.Today,

                            Status =
                                "Pending",

                            ClerkResponse =
                                null,

                            ResponseDate =
                                null
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

        public static Review GetReviewByRequestID(
            string requestID)
        {
            if (!requestID.StartsWith("REQ") ||
                !int.TryParse(
                    requestID.Substring(3),
                    out int requestNumber))
            {
                return null;
            }

            string query = @"
            SELECT
                r.Rev_ID,
                r.REQ_ID,
                r.CLIENT_ID,
                p.First_Name,
                p.Last_Name,
                r.Description,
                r.Rating,
                r.Rev_Status,
                r.Rev_Date,
                r.Clerk_Resp,
                r.Resp_Date
            FROM Reviews r
            INNER JOIN Clients c
                ON r.CLIENT_ID = c.CLIENT_ID
            INNER JOIN Persons p
                ON c.PersonID = p.PersonID
            WHERE r.REQ_ID = @REQ_ID";

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

                        return new Review
                        {
                            ReviewID =
                                "REV" +
                                Convert.ToInt32(
                                    reader["Rev_ID"]
                                ).ToString("D3"),

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

                            Description =
                                reader["Description"] == DBNull.Value
                                    ? ""
                                    : reader["Description"].ToString(),

                            Rating =
                                Convert.ToInt32(
                                    reader["Rating"]
                                ),

                            Status =
                                reader["Rev_Status"].ToString(),

                            ReviewDate =
                                Convert.ToDateTime(
                                    reader["Rev_Date"]
                                ),

                            ClerkResponse =
                                reader["Clerk_Resp"] == DBNull.Value
                                    ? null
                                    : reader["Clerk_Resp"].ToString(),

                            ResponseDate =
                                reader["Resp_Date"] == DBNull.Value
                                    ? (DateTime?)null
                                    : Convert.ToDateTime(
                                        reader["Resp_Date"]
                                    )
                        };
                    }
                }
            }
        }

        public static List<Review> GetAllReviews()
        {
            List<Review> reviews = new List<Review>();

            string query = @"
        SELECT
            r.Rev_ID,
            r.REQ_ID,
            r.Client_ID,
            p.First_Name AS ClientFirstName,
            p.Last_Name AS ClientLastName,
            r.Description,
            r.Rating,
            r.Rev_Status,
            r.Rev_Date,
            r.Clerk_Resp,
            r.Resp_Date
        FROM Reviews r
        LEFT JOIN Clients c
            ON r.Client_ID = c.CLIENT_ID
        LEFT JOIN Persons p
            ON c.PersonID = p.PersonID
        ORDER BY r.Rev_Date DESC";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                using (SqlDataReader reader =
                       command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string firstName =
                            reader["ClientFirstName"] == DBNull.Value
                                ? ""
                                : reader["ClientFirstName"].ToString();

                        string lastName =
                            reader["ClientLastName"] == DBNull.Value
                                ? ""
                                : reader["ClientLastName"].ToString();

                        reviews.Add(new Review
                        {
                            ReviewID =
                                "REV" +
                                Convert.ToInt32(
                                    reader["Rev_ID"]
                                ).ToString("D3"),

                            RequestID =
                                reader["REQ_ID"] == DBNull.Value
                                    ? ""
                                    : "REQ" +
                                      Convert.ToInt32(
                                          reader["REQ_ID"]
                                      ).ToString("D3"),

                            ClientID =
                                reader["Client_ID"] == DBNull.Value
                                    ? ""
                                    : "C" +
                                      Convert.ToInt32(
                                          reader["Client_ID"]
                                      ).ToString("D3"),

                            ClientName =
                                (firstName + " " + lastName).Trim(),

                            Description =
                                reader["Description"] == DBNull.Value
                                    ? ""
                                    : reader["Description"].ToString(),

                            Rating =
                                reader["Rating"] == DBNull.Value
                                    ? 0
                                    : Convert.ToInt32(
                                        reader["Rating"]
                                      ),

                            ReviewDate =
                                Convert.ToDateTime(
                                    reader["Rev_Date"]
                                ),

                            Status =
                                reader["Rev_Status"] == DBNull.Value
                                    ? ""
                                    : reader["Rev_Status"].ToString(),

                            ClerkResponse =
                                reader["Clerk_Resp"] == DBNull.Value
                                    ? ""
                                    : reader["Clerk_Resp"].ToString(),

                            ResponseDate =
                                reader["Resp_Date"] == DBNull.Value
                                    ? (DateTime?)null
                                    : Convert.ToDateTime(
                                        reader["Resp_Date"]
                                      )
                        });
                    }
                }
            }

            return reviews;
        }

        public static Review GetReviewByID(string reviewID)
        {
            if (string.IsNullOrWhiteSpace(reviewID))
            {
                return null;
            }

            string numericPart =
                reviewID
                    .Replace("REV", "")
                    .Trim();

            if (!int.TryParse(
                    numericPart,
                    out int reviewNumber))
            {
                return null;
            }

            string query = @"
        SELECT
            Rev_ID,
            REQ_ID,
            Client_ID,
            Description,
            Rating,
            Rev_Status,
            Rev_Date,
            Clerk_Resp,
            Resp_Date
        FROM Reviews
        WHERE Rev_ID = @Rev_ID";

            using (SqlConnection connection =
                   DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@Rev_ID",
                        reviewNumber
                    );

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        return new Review
                        {
                            ReviewID =
                                "REV" +
                                Convert.ToInt32(
                                    reader["Rev_ID"]
                                ).ToString("D3"),

                            RequestID =
                                reader["REQ_ID"] == DBNull.Value
                                    ? ""
                                    : "REQ" +
                                      Convert.ToInt32(
                                          reader["REQ_ID"]
                                      ).ToString("D3"),

                            ClientID =
                                "C" +
                                Convert.ToInt32(
                                    reader["Client_ID"]
                                ).ToString("D3"),

                            Description =
                                reader["Description"] == DBNull.Value
                                    ? ""
                                    : reader["Description"].ToString(),

                            Rating =
                                Convert.ToInt32(
                                    reader["Rating"]
                                ),

                            ReviewDate =
                                Convert.ToDateTime(
                                    reader["Rev_Date"]
                                ),

                            Status =
                                reader["Rev_Status"].ToString(),

                            ClerkResponse =
                                reader["Clerk_Resp"] == DBNull.Value
                                    ? ""
                                    : reader["Clerk_Resp"].ToString(),

                            ResponseDate =
                                reader["Resp_Date"] == DBNull.Value
                                    ? (DateTime?)null
                                    : Convert.ToDateTime(
                                        reader["Resp_Date"]
                                    )
                        };
                    }
                }
            }
        }
    }
}