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
    public partial class ForgotCredentialsForm : Form
    {
        public ForgotCredentialsForm()
        {
            InitializeComponent();
            AppTheme.ApplyToForm(this);
            StyleForgotForm();
        }
        private void StyleForgotForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnFindUsername_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter your email address.");
                txtEmail.Focus();
                return;
            }

            Credential credential =
    CredentialSqlService.FindByEmail(
        txtEmail.Text.Trim()
    );

            if (credential == null)
            {
                MessageBox.Show(
                    "No account was found with that email address."
                );
                return;
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter your username.");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter your email address.");
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                MessageBox.Show("Please enter a new password.");
                txtNewPassword.Focus();
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("The passwords do not match.");
                txtConfirmPassword.Focus();
                return;
            }

            Credential credential =
     CredentialSqlService.FindByUsername(
         txtUsername.Text.Trim()
     );
            if (credential == null)
            {
                MessageBox.Show("Username could not be found.");
                return;
            }

            if (!credential.Email.Equals(
                    txtEmail.Text.Trim(),
                    System.StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "The username and email address do not match."
                );
                return;
            }

            credential.Password = txtNewPassword.Text;

            MessageBox.Show(
                "Your password has been changed successfully.",
                "Password Changed"
            );

            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainPage mainPage = new MainPage();

            mainPage.Show();
            this.Close();
        }
    }
    }

