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
    public partial class ClientProfileForm : Form
    {
        public ClientProfileForm()
        {
            InitializeComponent();
            LoadProfile();
            AppTheme.ApplyToForm(this);
            StyleClientProfileForm();
        }
        private void StyleClientProfileForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void LoadProfile()
        {
            txtFirstName.Text = ClientProfileStore.CurrentClient.FirstName;
            txtLastName.Text = ClientProfileStore.CurrentClient.LastName;
            txtEmail.Text = ClientProfileStore.CurrentClient.Email;
            txtPhone.Text = ClientProfileStore.CurrentClient.Phone;
            txtUsername.Text = ClientProfileStore.CurrentClient.Username;
            cmbGender.SelectedItem = ClientProfileStore.CurrentClient.Gender;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter your first name.");
                txtFirstName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter your last name.");
                txtLastName.Focus();
                return;
            }

            if (cmbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Please select your gender.");
                return;
            }

            ClientProfile updatedClient =
                new ClientProfile
                {
                    ClientID =
                        ClientProfileStore.CurrentClient.ClientID,

                    FirstName =
                        txtFirstName.Text.Trim(),

                    LastName =
                        txtLastName.Text.Trim(),

                    Gender =
                        cmbGender.SelectedItem.ToString(),

                    Email =
                        txtEmail.Text.Trim(),

                    Phone =
                        txtPhone.Text.Trim(),

                    Username =
                        txtUsername.Text.Trim(),

                    Location =
                        ClientProfileStore.CurrentClient.Location
                };

            try
            {
                ClientSqlService.UpdateClient(
                    updatedClient.ClientID,
                    updatedClient
                );

                // Update the session copy only after SQL succeeds
                ClientProfileStore.CurrentClient.FirstName =
                    updatedClient.FirstName;

                ClientProfileStore.CurrentClient.LastName =
                    updatedClient.LastName;

                ClientProfileStore.CurrentClient.Gender =
                    updatedClient.Gender;

                ClientProfileStore.CurrentClient.Email =
                    updatedClient.Email;

                ClientProfileStore.CurrentClient.Phone =
                    updatedClient.Phone;

                ClientProfileStore.CurrentClient.Username =
                    updatedClient.Username;

                LoadProfile();

                MessageBox.Show(
                    "Profile updated successfully!",
                    "Success"
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not update your profile.\n\n" +
                    ex.Message,
                    "Database Error"
                );
            }
        }
    }
}
