using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AdminClientForm : Form
    {
        private Form previousForm;
        public AdminClientForm(Form previousForm)
        {
            InitializeComponent();
            this.previousForm = previousForm;
            LoadClients();
            AppTheme.ApplyToForm(this);
        }

        private void LoadClients()
        {
            dgvClients.Rows.Clear();

            foreach (ClientProfile client in ClientSqlService.GetAllClients())
            {
                dgvClients.Rows.Add(
                    client.ClientID,
                    client.FullName,
                    client.Gender,
                    client.Email,
                    client.Phone,
                    client.Username,
                    client.Location
                );
            }
        }

        private void dgvClients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header clicks
            if (e.RowIndex < 0) return;

            var row = dgvClients.Rows[e.RowIndex];
            string fullName = row.Cells["ClientName"].Value?.ToString() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(fullName))
            {
                string[] names = fullName.Split(new[] { ' ' }, 2);
                txtFirstName.Text = names[0];
                txtLastName.Text = names.Length > 1 ? names[1] : string.Empty;
            }

            cmbGender.SelectedItem = row.Cells["Gender"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();
            txtPhone.Text = row.Cells["PhoneNumber"].Value?.ToString();
            txtUsername.Text = row.Cells["Username"].Value?.ToString();
            txtLocation.Text = row.Cells["Location"].Value?.ToString();
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

            if (string.IsNullOrWhiteSpace(txtClientID.Text))
            {
                MessageBox.Show("Please enter a Client ID.");
                txtClientID.Focus();
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
            if (ClientSqlService.GetClientByID(txtClientID.Text.Trim()) != null)
            {
                MessageBox.Show("A client with this ID already exists.");
                return;
            }

            ClientProfile client = new ClientProfile
            {
                ClientID = txtClientID.Text.Trim(),
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Gender = cmbGender.SelectedItem.ToString(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Location = txtLocation.Text.Trim()
            };

            try
            {
                ClientSqlService.AddClient(
                    client,
                    txtPassword.Text
                );
                using (SqlConnection verifyConnection =
       DatabaseConnection.GetConnection())
                {
                    verifyConnection.Open();

                    string verifyQuery = @"
        SELECT TOP 1
            p.PersonID,
            p.First_Name,
            p.Last_Name,
            p.Username,
            c.CLIENT_ID,
            c.LOC_ID
        FROM Persons p
        INNER JOIN Clients c
            ON c.PersonID = p.PersonID
        WHERE p.Username = @Username
        ORDER BY p.PersonID DESC;";

                    using (SqlCommand verifyCommand =
                           new SqlCommand(verifyQuery, verifyConnection))
                    {
                        verifyCommand.Parameters.AddWithValue(
                            "@Username", client.Username);

                        using (SqlDataReader reader =
                               verifyCommand.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                throw new Exception(
                                    "The client was not found in the database " +
                                    "after the save operation.");
                            }

                            MessageBox.Show(
                                "Client verified in database!\n\n" +
                                "Name: " + reader["First_Name"] + " " +
                                reader["Last_Name"] + "\n" +
                                "Username: " + reader["Username"] + "\n" +
                                "Generated Client ID: " + reader["CLIENT_ID"],
                                "Database Verification");
                        }
                    }

                    LoadClients();
                    ClearFields();

                    MessageBox.Show(
                        "Client added successfully.",
                        "Success"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not add client.\n\n" + ex.Message,
                    "Database Error"
                );
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClientID.Text))
            {
                MessageBox.Show("Please select a client.");
                return;
            }

            ClientProfile client =
                ClientSqlService.GetClientByID(txtClientID.Text.Trim());

            if (client == null)
            {
                MessageBox.Show("Client could not be found.");
                return;
            }

            client.FirstName = txtFirstName.Text.Trim();
            client.LastName = txtLastName.Text.Trim();

            if (cmbGender.SelectedIndex != -1)
            {
                client.Gender = cmbGender.SelectedItem.ToString();
            }

            client.Email = txtEmail.Text.Trim();
            client.Phone = txtPhone.Text.Trim();
            client.Username = txtUsername.Text.Trim();
            client.Location = txtLocation.Text.Trim();

            LoadClients();
            ClearFields();

            MessageBox.Show(
                "Client updated successfully.",
                "Success"
            );
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClientID.Text))
            {
                MessageBox.Show("Please select a client.");
                return;
            }

            ClientProfile client =
                ClientSqlService.GetClientByID(txtClientID.Text.Trim());

            if (client == null)
            {
                MessageBox.Show("Client could not be found.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete " +
                client.FullName + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                ClientSqlService.DeleteClient(txtClientID.Text.Trim());

                LoadClients();
                ClearFields();

                MessageBox.Show(
                    "Client deleted successfully.",
                    "Deleted"
                );
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            txtClientID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtUsername.Clear();
            txtLocation.Clear();
            txtPassword.Clear();

            cmbGender.SelectedIndex = -1;

            dgvClients.ClearSelection();
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





