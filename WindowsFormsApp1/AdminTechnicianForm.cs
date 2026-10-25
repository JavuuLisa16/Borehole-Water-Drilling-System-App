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
    public partial class AdminTechnicianForm : Form
    { 
        private Form previousForm;
        public AdminTechnicianForm(Form previousForm)
        {
            InitializeComponent();
            this.previousForm = previousForm;
            LoadTechnicians();
            LoadProviders();
            AppTheme.ApplyToForm(this);
        }
        private void LoadTechnicians()
        {
            dgvTechnicians.Rows.Clear();

            foreach (Technician technician
                     in TechnicianSqlService.GetAllTechnicians())
            {
                dgvTechnicians.Rows.Add(
                    technician.TechnicianID,
                    technician.TechnicianName,
                    technician.Gender,
                    technician.Email,
                    technician.Phone,
                    technician.Username,
                    technician.Specialisation,
                    technician.Provider,
                    technician.Status
                );
            }
        }
        private void LoadProviders()
        {
            cmbProvider.Items.Clear();

            foreach (string provider in
         ProviderServiceSqlService.GetProviders())
            {
                cmbProvider.Items.Add(provider);
            }

            cmbProvider.SelectedIndex = -1;
        }

        private void dgvTechnicians_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvTechnicians.Rows[e.RowIndex];

            txtTechnicianID.Text =
                row.Cells["TechnicianID"].Value?.ToString();

            string fullName =
                row.Cells["TechnicianName"].Value?.ToString();

            string[] names = fullName.Split(
                new[] { ' ' },
                2
            );

            txtFirstName.Text = names[0];

            txtLastName.Text =
                names.Length > 1 ? names[1] : "";

            cmbGender.SelectedItem =
                row.Cells["Gender"].Value?.ToString();

            txtEmail.Text =
                row.Cells["Email"].Value?.ToString();

            txtPhone.Text =
                row.Cells["Phone"].Value?.ToString();

            txtUsername.Text =
                row.Cells["Username"].Value?.ToString();

            txtSpecialisation.Text =
                row.Cells["Specialisation"].Value?.ToString();

            cmbProvider.SelectedItem =
                row.Cells["Provider"].Value?.ToString();

            cmbStatus.SelectedItem =
                row.Cells["Status"].Value?.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter a username.");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter a password.");
                txtPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTechnicianID.Text))
            {
                MessageBox.Show("Please enter a Technician ID.");
                txtTechnicianID.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter the first name.");
                txtFirstName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter the last name.");
                txtLastName.Focus();
                return;
            }

            if (cmbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a gender.");
                return;
            }

            if (cmbProvider.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a provider.");
                return;
            }

            if (cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a status.");
                return;
            }

            Technician technician = new Technician
            {
                TechnicianID = txtTechnicianID.Text.Trim(),
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Gender = cmbGender.SelectedItem?.ToString(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Specialisation = txtSpecialisation.Text.Trim(),
                Provider = cmbProvider.SelectedItem?.ToString(),
                Status = cmbStatus.SelectedItem?.ToString()
            };

            try
            {
                int technicianID = TechnicianSqlService.AddTechnician(technician, txtPassword.Text);
                LoadTechnicians();
                ClearFields();

                MessageBox.Show(
                    "Technician added successfully.\n\n" +
                    "Technician ID: TECH" + technicianID.ToString("D3"),
                    "Success"
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Technician could not be added.\n\n" + ex.Message,
                    "Error"
                );
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Technician technician = TechnicianSqlService.GetTechnicianByID(
                txtTechnicianID.Text.Trim()
            );

            if (technician == null)
            {
                MessageBox.Show("Please select a technician.");
                return;
            }

            technician.FirstName = txtFirstName.Text.Trim();
            technician.LastName = txtLastName.Text.Trim();

            if (cmbGender.SelectedIndex != -1)
                technician.Gender = cmbGender.SelectedItem.ToString();

            technician.Email = txtEmail.Text.Trim();
            technician.Phone = txtPhone.Text.Trim();
            technician.Username = txtUsername.Text.Trim();
            technician.Specialisation = txtSpecialisation.Text.Trim();

            if (cmbProvider.SelectedIndex != -1)
                technician.Provider = cmbProvider.SelectedItem.ToString();

            if (cmbStatus.SelectedIndex != -1)
                technician.Status = cmbStatus.SelectedItem.ToString();

            try
            {
                bool updated = TechnicianSqlService.UpdateTechnician(
                    technician.TechnicianID,
                    technician
                );

                if (updated)
                {
                    LoadTechnicians();
                    ClearFields();

                    MessageBox.Show("Technician updated successfully.", "Success");
                }
                else
                {
                    MessageBox.Show("Technician could not be updated.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Technician could not be updated.\n\n" + ex.Message, "Error");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Technician technician = TechnicianSqlService.GetTechnicianByID(
                txtTechnicianID.Text.Trim()
            );

            if (technician == null)
            {
                MessageBox.Show("Please select a technician.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete " + technician.TechnicianName + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            try
            {
                bool deleted = TechnicianSqlService.DeleteTechnician(technician.TechnicianID);

                if (deleted)
                {
                    LoadTechnicians();
                    ClearFields();

                    MessageBox.Show("Technician deleted successfully.", "Deleted");
                }
                else
                {
                    MessageBox.Show("Technician could not be deleted.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Technician could not be deleted.\n\n" + ex.Message, "Delete Error");
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
            txtTechnicianID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtUsername.Clear();
            txtSpecialisation.Clear();
            txtPassword.Clear();

            cmbGender.SelectedIndex = -1;
            cmbProvider.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;

            dgvTechnicians.ClearSelection();
        }
    }
}

