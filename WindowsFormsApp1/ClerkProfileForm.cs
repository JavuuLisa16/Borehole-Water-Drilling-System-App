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
    public partial class ClerkProfileForm : Form
    {
        public ClerkProfileForm()
        {
            InitializeComponent();
            DateTime Employment = dtpEmploymentDate.Value.Date;
            LoadProfile();
            AppTheme.ApplyToForm(this);
            StyleClerkProfileForm();
        }
        private void StyleClerkProfileForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void LoadProfile()
        {
            txtFirstName.Text = ClerkProfileStore.CurrentClerk.FirstName;
            txtLastName.Text = ClerkProfileStore.CurrentClerk.LastName;

            cmbGender.SelectedItem = ClerkProfileStore.CurrentClerk.Gender;

            txtEmail.Text = ClerkProfileStore.CurrentClerk.Email;
            txtPhoneNumber.Text = ClerkProfileStore.CurrentClerk.Phone;
            txtUsername.Text = ClerkProfileStore.CurrentClerk.Username;

            // Clerk ID is a textbox in the designer
            txtCLERKID.Text = ClerkProfileStore.CurrentClerk.ClerkID;

            // Organisation is represented by comboBox1 in the designer
            comboBox1.SelectedItem = ClerkProfileStore.CurrentClerk.Organisation;

            // Employment date is a DateTimePicker in the designer
            dtpEmploymentDate.Value = ClerkProfileStore.CurrentClerk.EmploymentDate;


        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
            private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ClerkProfileStore.CurrentClerk == null)
            {
                MessageBox.Show(
                    "Clerk profile could not be found.",
                    "Error"
                );
                return;
            }

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

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter your username.");
                txtUsername.Focus();
                return;
            }

            ClerkProfile updatedClerk =
                new ClerkProfile
                {
                    ClerkID =
                        ClerkProfileStore.CurrentClerk.ClerkID,

                    FirstName =
                        txtFirstName.Text.Trim(),

                    LastName =
                        txtLastName.Text.Trim(),

                    Gender =
                        cmbGender.SelectedItem.ToString(),

                    Email =
                        txtEmail.Text.Trim(),

                    Phone =
                        txtPhoneNumber.Text.Trim(),

                    Username =
                        txtUsername.Text.Trim(),

                    Organisation =
                        comboBox1.SelectedItem == null
                            ? ""
                            : comboBox1.SelectedItem.ToString(),

                    EmploymentDate =
                        dtpEmploymentDate.Value.Date
                };

            try
            {
                ClerkSqlService.UpdateClerk(
                    updatedClerk.ClerkID,
                    updatedClerk
                );

                // Update the session copy only after SQL succeeds
                ClerkProfileStore.CurrentClerk.FirstName =
                    updatedClerk.FirstName;

                ClerkProfileStore.CurrentClerk.LastName =
                    updatedClerk.LastName;

                ClerkProfileStore.CurrentClerk.Gender =
                    updatedClerk.Gender;

                ClerkProfileStore.CurrentClerk.Email =
                    updatedClerk.Email;

                ClerkProfileStore.CurrentClerk.Phone =
                    updatedClerk.Phone;

                ClerkProfileStore.CurrentClerk.Username =
                    updatedClerk.Username;

                ClerkProfileStore.CurrentClerk.Organisation =
                    updatedClerk.Organisation;

                ClerkProfileStore.CurrentClerk.EmploymentDate =
                    updatedClerk.EmploymentDate;

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

