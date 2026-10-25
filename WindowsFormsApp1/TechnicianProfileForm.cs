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
    public partial class TechnicianProfileForm : Form
    {
        public TechnicianProfileForm()
        {
            InitializeComponent();
            LoadProfile();
            AppTheme.ApplyToForm(this);
            StyleTechnicianProfileForm();
        }
        private void StyleTechnicianProfileForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void LoadProfile()
        {
            var technician = TechnicianProfileStore.CurrentTechnician;
            // Populate fields with technician profile
            txtFirstName.Text = technician.FirstName;
            txtLastName.Text = technician.LastName;
            cmbGender.SelectedItem = technician.Gender;
            txtEmail.Text = technician.Email;
            txtPhone.Text = technician.Phone;
            txtUsername.Text = technician.Username;

            lblTechnicianID.Text = "Technician ID: " + technician.TechnicianID;
            lblSpecialisation.Text = "Specialisation: " + technician.Specialisation;
            lblOrganisation.Text = "Organisation: " + technician.Organisation;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (TechnicianProfileStore.CurrentTechnician == null)
            {
                MessageBox.Show(
                    "Technician profile could not be found.",
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

            // Retrieve the full Technician record to preserve fields
            // not present on TechnicianProfile (Status, Provider)
            Technician existingTechnician = TechnicianSqlService.GetTechnicianByID(
                TechnicianProfileStore.CurrentTechnician.TechnicianID
            );

            Technician updatedTechnician = new Technician
            {
                    TechnicianID =
                        TechnicianProfileStore.CurrentTechnician.TechnicianID,

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

                    Specialisation = TechnicianProfileStore.CurrentTechnician.Specialisation,

                    // If the existing full Technician record was found, keep its Status/Provider
                    Status = existingTechnician == null ? string.Empty : existingTechnician.Status,

                    Provider = existingTechnician == null ? string.Empty : existingTechnician.Provider
                };

            try
            {
                TechnicianSqlService.UpdateTechnician(
                    updatedTechnician.TechnicianID,
                    updatedTechnician
                );

                // Update the session copy only after SQL succeeds
                TechnicianProfileStore.CurrentTechnician.FirstName =
                    updatedTechnician.FirstName;

                TechnicianProfileStore.CurrentTechnician.LastName =
                    updatedTechnician.LastName;

                TechnicianProfileStore.CurrentTechnician.Gender =
                    updatedTechnician.Gender;

                TechnicianProfileStore.CurrentTechnician.Email =
                    updatedTechnician.Email;

                TechnicianProfileStore.CurrentTechnician.Phone =
                    updatedTechnician.Phone;

                TechnicianProfileStore.CurrentTechnician.Username =
                    updatedTechnician.Username;

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

        private void btnBack_Click(object sender, EventArgs e)
        {
            TechnicianForm technicianForm =
        new TechnicianForm();

            technicianForm.Show();
            this.Close();
        }
    }
    }

