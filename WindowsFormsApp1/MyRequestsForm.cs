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
    public partial class MyRequestsForm : Form
    {
        private string requestID;
        private Form previousForm;

        public MyRequestsForm()
        {
            InitializeComponent();

            SetupRequestColumns();
            LoadRequests();

            AppTheme.ApplyToForm(this);
        }

        private void MyRequestsForm_Load(
            object sender,
            EventArgs e)
        {
            LoadRequests();
        }

        private void MyRequestsForm_Activated(
            object sender,
            EventArgs e)
        {
            LoadRequests();
        }

        private void LoadRequests()
        {
            dgvRequests.Rows.Clear();

            try
            {
                List<ServiceRequest> requests =
                    ServiceRequestSqlService.GetRequestsByPersonID(
                        CurrentUser.PersonID
                    );

                foreach (ServiceRequest request in requests)
                {
                    dgvRequests.Rows.Add(
                        request.RequestID,
                        request.ClientID,
                        request.ClientName,
                        request.Service,
                        request.Provider,
                        request.Addressline1,
                        request.Addressline2,
                        request.City,
                        request.Province,
                        request.RequestDate.ToShortDateString(),
                        request.ServiceDate == DateTime.MinValue
                            ? ""
                            : request.ServiceDate.ToShortDateString(),
                        request.Description,
                        request.Status
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load your service requests.\n\n" +
                    ex.Message,
                    "Database Error"
                );
            }
        }

        private void btnBack_Click(
            object sender,
            EventArgs e)
        {
            var ownerClient =
                this.Owner as ClientForm;

            if (ownerClient != null)
            {
                ownerClient.Show();
                ownerClient.BringToFront();
            }
            else
            {
                var openClient =
                    Application.OpenForms
                    .OfType<ClientForm>()
                    .FirstOrDefault();

                if (openClient != null)
                {
                    openClient.Show();
                    openClient.BringToFront();
                }
                else
                {
                    var newClient =
                        new ClientForm();

                    newClient.Show();
                }
            }

            this.Hide();
        }

        private void btnPay_Click(
            object sender,
            EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select an approved request."
                );
                return;
            }

            string selectedRequestID =
                dgvRequests.SelectedRows[0]
                .Cells["RequestID"]
                .Value?.ToString();

            if (string.IsNullOrWhiteSpace(
                selectedRequestID))
            {
                MessageBox.Show(
                    "Service request could not be found."
                );
                return;
            }

            string status =
                dgvRequests.SelectedRows[0]
                .Cells["Status"]
                .Value?.ToString();

            if (status != "Approved")
            {
                MessageBox.Show(
                    "Payment is only available after the request has been approved.",
                    "Payment Not Available"
                );
                return;
            }

            try
            {
                if (PaymentSqlService.HasPayment(
                    selectedRequestID))
                {
                    MessageBox.Show(
                        "This request has already been paid.",
                        "Payment Complete"
                    );
                    return;
                }

                PaymentForm paymentForm =
                    new PaymentForm(
                        selectedRequestID,
                        this
                    );

                paymentForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not check the payment status.\n\n" +
                    ex.Message,
                    "Database Error"
                );
            }
        }

        private void btnReview_Click(
            object sender,
            EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a service request to review."
                );
                return;
            }

            string selectedRequestID =
                dgvRequests.SelectedRows[0]
                .Cells["RequestID"]
                .Value?.ToString();

            if (string.IsNullOrWhiteSpace(
                selectedRequestID))
            {
                MessageBox.Show(
                    "Service request could not be found."
                );
                return;
            }

            string status =
                dgvRequests.SelectedRows[0]
                .Cells["Status"]
                .Value?.ToString();

            if (status != "Completed")
            {
                MessageBox.Show(
                    "You can only review a completed service."
                );
                return;
            }

            if (ReviewSqlService.GetReviewByRequestID(selectedRequestID) != null)
            {
                MessageBox.Show(
                    "You have already submitted a review for this service."
                );
                return;
            }

            ReviewsForm reviewsForm =
                new ReviewsForm(
                    selectedRequestID
                );

            reviewsForm.FormClosed +=
                (s, args) =>
                {
                    this.Show();
                    LoadRequests();
                };

            reviewsForm.Show();
            this.Hide();
        }

        private void SetupRequestColumns()
        {
            dgvRequests.Columns.Clear();

            dgvRequests.AutoGenerateColumns = false;

            dgvRequests.Columns.Add(
                "RequestID",
                "Request ID"
            );

            dgvRequests.Columns.Add(
                "ClientID",
                "Client ID"
            );

            dgvRequests.Columns.Add(
                "ClientName",
                "Client Name"
            );

            dgvRequests.Columns.Add(
                "Service",
                "Service"
            );

            dgvRequests.Columns.Add(
                "Provider",
                "Provider"
            );

            dgvRequests.Columns.Add(
                "Addressline1",
                "Address Line 1"
            );

            dgvRequests.Columns.Add(
                "Addressline2",
                "Address Line 2"
            );

            dgvRequests.Columns.Add(
                "City",
                "City"
            );

            dgvRequests.Columns.Add(
                "Province",
                "Province"
            );

            dgvRequests.Columns.Add(
                "RequestDate",
                "Request Date"
            );

            dgvRequests.Columns.Add(
                "ServiceDate",
                "Service Date"
            );

            dgvRequests.Columns.Add(
                "Description",
                "Description"
            );

            dgvRequests.Columns.Add(
                "Status",
                "Status"
            );
        }

        private void btnCancelRequest_Click(
            object sender,
            EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a service request to cancel."
                );
                return;
            }

            string selectedRequestID =
                dgvRequests.SelectedRows[0]
                .Cells["RequestID"]
                .Value?.ToString();

            if (string.IsNullOrWhiteSpace(
                selectedRequestID))
            {
                MessageBox.Show(
                    "Service request could not be found."
                );
                return;
            }

            if (!selectedRequestID.StartsWith("REQ"))
            {
                MessageBox.Show(
                    "Invalid service request ID."
                );
                return;
            }

            if (!int.TryParse(
                selectedRequestID.Substring(3),
                out int requestNumber))
            {
                MessageBox.Show(
                    "Invalid service request ID."
                );
                return;
            }

            DialogResult result =
                MessageBox.Show(
                    "Are you sure you want to cancel this service request?",
                    "Cancel Request",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                bool cancelled =
                    ServiceRequestSqlService.CancelRequest(
                        requestNumber
                    );

                if (!cancelled)
                {
                    MessageBox.Show(
                        "This service request cannot be cancelled.\n\n" +
                        "It may already be completed or cancelled.",
                        "Cancellation Not Allowed"
                    );

                    return;
                }

                MessageBox.Show(
                    "Service request cancelled successfully.",
                    "Request Cancelled"
                );

                LoadRequests();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not cancel the service request.\n\n" +
                    ex.Message,
                    "Database Error"
                );
            }
        }
    }
}
