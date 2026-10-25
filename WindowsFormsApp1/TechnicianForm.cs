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
    public partial class TechnicianForm : Form
    {
        public TechnicianForm()
        {
            InitializeComponent();
            AppTheme.ApplyToForm(this);
            StyleTechnicianForm();
        }
         private void StyleTechnicianForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnAssignments_Click(object sender, EventArgs e)
        {
            TechnicianAssignments Assignment = new TechnicianAssignments();
            Assignment.Show();
            this.Hide();
            Assignment.FormClosed += (s, args) => this.Show();
        }

        private void btnSiteVisits_Click(object sender, EventArgs e)
        {
            SiteVisitForm SiteVisit = new SiteVisitForm();
            SiteVisit.FormClosed += (s, args) => this.Show();
            SiteVisit.Show();
            this.Hide();
        }

        private void btnLogout2_Click(object sender, EventArgs e)
        {
            MainPage login = new MainPage();
            login.Show();
            this.Close();
        }

        private void btnFieldData_Click(object sender, EventArgs e)
        {
            // Open FieldDataForm without a specific visit (user can select or create a visit there)
            FieldDataForm fieldData = new FieldDataForm();
            fieldData.FormClosed += (s, args) => this.Show();
            fieldData.Show();
            this.Hide();
        }

        private void btnTechProfile_Click(object sender, EventArgs e)
        {
            TechnicianProfileForm profile =
        new TechnicianProfileForm();

            profile.Show();
            this.Hide();
        }
    }
}
