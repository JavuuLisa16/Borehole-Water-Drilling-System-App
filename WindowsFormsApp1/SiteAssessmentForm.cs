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
    public partial class SiteAssessmentForm : Form
    {


        // Variables used by the form
        private string requestID = "";
        private string technicianID = "";
        private Form previousForm = null;

        // Constructor used by the Visual Studio Designer
        public SiteAssessmentForm()
        {
            InitializeComponent();
            AppTheme.ApplyToForm(this);
            StyleSiteAssessmentForm();
        }

        private void StyleSiteAssessmentForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        // Constructor used when opening the form from Technician Assignment
        public SiteAssessmentForm(string requestID, string technicianID, Form previousForm)
        {
            InitializeComponent();

            this.requestID = requestID;
            this.technicianID = technicianID;
            this.previousForm = previousForm;

            lblRequestID.Text = "Request ID: " + requestID;
            lblTechnicianID.Text = "Technician ID: " + technicianID;
        }

        private void SiteAssessmentForm_Load(object sender, EventArgs e)
        {
            // Display request and technician information
            lblRequestID.Text = "Request ID: " + requestID;
            lblTechnicianID.Text = "Technician ID: " + technicianID;

            // Site Condition
            cmbSiteCondition.Items.Clear();
            cmbSiteCondition.Items.Add("Good");
            cmbSiteCondition.Items.Add("Fair");
            cmbSiteCondition.Items.Add("Poor");

            // Accessibility
            cmbAccessibility.Items.Clear();
            cmbAccessibility.Items.Add("Easy");
            cmbAccessibility.Items.Add("Moderate");
            cmbAccessibility.Items.Add("Difficult");

            // Suitability
            cmbSuitability.Items.Clear();
            cmbSuitability.Items.Add("Suitable");
            cmbSuitability.Items.Add("Not Suitable");
            cmbSuitability.Items.Add("Requires Further Investigation");

            // No option selected initially
            cmbSiteCondition.SelectedIndex = -1;
            cmbAccessibility.SelectedIndex = -1;
            cmbSuitability.SelectedIndex = -1;

            // Automatically use today's date
            dtpAssessmentDate.Value = DateTime.Today;
        }

        private void btnSaveAssessment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.requestID))
            {
                MessageBox.Show(
                    "Request ID could not be found."
                );
                return;
            }

            if (string.IsNullOrWhiteSpace(this.technicianID))
            {
                MessageBox.Show(
                    "Technician ID could not be found."
                );
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbSiteCondition.Text))
            {
                MessageBox.Show(
                    "Please select the site condition."
                );
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbAccessibility.Text))
            {
                MessageBox.Show(
                    "Please select the accessibility."
                );
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbSuitability.Text))
            {
                MessageBox.Show(
                    "Please select the suitability."
                );
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAssessmentNotes.Text))
            {
                MessageBox.Show(
                    "Please enter assessment notes."
                );
                return;
            }

            if (!this.requestID.StartsWith("REQ") ||
                !int.TryParse(
                    this.requestID.Substring(3),
                    out int requestNumber))
            {
                MessageBox.Show(
                    "Invalid request ID."
                );
                return;
            }
            int technicianNumber;

            if (this.technicianID.StartsWith(
                    "TECH",
                    StringComparison.OrdinalIgnoreCase))
            {
                if (!int.TryParse(
                        this.technicianID.Substring(4),
                        out technicianNumber))
                {
                    MessageBox.Show(
                        "Invalid technician ID."
                    );
                    return;
                }
            }
            else if (!int.TryParse(
                         this.technicianID,
                         out technicianNumber))
            {
                MessageBox.Show(
                    "Invalid technician ID."
                );
                return;
            }

            try
            {
                SiteAssessmentSqlService.SaveAssessment(
                    requestNumber,
                    technicianNumber,
                    dtpAssessmentDate.Value.Date,
                    cmbSiteCondition.Text,
                    cmbAccessibility.Text,
                    cmbSuitability.Text,
                    txtAssessmentNotes.Text.Trim()
                );

                MessageBox.Show(
                    "Site assessment saved successfully.",
                    "Assessment Saved"
                );

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Site assessment could not be saved.\n\n" +
                    ex.Message,
                    "Assessment Error"
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