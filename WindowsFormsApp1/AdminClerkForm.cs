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
    public partial class AdminClerkForm : Form
    {
        private Form previousForm;
        public AdminClerkForm(Form previousForm)
        {
            InitializeComponent();
            this.previousForm = previousForm;
            LoadClerks();
            AppTheme.ApplyToForm(this);
        }

        private void LoadClerks()
        {
            dgvClerks.Rows.Clear();
            
            foreach (ClerkProfile clerk in ClerkSqlService.GetAllClerks())
            {
                dgvClerks.Rows.Add(
                    clerk.ClerkID,
                    clerk.FullName,
                    clerk.Gender,
                    clerk.Email,
                    clerk.Phone,
                    clerk.Username,
                    clerk.Organisation,
                    clerk.EmploymentDate.ToShortDateString()
                );
            }
        }

        private void dgvClerks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvClerks.Rows[e.RowIndex];

            txtClerkID.Text =
                row.Cells["ClerkID"].Value?.ToString();

            string fullName =
                row.Cells["ClerkName"].Value?.ToString();

            if (!string.IsNullOrWhiteSpace(fullName))
            {
                string[] names = fullName.Split(
                    new[] { ' ' },
                    2
                );

                txtFirstName.Text = names[0];

                txtLastName.Text =
                    names.Length > 1 ? names[1] : "";
            }

            cmbGender.SelectedItem =
                row.Cells["Gender"].Value?.ToString();

            txtEmail.Text =
                row.Cells["Email"].Value?.ToString();

            txtPhone.Text =
                row.Cells["Phone"].Value?.ToString();

            txtUsername.Text =
                row.Cells["Username"].Value?.ToString();

            txtOrganisation.Text =
                row.Cells["Organisation"].Value?.ToString();

            DateTime employmentDate;

            if (DateTime.TryParse(
                row.Cells["EmploymentDate"].Value?.ToString(),
                out employmentDate))
            {
                dtpEmploymentDate.Value = employmentDate;
            }
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
            if (string.IsNullOrWhiteSpace(txtClerkID.Text))
            {
                MessageBox.Show("Please enter a Clerk ID.");
                txtClerkID.Focus();
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

            if (ClerkSqlService.GetClerkByID(
                    txtClerkID.Text.Trim()) != null)
            {
                MessageBox.Show(
                    "A clerk with this ID already exists."
                );
                return;
            }

            ClerkProfile clerk = new ClerkProfile
            {
                ClerkID = txtClerkID.Text.Trim(),
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Gender = cmbGender.SelectedItem.ToString(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Organisation = txtOrganisation.Text.Trim(),
                EmploymentDate = dtpEmploymentDate.Value.Date
            };

            try
            {
                // AddClerk now requires the initial password and inserts credentials
                ClerkSqlService.AddClerk(clerk, txtPassword.Text);

                LoadClerks();
                ClearFields();

                MessageBox.Show(
                    "Clerk added successfully.",
                    "Success"
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not add clerk.\n\n" + ex.Message,
                    "Database Error"
                );

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClerkID.Text))
            {
                MessageBox.Show("Please select a clerk.");
                return;
            }

            ClerkProfile clerk =
                ClerkSqlService.GetClerkByID(
                    txtClerkID.Text.Trim()
                );

            if (clerk == null)
            {
                MessageBox.Show("Clerk could not be found.");
                return;
            }

            clerk.FirstName =
                txtFirstName.Text.Trim();

            clerk.LastName =
                txtLastName.Text.Trim();

            if (cmbGender.SelectedIndex != -1)
            {
                clerk.Gender =
                    cmbGender.SelectedItem.ToString();
            }

            clerk.Email =
                txtEmail.Text.Trim();

            clerk.Phone =
                txtPhone.Text.Trim();

            clerk.Username =
                txtUsername.Text.Trim();

            clerk.Organisation =
                txtOrganisation.Text.Trim();

            clerk.EmploymentDate =
                dtpEmploymentDate.Value.Date;

            try
            {
                ClerkSqlService.UpdateClerk(
                    txtClerkID.Text.Trim(),
                    clerk
                );

                LoadClerks();
                ClearFields();

                MessageBox.Show(
                    "Clerk updated successfully.",
                    "Success"
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not update clerk.\n\n" +
                    ex.Message,
                    "Database Error"
                );
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClerkID.Text))
            {
                MessageBox.Show("Please select a clerk.");
                return;
            }

            ClerkProfile clerk =
                ClerkSqlService.GetClerkByID(
                    txtClerkID.Text.Trim()
                );

            if (clerk == null)
            {
                MessageBox.Show("Clerk could not be found.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete " +
                clerk.FullName + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    ClerkSqlService.AddClerk(
                        clerk,
                        txtPassword.Text
                    );

                    LoadClerks();
                    ClearFields();

                    MessageBox.Show(
                        "Clerk added successfully.",
                        "Success"
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Could not add clerk.\n\n" + ex.Message,
                        "Database Error"
                    );
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            txtClerkID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtUsername.Clear();
            txtOrganisation.Clear();
            txtPassword.Clear();

            cmbGender.SelectedIndex = -1;

            dtpEmploymentDate.Value = DateTime.Today;

            dgvClerks.ClearSelection();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ITAdminForm adminForm = new ITAdminForm();
            previousForm.Show();
            adminForm.Show();
            this.Close();
        }
    }
}
