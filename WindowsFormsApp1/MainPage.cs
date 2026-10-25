using System.Data.SqlClient;
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
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
            AppTheme.ApplyToForm(this);
            StyleLoginForm();

        }

        private void StyleLoginForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter your username.");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your password.");
                txtPassword.Focus();
                return;
            }

            
          
            string query = @"
        SELECT
            c.PersonID,
            p.Username,
            c.Password,
            c.Role_Name
        FROM Credentials c
        INNER JOIN Persons p
            ON c.PersonID = p.PersonID
        WHERE p.Username = @Username
        AND c.Password = @Password";

            try
            {
                using (SqlConnection connection =
        DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    using (SqlCommand command =
                           new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show(
                                    "Username or password is incorrect."
                                );

                                txtPassword.Clear();
                                txtUsername.Focus();
                                return;
                            }

                            int personID =
                                Convert.ToInt32(reader["PersonID"]);

                            string role =
                                reader["Role_Name"].ToString();

                            string loggedInUsername =
                                username;

                            // Store the logged-in user's details
                            CurrentUser.PersonID = personID;
                            CurrentUser.Username = loggedInUsername;
                            CurrentUser.Role = role;

                            // Open the correct page according to the user's role
                            if (role == "IT Admin")
                            {
                                var adminForm = new ITAdminForm();
                                adminForm.Show();
                                this.Hide();
                            }
                            else if (role == "Client")
                            {
                                ClientProfile client =
       ClientSqlService.GetClientByPersonID(
           CurrentUser.PersonID
       );

                                if (client == null)
                                {
                                    MessageBox.Show(
                                        "Client profile could not be found."
                                    );

                                    return;
                                }


                                ClientProfileStore.CurrentClient = client;
                                var clientForm = new ClientForm();
                                clientForm.Show();
                                this.Hide();
                            }
                            else if (role == "Clerk")
                            {
                                var clerkForm = new ClerkForm();
                                clerkForm.Show();
                                this.Hide();
                            }
                            else if (role == "Technician")
                            {
                                var technicianForm = new TechnicianForm();
                                technicianForm.Show();
                                this.Hide();
                            }
                            else if (role == "Organisation")
                            {
                                var organisationForm = new OrganisationForm();
                                organisationForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("The account role is not recognised.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Database connection error:\n\n" + ex.Message,
                    "Error"
                );
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();

            txtUsername.Focus(); // ensure username box is focused after clearing
        }

        private void btnForgotCredentials_Click(object sender, EventArgs e)
        {
            ForgotCredentialsForm recovery =
      new ForgotCredentialsForm();

            recovery.Show();
            this.Hide();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "BOREHOLE MANAGEMENT SYSTEM";
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = AppTheme.Primary;
        }
    }
}
