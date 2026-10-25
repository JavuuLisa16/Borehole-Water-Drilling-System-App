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
    [System.ComponentModel.DesignerCategory("Form")]
    public partial class AdminDspForm : Form
    {
        private Form previousForm;
        public AdminDspForm(Form previousForm)
        {
            InitializeComponent();
            this.previousForm = previousForm;
            LoadProviders();
            AppTheme.ApplyToForm(this);
        }
        private void LoadProviders()
        {
            dgvDsp.Rows.Clear();

            foreach (
    DrillingServiceProvider provider
    in DrillingServiceProviderSqlService.GetAllProviders())
            {
                dgvDsp.Rows.Add(
                    provider.DSPID,
                    provider.OrganisationName,
                    provider.Email,
                    provider.Phone,
                    provider.Address,
                    provider.Location
                );
            }
        }

        private void dgvDsp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvDsp.Rows[e.RowIndex];

            txtDspID.Text =
                row.Cells["DSPID"].Value?.ToString();

            txtOrganisationName.Text =
                row.Cells["OrganisationName"].Value?.ToString();

            txtEmail.Text =
                row.Cells["Email"].Value?.ToString();

            txtPhone.Text =
                row.Cells["Phone"].Value?.ToString();

            txtAddress.Text =
                row.Cells["Address"].Value?.ToString();

            txtLocation.Text =
                row.Cells["Location"].Value?.ToString();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDspID.Text))
            {
                MessageBox.Show("Please enter the DSP ID.");
                txtDspID.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtOrganisationName.Text))
            {
                MessageBox.Show("Please enter the organisation name.");
                txtOrganisationName.Focus();
                return;
            }

            if (DrillingServiceProviderSqlService.GetProviderByID(
                    txtDspID.Text.Trim()) != null)
            {
                MessageBox.Show(
                    "A provider with this DSP ID already exists."
                );
                return;
            }

            DrillingServiceProvider provider =
                new DrillingServiceProvider
                {
                    DSPID =
                        txtDspID.Text.Trim(),

                    OrganisationName =
                        txtOrganisationName.Text.Trim(),

                    Email =
                        txtEmail.Text.Trim(),

                    Phone =
                        txtPhone.Text.Trim(),

                    Address =
                        txtAddress.Text.Trim(),

                    Location =
                        txtLocation.Text.Trim()
                };

            try
            {
                DrillingServiceProviderSqlService.AddProvider(
                    provider
                );

                LoadProviders();
                ClearFields();

                MessageBox.Show(
                    "Drilling service provider added successfully.",
                    "Success"
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not add provider.\n\n" +
                    ex.Message,
                    "Database Error"
                );
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDspID.Text))
            {
                MessageBox.Show("Please select a provider.");
                return;
            }

            DrillingServiceProvider provider =
                DrillingServiceProviderSqlService.GetProviderByID(
                    txtDspID.Text.Trim()
                );

            if (provider == null)
            {
                MessageBox.Show("Provider could not be found.");
                return;
            }

            provider.OrganisationName =
                txtOrganisationName.Text.Trim();

            provider.Email =
                txtEmail.Text.Trim();

            provider.Phone =
                txtPhone.Text.Trim();

            provider.Address =
                txtAddress.Text.Trim();

            provider.Location =
                txtLocation.Text.Trim();

            try
            {
                DrillingServiceProviderSqlService.UpdateProvider(
                    txtDspID.Text.Trim(),
                    provider
                );

                LoadProviders();
                ClearFields();

                MessageBox.Show(
                    "Drilling service provider updated successfully.",
                    "Success"
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not update provider.\n\n" +
                    ex.Message,
                    "Database Error"
                );
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDspID.Text))
            {
                MessageBox.Show("Please select a provider.");
                return;
            }

            DrillingServiceProvider provider =
                DrillingServiceProviderSqlService.GetProviderByID(
                    txtDspID.Text.Trim()
                );

            if (provider == null)
            {
                MessageBox.Show("Provider could not be found.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete " +
                provider.OrganisationName + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    DrillingServiceProviderSqlService.DeleteProvider(
                        txtDspID.Text.Trim()
                    );

                    LoadProviders();
                    ClearFields();

                    MessageBox.Show(
                        "Provider deleted successfully.",
                        "Deleted"
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Could not delete provider.\n\n" +
                        ex.Message,
                        "Database Error"
                    );
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ITAdminForm adminForm = new ITAdminForm();
            previousForm.Show();

            adminForm.Show();
            this.Close();
        }
        private void ClearFields()
        {
            txtDspID.Clear();
            txtOrganisationName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtLocation.Clear();

            dgvDsp.ClearSelection();
        }
    }
}
