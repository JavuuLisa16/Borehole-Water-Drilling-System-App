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
    public partial class ClerkServiceRequestsForm : Form
    {
        private void LoadRequests()
        {
            dgvRequests.Rows.Clear();

            foreach (ServiceRequest request in
         ServiceRequestSqlService.GetAllRequests())
            {
                ClientProfile client = ClientSqlService.GetClientByID(request.ClientID);

                string clientName = "Unknown Client";

                if (client != null)
                {
                    clientName = client.FullName;
                }
                int rowIndex =
                dgvRequests.Rows.Add(
 request.ClientID,
 request.RequestID,
 clientName,
 request.Service,
 request.Provider,
 request.Addressline1,
 request.Addressline2,
 request.City,
 request.Province,
 request.RequestDate.ToShortDateString(),
 request.ServiceDate.ToShortDateString(),
 request.Description,
 request.Status
             );
                dgvRequests.Rows[rowIndex].Tag = request.RequestID;
            }
        }
        public ClerkServiceRequestsForm()
        {
            InitializeComponent();
            LoadRequests();
            AppTheme.ApplyToForm(this);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a service request.");
                return;
            }

            if (cmbRequestStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a new status.");
                return;
            }

            string requestID =
    dgvRequests.SelectedRows[0]
    .Cells["RequestID"]
    .Value?.ToString()
    .Trim();

            if (string.IsNullOrWhiteSpace(requestID))
            {
                MessageBox.Show(
                    "Request ID could not be identified.",
                    "Error"
                );
                return;
            }

            string newStatus =
                cmbRequestStatus.SelectedItem.ToString();

            ServiceRequest request =
    ServiceRequestSqlService.GetRequestByRequestID(
        requestID
    );

            if (request == null)
            {
                MessageBox.Show(
                    "Service request could not be found.\n\n" +
                    "Request ID: " + requestID,
                    "Error"
                );
                return;
            }

            // Only allow approval after a suitable site assessment
            if (newStatus == "Approved")
            {
                SiteAssessment assessment =
                    SiteAssessmentSqlService.GetAssessmentByRequestID(requestID);

                if (assessment == null)
                {
                    MessageBox.Show(
                        "This request cannot be approved because a site assessment has not been completed.",
                        "Approval Not Allowed"
                    );

                    return;
                }

                if (assessment.Suitability != "Suitable")
                {
                    MessageBox.Show(
                        "This request cannot be approved because the site has not been marked as suitable.",
                        "Approval Not Allowed"
                    );

                    return;
                }
            }

            bool updated =
     ServiceRequestSqlService.UpdateRequestStatus(
         requestID,
         newStatus
     );

            if (!updated)
            {
                MessageBox.Show(
                    "The request status could not be updated in the database.",
                    "Update Failed"
                );

                return;
            }

            LoadRequests();

            MessageBox.Show(
                "Request status updated to: " + newStatus,
                "Status Updated"
            );

        }





        private void ClerkServiceRequestsForm_Activated_1(object sender, EventArgs e)
        {
            LoadRequests();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRequests();

            MessageBox.Show(
                "Service requests refreshed.",
                "Refresh"
            );
        }

       
    }
}

       
