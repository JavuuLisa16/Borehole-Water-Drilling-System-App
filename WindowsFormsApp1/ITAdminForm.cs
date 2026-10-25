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
    public partial class ITAdminForm : Form
    {
        public ITAdminForm()
        {
            InitializeComponent();
            AppTheme.ApplyAdminDashboardStyle(this);
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            AdminClientForm clients =
        new AdminClientForm(this);


            clients.Show();
            this.Hide();
        }

        private void btnClerks_Click(object sender, EventArgs e)
        {
            AdminClerkForm clerks =
       new AdminClerkForm(this);

            clerks.Show();
            this.Hide();
        }

        private void btnTechnicians_Click(object sender, EventArgs e)
        {
            AdminTechnicianForm technicians =
       new AdminTechnicianForm(this);

            technicians.Show();
            this.Hide();
        }

        private void btnDSPs_Click(object sender, EventArgs e)
        {
            AdminDspForm dspForm =
        new AdminDspForm(this);

            dspForm.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            MainPage login = new MainPage();
            
            login.Show();
            this.Close();
        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            ITAdminserviceFrom service = new ITAdminserviceFrom();
            service.FormClosed += (s, args) => this.Show();
            service.Show();
            this.Hide();
        }
    }
}
