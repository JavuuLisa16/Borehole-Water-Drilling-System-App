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
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
            AppTheme.ApplyToForm(this);
        }

        private void btnBookService_Click(object sender, EventArgs e)
        {
            ServiceRequetsForm Request = new ServiceRequetsForm();
            Request.Show();
            this.Hide();
        }

        private void btnMyRequets_Click(object sender, EventArgs e)
        {
            MyRequestsForm requests = new MyRequestsForm();
            // Set Owner so the child can reference the parent and return to it
            requests.Owner = this;
            requests.FormClosed += (s, args) => this.Show();
            requests.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?");
            if (result == DialogResult.OK)
            {
                MainPage login = new MainPage();
                login.Show();
                this.Close();
            }
        }

        private void btnMyProfile_Click(object sender, EventArgs e)
        {
            ClientProfileForm Profile = new ClientProfileForm();
            Profile.FormClosed += (s, args) => this.Show();
            Profile.Show();
            this.Hide();
        }

        private void btnMyReviews_Click(object sender, EventArgs e)
        {
            ReviewsForm Remarks = new ReviewsForm();
            
            Remarks.FormClosed += (s, args) => this.Show();

            Remarks.Show();
            this.Hide();
        }
    }
}
