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
    public partial class TechnicianAssignments : Form
    {
        public TechnicianAssignments()
        {
            InitializeComponent();
            LoadAssignments();
            AppTheme.ApplyToForm(this);
            StyleTechnicianAssignmentsForm();
        }
        private void StyleTechnicianAssignmentsForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void LoadAssignments()
        {
            dgvAssignment.Rows.Clear();

            foreach (Assignment assignment in
         TechnicianAssignmentSqlService.GetAllAssignments())
            {
                dgvAssignment.Rows.Add(
                    assignment.AssignmentID,
                    assignment.RequestID,
                    assignment.TechnicianID,
                    assignment.AssignmentDate.ToShortDateString(),
                    assignment.Status
                );
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Refresh assignments when the form becomes active
        private void TechnicianAssignmentForm_Activated(object sender, EventArgs e)
        {
            LoadAssignments();
        }

        private void btnSiteVisit_Click(object sender, EventArgs e)
        {
            if (dgvAssignment.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an assignment.");
                return;
            }

            string requestID =
                dgvAssignment.SelectedRows[0]
                .Cells["RequestID"].Value?.ToString();

            string technicianID =
                dgvAssignment.SelectedRows[0]
                .Cells["TechnicianID"].Value?.ToString();

            if (string.IsNullOrWhiteSpace(requestID))
            {
                MessageBox.Show("Request ID could not be found.");
                return;
            }

            if (string.IsNullOrWhiteSpace(technicianID))
            {
                MessageBox.Show("Technician ID could not be found.");
                return;
            }

            SiteAssessmentForm assessmentForm =
                new SiteAssessmentForm(requestID, technicianID, this);

            assessmentForm.Show();
            this.Hide();
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvAssignment.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an assignment.");
                return;
            }

            if (cmbAssignmentStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a status.");
                return;
            }

            string assignmentID =
                dgvAssignment.SelectedRows[0]
                .Cells["AssignmentID"]
                .Value?.ToString();
            Assignment assignment =
    TechnicianAssignmentSqlService.GetAssignmentByID(
        assignmentID
    );

            if (assignment == null)
            {
                MessageBox.Show("Assignment could not be found.");
                return;
            }

            string currentStatus = assignment.Status;
            string newStatus = cmbAssignmentStatus.SelectedItem.ToString();

            // Prevent changing a completed assignment
            if (currentStatus == "Completed")
            {
                MessageBox.Show(
                    "This assignment is already completed and cannot be changed."
                );

                cmbAssignmentStatus.SelectedItem = currentStatus;
                return;
            }

            // Prevent invalid status jumps
            bool validChange = false;

            if (currentStatus == "Assigned")
            {
                if (newStatus == "In Progress" ||
                    newStatus == "On Hold")
                {
                    validChange = true;
                }
            }
            else if (currentStatus == "In Progress")
            {
                if (newStatus == "Completed" ||
                    newStatus == "On Hold")
                {
                    validChange = true;
                }
            }
            else if (currentStatus == "On Hold")
            {
                if (newStatus == "In Progress")
                {
                    validChange = true;
                }
            }

            // Allow keeping the same status
            if (newStatus == currentStatus)
            {
                validChange = true;
            }

            if (!validChange)
            {
                MessageBox.Show(
                    "Invalid status change.\n\n" +
                    "Allowed flow:\n" +
                    "Assigned → In Progress → Completed\n" +
                    "In Progress ↔️ On Hold"
                );

                cmbAssignmentStatus.SelectedItem = currentStatus;
                return;
            }

            // Update assignment
            assignment.Status = newStatus;

            // Update technician status
            Technician technician =
                TechnicianSqlService.GetTechnicianByID(
                    assignment.TechnicianID
                );

            if (technician != null)
            {
                if (newStatus == "Completed")
                {
                    technician.Status = "Available";
                }
                else
                {
                    technician.Status = "Busy";
                }
            }

            // Update the related service request
            ServiceRequest request =
    ServiceRequestSqlService.GetRequestByRequestID(
        assignment.RequestID
    );

            if (request != null)
            {
                if (newStatus == "In Progress")
                {
                    request.Status = "In Progress";
                }
                else if (newStatus == "Completed")
                {
                    request.Status = "Completed";
                }
            }

            LoadAssignments();

            cmbAssignmentStatus.SelectedItem = newStatus;

            MessageBox.Show(
                "Assignment status updated successfully.",
                "Status Updated"
            );

        }

        private void dgvAssignment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
        return;

            string status =
                dgvAssignment.Rows[e.RowIndex]
                .Cells["Status"].Value?.ToString();

            if (!string.IsNullOrEmpty(status))
            {
                cmbAssignmentStatus.SelectedItem = status;
            }
        }

        private void btnSitev_Click(object sender, EventArgs e)
        {
            if (dgvAssignment.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an assignment.");
                return;
            }

            string assignmentID =
                dgvAssignment.SelectedRows[0]
                .Cells["AssignmentID"]
                .Value?.ToString();

            if (string.IsNullOrWhiteSpace(assignmentID))
            {
                MessageBox.Show("Assignment ID could not be found.");
                return;
            }

            Assignment assignment =
    TechnicianAssignmentSqlService.GetAssignmentByID(
        assignmentID
    );
            if (assignment == null)
            {
                MessageBox.Show("Assignment could not be found.");
                return;
            }

            ServiceRequest request =
    ServiceRequestSqlService.GetRequestByRequestID(
        assignment.RequestID
    );

            if (request == null)
            {
                MessageBox.Show("Service request could not be found.");
                return;
            }

            if (request.Status != "Paid")
            {
                MessageBox.Show(
                    "The site visit can only be performed after the service request has been paid.",
                    "Site Visit Not Available"
                );
                return;
            }

            SiteVisitForm siteVisitForm =
                new SiteVisitForm(assignmentID);

            siteVisitForm.Show();
            this.Hide();
        }
    }
}
