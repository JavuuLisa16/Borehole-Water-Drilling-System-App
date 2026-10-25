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
    public partial class ClerkForm : Form
    {
        public ClerkForm()
        {
            InitializeComponent();
            AppTheme.ApplyToForm(this);
            StyleClerkForm();
        }
        private void StyleClerkForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void btnServiceRequests_Click(object sender, EventArgs e)
        {
            ClerkServiceRequestsForm Requests = new ClerkServiceRequestsForm();
            Requests.FormClosed += (s, args) => this.Show();
            Requests.Show();
            this.Hide();

        }

        private void btnReviewRequests_Click(object sender, EventArgs e)
        {
            ClerkReviewRequestForm Review = new ClerkReviewRequestForm();
            Review.FormClosed += (s, args) => this.Show();
            Review.Show();
            this.Hide();
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            ClerkClientsForm Client = new ClerkClientsForm();
            Client.FormClosed += (s, args) => this.Show();
            Client.Show();
            this.Hide();
        }

        private void btnMyProfile_Click(object sender, EventArgs e)
        {
            ClerkProfileForm Profile = new ClerkProfileForm();
            Profile.FormClosed += (s, args) => this.Show();
            Profile.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                MainPage login = new MainPage();
                login.Show();
                this.Close();
            }
        }
    }
}
