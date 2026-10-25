using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class PaymentSqlService
    {
        public static Payment ProcessPayment(
       string requestID,
       string clientID,
       decimal amount,
       string paymentMethod,
       string transactionReference)
        {
            if (!requestID.StartsWith("REQ") ||
                !int.TryParse(requestID.Substring(3), out int reqID))
            {
                throw new Exception("Invalid request ID.");
            }

            if (!clientID.StartsWith("C") ||
                !int.TryParse(clientID.Substring(1), out int clientIDNumber))
            {
                throw new Exception("Invalid client ID.");
            }

            if (amount <= 0)
            {
                throw new Exception("Payment amount must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(paymentMethod))
            {
                throw new Exception("Please select a payment method.");
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
                        // Check the service request
                        string requestQuery = @"
                        SELECT REQ_Status
                        FROM Service_Requests
                        WHERE REQ_ID = @REQ_ID";

                        string requestStatus;

                        using (SqlCommand command =
                               new SqlCommand(
                                   requestQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@REQ_ID",
                                reqID
                            );

                            object result =
                                command.ExecuteScalar();

                            if (result == null)
                            {
                                throw new Exception(
                                    "Service request could not be found."
                                );
                            }

                            requestStatus = result.ToString();
                        }

                        if (requestStatus != "Approved")
                        {
                            throw new Exception(
                                "Payment can only be made for an approved service request."
                            );
                        }

                        // Prevent duplicate payment
                        string duplicateQuery = @"
                        SELECT COUNT(*)
                        FROM Payments
                        WHERE REQ_ID = @REQ_ID";

                        using (SqlCommand command =
                               new SqlCommand(
                                   duplicateQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@REQ_ID",
                                reqID
                            );

                            int paymentCount =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );

                            if (paymentCount > 0)
                            {
                                throw new Exception(
                                    "A payment has already been recorded for this service request."
                                );
                            }
                        }

                        // Generate transaction reference if none was supplied
                        if (string.IsNullOrWhiteSpace(transactionReference))
                        {
                            transactionReference =
                                "TRX" +
                                DateTime.Now.ToString("yyyyMMddHHmmss");
                        }

                        // Insert payment
                        string paymentQuery = @"
                        INSERT INTO Payments
                        (
                            REQ_ID,
                            CLIENT_ID,
                            Amount,
                            Payment_Date,
                            Payment_Status,
                            Payment_Method,
                            Transaction_Ref
                        )
                        OUTPUT INSERTED.PAYMENT_ID
                        VALUES
                        (
                            @REQ_ID,
                            @CLIENT_ID,
                            @Amount,
                            @Payment_Date,
                            @Payment_Status,
                            @Payment_Method,
                            @Transaction_Ref
                        )";

                        int paymentID;

                        using (SqlCommand command =
                               new SqlCommand(
                                   paymentQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@REQ_ID",
                                reqID
                            );

                            command.Parameters.AddWithValue(
                                "@CLIENT_ID",
                                clientIDNumber
                            );

                            command.Parameters.AddWithValue(
                                "@Amount",
                                amount
                            );

                            command.Parameters.AddWithValue(
                                "@Payment_Date",
                                DateTime.Today
                            );

                            command.Parameters.AddWithValue(
                                "@Payment_Status",
                                "Paid"
                            );

                            command.Parameters.AddWithValue(
                                "@Payment_Method",
                                paymentMethod
                            );

                            command.Parameters.AddWithValue(
                                "@Transaction_Ref",
                                transactionReference
                            );

                            paymentID =
                                Convert.ToInt32(
                                    command.ExecuteScalar()
                                );
                        }

                        // Update service request status
                        string updateRequestQuery = @"
                        UPDATE Service_Requests
                        SET REQ_Status = 'Paid'
                        WHERE REQ_ID = @REQ_ID
                        AND REQ_Status = 'Approved'";

                        using (SqlCommand command =
                               new SqlCommand(
                                   updateRequestQuery,
                                   connection,
                                   transaction))
                        {
                            command.Parameters.AddWithValue(
                                "@REQ_ID",
                                reqID
                            );

                            int rowsAffected =
                                command.ExecuteNonQuery();

                            if (rowsAffected == 0)
                            {
                                throw new Exception(
                                    "The service request could not be updated to Paid."
                                );
                            }
                        }

                        transaction.Commit();

                        return new Payment
                        {
                            PaymentID =
                                "PAY" +
                                paymentID.ToString("D3"),

                            RequestID =
                                requestID,

                            ClientID =
                                clientID,

                            Amount =
                                amount,

                            PaymentMethod =
                                paymentMethod,

                            PaymentStatus =
                                "Paid",

                            TransactionReference =
                                transactionReference,

                            PaymentDate =
                                DateTime.Today
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

        public static bool HasPayment(string requestID)
        {
            if (!requestID.StartsWith("REQ") ||
                !int.TryParse(requestID.Substring(3), out int reqID))
            {
                return false;
            }

            string query = @"
        SELECT COUNT(*)
        FROM Payments
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
                        reqID
                    );

                    int count =
                        Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }
    }
}
