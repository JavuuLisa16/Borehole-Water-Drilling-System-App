using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class PaymentForm : Form
    {
        private string requestID;
        private Form previousForm;
        public PaymentForm(string requestID, Form previousForm)
        {
            InitializeComponent();
            this.requestID = requestID;
            this.previousForm = previousForm;
        }
        public PaymentForm()
        {
            InitializeComponent();
            AppTheme.ApplyToForm(this);
            StylePaymentForm();

            // Parameterless ctor: leave fields unset or initialize defaults
            this.requestID = string.Empty;
            this.previousForm = null;

        }
        private void StylePaymentForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            ServiceRequest request;

            try
            {
                request =
                    ServiceRequestSqlService.GetRequestByRequestID(requestID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load the service request.\n\n" +
                    ex.Message,
                    "Database Error"
                );

                return;
            }

            if (request == null)
            {
                MessageBox.Show(
                    "Service request could not be found."
                );

                return;
            }

            lblRequestID.Text =
                "Request ID: " + request.RequestID;

            lblClientID.Text =
                "Client ID: " + request.ClientID;

            lblService.Text =
                "Service: " + request.Service;

            lblProvider.Text =
                "Provider: " + request.Provider;

            // Get the service price directly from SQL
            decimal price;

            try
            {
                price =
                    ProviderServiceSqlService.GetServicePrice(
                        request.Provider,
                        request.Service
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load the service price.\n\n" +
                    ex.Message,
                    "Database Error"
                );

                return;
            }

            if (price <= 0)
            {
                MessageBox.Show(
                    "Price information could not be found."
                );

                return;
            }

            decimal vat = price * 0.15m;
            decimal total = price + vat;

            lblPrice.Text =
                "Price: R" + price.ToString("N2");

            lblVAT.Text =
                "VAT (15%): R" + vat.ToString("N2");

            lblTotal.Text =
                "Total: R" + total.ToString("N2");

            cmbPaymentMethod.Items.Clear();

            cmbPaymentMethod.Items.Add("Card");
            cmbPaymentMethod.Items.Add("EFT");
            cmbPaymentMethod.Items.Add("Instant EFT");

            cmbPaymentMethod.SelectedIndex = -1;

            txtTransactionReference.Clear();
        }
        private void btnPayNow_Click(object sender, EventArgs e)
        {
            if (cmbPaymentMethod.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Please select a payment method."
                );

                cmbPaymentMethod.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(
                txtTransactionReference.Text))
            {
                MessageBox.Show(
                    "Please enter the transaction reference."
                );

                txtTransactionReference.Focus();
                return;
            }

            ServiceRequest request;

            try
            {
                request =
                    ServiceRequestSqlService.GetRequestByRequestID(
                        requestID
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load the service request.\n\n" +
                    ex.Message,
                    "Database Error"
                );

                return;
            }

            if (request == null)
            {
                MessageBox.Show(
                    "Service request could not be found."
                );

                return;
            }

            decimal price;

            try
            {
                price =
                    ProviderServiceSqlService.GetServicePrice(
                        request.Provider,
                        request.Service
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Price Information"
                );

                return;
            }

            decimal vat = price * 0.15m;
            decimal total = price + vat;

            try
            {
                Payment payment =
                    PaymentSqlService.ProcessPayment(
                        request.RequestID,
                        request.ClientID,
                        total,
                        cmbPaymentMethod.SelectedItem.ToString(),
                        txtTransactionReference.Text.Trim()
                    );

                MessageBox.Show(
                    "Payment successful!\n\n" +
                    "Payment ID: " + payment.PaymentID + "\n" +
                    "Request ID: " + payment.RequestID + "\n" +
                    "Amount: R" +
                    payment.Amount.ToString("N2") + "\n" +
                    "Payment Method: " +
                    payment.PaymentMethod + "\n" +
                    "Status: " +
                    payment.PaymentStatus,
                    "Payment Successful"
                );

                if (previousForm != null)
                {
                    previousForm.Show();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Payment could not be processed.\n\n" +
                    ex.Message,
                    "Payment Error"
                );
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (previousForm != null)
            {
                previousForm.Show();
            }

            this.Close();
        }
    }
}
