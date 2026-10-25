using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace WindowsFormsApp1
{
    public partial class OrganisationTechnicianForm : Form
    {
        private void LoadServiceRequests()
        {
            dgvServiceRequests.Rows.Clear();

            if (cmbProvider.SelectedItem == null)
                return;

            string selectedProvider =
                cmbProvider.SelectedItem.ToString();

            foreach (ServiceRequest request in
         ServiceRequestSqlService.GetAllRequests())
            {
                if (request.Provider == selectedProvider &&
                    request.Status == "Pending")
                {
                    ClientProfile client =
                        ClientSqlService.GetClientByID(request.ClientID);

                    string clientName = "Unknown Client";

                    if (client != null)
                    {
                        clientName = client.FullName;
                    }

                    dgvServiceRequests.Rows.Add(
    request.RequestID,
    request.ClientID,
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
                }
            }
        }
        private void LoadTechnicians()
        {
            dgvTechnician.Rows.Clear();

            if (cmbProvider.SelectedItem == null)
                return;

            string selectedProvider =
                cmbProvider.SelectedItem.ToString();

            foreach (Technician technician
         in TechnicianSqlService.GetAllTechnicians())
            {
                if (technician.Provider == selectedProvider)
                {
                    dgvTechnician.Rows.Add(
                        technician.TechnicianID,
                        technician.TechnicianName,
                        technician.Phone,
                        technician.Specialisation,
                        technician.Status
                    );
                }
            }
        }
        public OrganisationTechnicianForm()
        {
            InitializeComponent();
            LoadServiceRequests();
            StyleOrganisationTechnicianForm();




        }
        private void StyleOrganisationTechnicianForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Information refreshed.");
        }

        private void btnViewRequest_Click(object sender, EventArgs e)
        {
            if (dgvServiceRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an approved service request.");
                return;
            }

            DataGridViewRow row = dgvServiceRequests.SelectedRows[0];

            string requestID = row.Cells["RequestID"].Value?.ToString();
            string clientID = row.Cells["ClientID"].Value?.ToString();
            string clientName = row.Cells["ClientName"].Value?.ToString();
            string service = row.Cells["Service"].Value?.ToString();
            string provider = row.Cells["Provider"].Value?.ToString();
            string addressLine1 = row.Cells["Addressline1"].Value?.ToString();
            string addressLine2 = row.Cells["Addressline2"].Value?.ToString();
            string city = row.Cells["City"].Value?.ToString();
            string province = row.Cells["Province"].Value?.ToString();
            string requestDate = row.Cells["RequestDate"].Value?.ToString();
            string serviceDate = row.Cells["ServiceDate"].Value?.ToString();
            string description = row.Cells["Description"].Value?.ToString();
            string status = row.Cells["Status"].Value?.ToString();

            ServiceRequest request =
    ServiceRequestSqlService.GetRequestByRequestID(
        requestID
    );

            if (request == null)
            {
                MessageBox.Show("Service request could not be found.");
                return;
            }



            MessageBox.Show(
        "Request ID: " + requestID +
        "\nClient ID: " + clientID +
        "\nClient: " + clientName +
        "\nService: " + service +
        "\nProvider: " + provider +
        "\nAddress Line 1: " + addressLine1 +
        "\nAddress Line 2: " + addressLine2 +
        "\nCity: " + city +
        "\nProvince: " + province +
        "\nRequest Date: " + requestDate +
        "\nService Date: " + serviceDate +
        "\nDescription: " + description +
        "\nStatus: " + status,
        "Service Request Details");
        }




        private void btnAssignTechnician_Click(object sender, EventArgs e)
        {
            if (dgvServiceRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a pending service request."
                );
                return;
            }

            if (dgvTechnician.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a technician."
                );
                return;
            }

            string requestID =
                dgvServiceRequests.SelectedRows[0]
                .Cells["RequestID"]
                .Value?.ToString();

            string technicianID =
                dgvTechnician.SelectedRows[0]
                .Cells["TechnicianID"]
                .Value?.ToString();

            if (string.IsNullOrWhiteSpace(requestID))
            {
                MessageBox.Show(
                    "Request ID could not be found."
                );
                return;
            }

            if (string.IsNullOrWhiteSpace(technicianID))
            {
                MessageBox.Show(
                    "Technician ID could not be found."
                );
                return;
            }

            // Validate Request ID
            if (!requestID.StartsWith("REQ") ||
                !int.TryParse(
                    requestID.Substring(3),
                    out int requestNumber))
            {
                MessageBox.Show(
                    "Invalid service request ID."
                );
                return;
            }

            // Validate Technician ID
            int technicianNumber;

            if (technicianID.StartsWith(
                    "TECH",
                    StringComparison.OrdinalIgnoreCase))
            {
                if (!int.TryParse(
                        technicianID.Substring(4),
                        out technicianNumber))
                {
                    MessageBox.Show(
                        "Invalid technician ID."
                    );
                    return;
                }
            }
            else if (!int.TryParse(
                         technicianID,
                         out technicianNumber))
            {
                MessageBox.Show(
                    "Invalid technician ID."
                );
                return;
            }

            try
            {
                // Save assignment directly to SQL
                int assignmentID =
                    TechnicianAssignmentSqlService.AssignTechnician(
                        requestNumber,
                        technicianNumber
                    );

                MessageBox.Show(
                    "Technician assigned successfully.\n\n" +
                    "Assignment ID: ASS" +
                    assignmentID.ToString("D3"),
                    "Assignment Successful"
                );

                // Refresh the grids
                LoadServiceRequests();
                LoadTechnicians();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Technician could not be assigned.\n\n" +
                    ex.Message,
                    "Assignment Error"
                );
            }
        }


        private void OrganisationTechnicianForm_Activated(object sender, EventArgs e)
        {
            LoadServiceRequests();
            LoadTechnicians();
        }

        private void cmbProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadServiceRequests();
            LoadTechnicians();
        }

        private void OrganisationTechnicianForm_Load(object sender, EventArgs e)
        {
            ApplyFormStyle();
            LoadServiceRequests();
            SetupServiceRequestColumns();
            StyleServiceRequestGrid();
        }

        private void SetupServiceRequestColumns()
        {
            dgvServiceRequests.Columns.Clear();
            dgvServiceRequests.AutoGenerateColumns = false;

            dgvServiceRequests.Columns.Add("RequestID", "Request ID");
            dgvServiceRequests.Columns.Add("ClientID", "Client ID");
            dgvServiceRequests.Columns.Add("ClientName", "Client Name");
            dgvServiceRequests.Columns.Add("Service", "Service");
            dgvServiceRequests.Columns.Add("Provider", "Provider");
            dgvServiceRequests.Columns.Add("Addressline1", "Address Line 1");
            dgvServiceRequests.Columns.Add("Addressline2", "Address Line 2");
            dgvServiceRequests.Columns.Add("City", "City");
            dgvServiceRequests.Columns.Add("Province", "Province");
            dgvServiceRequests.Columns.Add("RequestDate", "Request Date");
            dgvServiceRequests.Columns.Add("ServiceDate", "Service Date");
            dgvServiceRequests.Columns.Add("Description", "Description");
            dgvServiceRequests.Columns.Add("Status", "Status");
        }

        private void ApplyFormStyle()
        {
            this.BackColor = AppTheme.Background;

            btnViewRequest.BackColor = AppTheme.Accent;
            btnViewRequest.ForeColor = AppTheme.White;
            btnViewRequest.FlatStyle = FlatStyle.Flat;
            btnViewRequest.FlatAppearance.BorderSize = 0;
            btnViewRequest.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        }
        private void StyleServiceRequestGrid()
        {
            dgvServiceRequests.BackgroundColor = Color.White;
            dgvServiceRequests.BorderStyle = BorderStyle.None;

            dgvServiceRequests.EnableHeadersVisualStyles = false;

            dgvServiceRequests.ColumnHeadersDefaultCellStyle.BackColor =
                AppTheme.Primary;

            dgvServiceRequests.ColumnHeadersDefaultCellStyle.ForeColor =
                AppTheme.White;

            dgvServiceRequests.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgvServiceRequests.DefaultCellStyle.Font =
                new Font("Segoe UI", 9);

            dgvServiceRequests.DefaultCellStyle.SelectionBackColor =
                AppTheme.Accent;

            dgvServiceRequests.DefaultCellStyle.SelectionForeColor =
                AppTheme.White;

            dgvServiceRequests.RowTemplate.Height = 35;

            dgvServiceRequests.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(245, 247, 250);
        }

    }
}