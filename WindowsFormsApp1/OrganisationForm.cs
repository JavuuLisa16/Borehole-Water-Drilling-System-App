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
    public partial class OrganisationForm : Form
    {
        public OrganisationForm()
        {
            InitializeComponent();
            AppTheme.ApplyToForm(this);
            
            cmbProvider.Items.Clear();
            foreach (string provider in
         ProviderServiceSqlService.GetProviders())
            {
                cmbProvider.Items.Add(provider);
            }

            cmbProvider.SelectedIndex = -1;

            // Optional starting values
            lblOrganisationName.Text = "Organisation Name:";
            lblEmail.Text = "Email:";
            lblPhone.Text = "Phone:";
            lblAddress.Text = "Address:";
            lblLocation.Text = "Location:";
        }


        // Price details are shown in the service grid; no per-service selection on this form.
        private void cmbProvider_SelectedIndexChanged(
    object sender,
    EventArgs e)
        {
            if (cmbProvider.SelectedItem == null)
                return;

            string selectedProvider =
                cmbProvider.SelectedItem.ToString();

            dgvServices.Rows.Clear();

            List<string> services =
                ProviderServiceSqlService.GetServicesForProvider(
                    selectedProvider
                );

            foreach (string service in services)
            {
                decimal price =
                    ProviderServiceSqlService.GetServicePrice(
                        selectedProvider,
                        service
                    );

                dgvServices.Rows.Add(
                    service,
                    price.ToString("N2")
                );
            }

            ProviderServiceSqlService.ProviderInfoSqlRecord info =
                ProviderServiceSqlService.GetProviderInfo(
                    selectedProvider
                );

            if (info != null)
            {
                lblOrganisationName.Text =
                    "Organisation Name: " + info.Name;

                lblEmail.Text =
                    "Email: " + info.Email;

                lblPhone.Text =
                    "Phone: " + info.Phone;

                lblAddress.Text =
                    "Address: " + info.Address;

                lblLocation.Text =
                    "Location: " + info.Location;
            }
        }

        private void btnServiceRequests_Click(object sender, EventArgs e)
        {
            OrganisationTechnicianForm requests =
      new OrganisationTechnicianForm();

            requests.FormClosed += (s, args) => this.Show();

            requests.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            MainPage login = new MainPage();

            login.Show();
            this.Close();
        }

        private void btnTechnicians_Click(object sender, EventArgs e)
        {
            OrganisationTechnicianForm requests =
                  new OrganisationTechnicianForm();

            requests.FormClosed += (s, args) => this.Show();

            requests.Show();
            this.Hide();
        }
    }
}
