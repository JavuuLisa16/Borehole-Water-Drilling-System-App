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
    public partial class SiteVisitForm : Form

    {
        private string assignmentID;
        private void LoadSiteVisits()
        {
            dgvSiteVisits.Rows.Clear();

            if (string.IsNullOrWhiteSpace(assignmentID))
            {
                MessageBox.Show("Assignment ID is missing.");
                return;
            }

            List<SiteVisit> visits =
                SiteVisitSqlService.GetVisitsByAssignmentID(
                    assignmentID
                );

            foreach (SiteVisit visit in visits)
            {
                dgvSiteVisits.Rows.Add(
                    visit.VisitID,
                    visit.RequestID,
                    visit.AssignmentID,
                    visit.VisitDate.ToShortDateString(),
                    visit.Notes
                );
            }
        }
        public SiteVisitForm(string assignmentID)
        {
            InitializeComponent();

            SetupSiteVisitGrid();

            this.assignmentID = assignmentID;

            AppTheme.ApplyToForm(this);
            StyleSiteVisitForm();

            LoadAssignment(assignmentID);
            LoadSiteVisits();
        }

        public SiteVisitForm()
        {
            InitializeComponent();

            SetupSiteVisitGrid();

            AppTheme.ApplyToForm(this);
            StyleSiteVisitForm();
        }

        private void StyleSiteVisitForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void LoadAssignment(string assignmentID)
        {
            Assignment assignment =
                TechnicianAssignmentSqlService.GetAssignmentByID(
                    assignmentID
                );

            if (assignment == null)
            {
                MessageBox.Show("Assignment could not be found.");
                return;
            }

            // Allow the labels to display their complete text.
            lblAssignmentID.AutoSize = true;
            lblRequestID.AutoSize = true;
            lblVisitID.AutoSize = true;

            lblAssignmentID.Text =
                "Assignment ID: " + assignment.AssignmentID;

            lblRequestID.Text =
                "Request ID: " + assignment.RequestID;

            lblVisitID.Text = "Visit ID: Not saved yet";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Close this form so the caller's FormClosed handler (if any) can show itself.
            this.Close();
        }

        private void btnSaveVisit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVisitNotes.Text))
            {
                MessageBox.Show(
                    "Please enter the visit notes."
                );

                txtVisitNotes.Focus();
                return;
            }

            string assignmentID =
                lblAssignmentID.Text.Replace(
                    "Assignment ID: ",
                    ""
                ).Trim();

            string requestID =
                lblRequestID.Text.Replace(
                    "Request ID: ",
                    ""
                ).Trim();

            string numericAssignmentID = assignmentID.Trim();

            if (numericAssignmentID.StartsWith(
                    "ASS",
                    StringComparison.OrdinalIgnoreCase))
            {
                numericAssignmentID =
                    numericAssignmentID.Substring(3);
            }
            else if (numericAssignmentID.StartsWith(
                         "A",
                         StringComparison.OrdinalIgnoreCase))
            {
                numericAssignmentID =
                    numericAssignmentID.Substring(1);
            }

            if (!int.TryParse(
                    numericAssignmentID,
                    out int assignmentNumber))
            {
                MessageBox.Show(
                    "Invalid assignment ID: " + assignmentID
                );
                return;
            }

            if (!requestID.StartsWith("REQ") ||
                !int.TryParse(
                    requestID.Substring(3),
                    out int requestNumber))
            {
                MessageBox.Show(
                    "Invalid request ID."
                );
                return;
            }

            try
            {
                int siteVisitID =
                    SiteVisitSqlService.SaveSiteVisit(
                        requestNumber,
                        assignmentNumber,
                        dtpVisitDate.Value.Date,
                        txtVisitNotes.Text.Trim()
                    );
                lblVisitID.Text =
    "Visit ID: V" + siteVisitID.ToString("D3");


                LoadSiteVisits();

                MessageBox.Show(
                    "Site visit saved successfully!\n\n" +
                    "Visit ID: V" + siteVisitID.ToString("D3") +
                    "\nAssignment ID: " + assignmentID +
                    "\nRequest ID: " + requestID +
                    "\nVisit Date: " +
                    dtpVisitDate.Value.ToShortDateString(),
                    "Site Visit"
                );

                txtVisitNotes.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Site visit could not be saved.\n\n" +
                    ex.Message,
                    "Site Visit Error"
                );
            }
        }

        private void btnFieldData_Click(object sender, EventArgs e)
        {
            string visitID =
                SiteVisitSqlService.GetVisitIDForAssignment(
                    assignmentID
                );

            if (string.IsNullOrEmpty(visitID))
            {
                MessageBox.Show(
                    "Please save the site visit before entering field data."
                );
                return;
            }

            FieldDataForm fieldDataForm =
                new FieldDataForm(visitID);

            fieldDataForm.Show();
            this.Hide();
        }

        private void SetupSiteVisitGrid()
        {
            dgvSiteVisits.AutoGenerateColumns = false;
            dgvSiteVisits.Columns.Clear();

            dgvSiteVisits.Columns.Add("VisitID", "Visit ID");
            dgvSiteVisits.Columns.Add("RequestID", "Request ID");
            dgvSiteVisits.Columns.Add("AssignmentID", "Assignment ID");
            dgvSiteVisits.Columns.Add("VisitDate", "Visit Date");
            dgvSiteVisits.Columns.Add("Notes", "Notes");

            dgvSiteVisits.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}


