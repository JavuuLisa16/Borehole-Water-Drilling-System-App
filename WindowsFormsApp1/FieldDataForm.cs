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
    public partial class FieldDataForm : Form
    {
        private string visitID;
        public FieldDataForm(string VisitID)
        {
            InitializeComponent();
            this.visitID = VisitID;
            LoadVisit(VisitID);
        }

        // Parameterless ctor for general opening (no specific visit)
        public FieldDataForm()
        {
            InitializeComponent();
            AppTheme.ApplyToForm(this);
            StyleFieldDataForm();
        }
        private void StyleFieldDataForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void LoadVisit(string visitID)
        {
            SiteVisit visit =
                
                SiteVisitSqlService.GetVisitByID(visitID);

            if (visit == null)
            {
                MessageBox.Show("Site visit could not be found.");
                return;
            }

            lblVisitID.Text =
                "Visit ID: " + visit.VisitID;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveFieldData_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoilType.Text))
            {
                MessageBox.Show("Please enter the soil type.");
                txtSoilType.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtWaterDepth.Text))
            {
                MessageBox.Show("Please enter the water depth.");
                txtWaterDepth.Focus();
                return;
            }

            if (!decimal.TryParse(
                    txtWaterDepth.Text,
                    out decimal waterDepth))
            {
                MessageBox.Show(
                    "Please enter a valid water depth."
                );
                txtWaterDepth.Focus();
                return;
            }

            if (cmbDrillingStatus.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Please select the drilling status."
                );
                cmbDrillingStatus.Focus();
                return;
            }

            string visitID =
                lblVisitID.Text
                .Replace("Visit ID: ", "")
                .Trim();

            if (!visitID.StartsWith("V") ||
                !int.TryParse(
                    visitID.Substring(1),
                    out int siteVisitID))
            {
                MessageBox.Show(
                    "Invalid visit ID."
                );
                return;
            }

            string drillingStatus =
                cmbDrillingStatus.SelectedItem.ToString();

            try
            {
                bool saved =
                    FieldDataSqlService.SaveFieldData(
                        siteVisitID,
                        txtSoilType.Text.Trim(),
                        waterDepth,
                        drillingStatus,
                        txtRemarks.Text.Trim()
                    );

                if (!saved)
                {
                    MessageBox.Show(
                        "Field data could not be saved."
                    );
                    return;
                }

                MessageBox.Show(
                    "Field data saved successfully!\n\n" +
                    "Visit ID: " + visitID +
                    "\nSoil Type: " + txtSoilType.Text.Trim() +
                    "\nWater Depth: " + waterDepth +
                    "\nDrilling Status: " + drillingStatus,
                    "Field Data"
                );

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Field data could not be saved.\n\n" +
                    ex.Message,
                    "Field Data Error"
                );
            }
        }

        private void FieldDataForm_Load(object sender, EventArgs e)
        {
            cmbDrillingStatus.Items.Clear();

            cmbDrillingStatus.Items.Add("In Progress");
            cmbDrillingStatus.Items.Add("Completed");
            cmbDrillingStatus.Items.Add("Requires Further Work");

            cmbDrillingStatus.SelectedIndex = -1;
        }
    }
    }

