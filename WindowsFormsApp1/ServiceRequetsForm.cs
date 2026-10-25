using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace WindowsFormsApp1
{
    public partial class ServiceRequetsForm : Form
    {
        public ServiceRequetsForm()
        {
            InitializeComponent();
            AppTheme.ApplyToForm(this);

            // Connect the province and city events only once
            this.Load -= ServiceRequetsForm_Load;
            this.Load += ServiceRequetsForm_Load;

            cmbProvince.SelectedIndexChanged -=
                cmbProvince_SelectedIndexChanged;

            cmbProvince.SelectedIndexChanged +=
                cmbProvince_SelectedIndexChanged;

            LoadProviders();

            cmbProvider.SelectedIndex = -1;
            cmbServices.SelectedIndex = -1;

            cmbCity.Enabled = false;
        }
        private void LoadProviders()
        {
            try
            {
                List<string> providers =
                    ProviderServiceSqlService.GetProviders();

                cmbProvider.Items.Clear();

                foreach (string provider in providers)
                {
                    cmbProvider.Items.Add(provider);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load drilling service providers.\n\n" +
                    ex.Message,
                    "Database Error"
                );
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cmbServices.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a service.");
                return;
            }

            if (cmbProvider.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a drilling service provider.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAddressline1.Text))
            {
                MessageBox.Show("Please enter Address Line 1.");
                txtAddressline1.Focus();
                return;
            }

            if (cmbCity.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a city.");
                cmbCity.Focus();
                return;
            }

            if (cmbProvince.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a province.");
                cmbProvince.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please enter a description.");
                txtDescription.Focus();
                return;
            }

            try
            {
                ServiceRequest request =
                    ServiceRequestSqlService.CreateRequest(
                        CurrentUser.PersonID,
                        cmbServices.SelectedItem.ToString(),
                        cmbProvider.SelectedItem.ToString(),
                        txtAddressline1.Text.Trim(),
                        txtAddressline2.Text.Trim(),
                        cmbCity.Text.Trim(),
                        dtpServiceDate.Value.Date,
                        txtDescription.Text.Trim()
                    );



                MessageBox.Show(
                    "Service request submitted successfully!\n\n" +
                    "Request ID: " + request.RequestID + "\n" +
                    "Provider: " + request.Provider + "\n" +
                    "Service: " + request.Service + "\n" +
                    lblPrice.Text + "\n" +
                    lblVAT.Text + "\n" +
                    lblTotal.Text + "\n" +
                    "Address Line 1: " + request.Addressline1 + "\n" +
                    "Address Line 2: " + request.Addressline2 + "\n" +
                    "City: " + cmbCity.Text.Trim() + "\n" +
                    "Province: " + cmbProvince.Text.Trim() + "\n" +
                    "Service Date: " +
                    request.ServiceDate.ToShortDateString() + "\n" +
                    "Description: " + request.Description,
                    "Request Submitted"
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not save the service request.\n\n" +
                    ex.Message,
                    "Database Error"
                );
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var ownerClient = this.Owner as ClientForm;

            if (ownerClient != null)
            {
                ownerClient.Show();
                ownerClient.BringToFront();
            }
            else
            {
                var openClient =
                    Application.OpenForms
                    .OfType<ClientForm>()
                    .FirstOrDefault();

                if (openClient != null)
                {
                    openClient.Show();
                    openClient.BringToFront();
                }
                else
                {
                    var newClient = new ClientForm();
                    newClient.Show();
                }
            }

            this.Hide();
        }

        private void cmbProvider_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            cmbServices.Items.Clear();

            if (cmbProvider.SelectedItem == null)
            {
                return;
            }

            string selectedProvider =
                cmbProvider.SelectedItem.ToString();

            try
            {
                List<string> services =
                    ProviderServiceSqlService.GetServicesForProvider(
                        selectedProvider
                    );

                foreach (string service in services)
                {
                    cmbServices.Items.Add(service);
                }

                cmbServices.SelectedIndex = -1;

                lblPrice.Text = "Price: R0.00";
                lblVAT.Text = "VAT (15%): R0.00";
                lblTotal.Text = "Total: R0.00";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load services for the selected provider.\n\n" +
                    ex.Message,
                    "Database Error"
                );
            }
        }

        private void cmbServices_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (cmbProvider.SelectedItem == null ||
                cmbServices.SelectedItem == null)
            {
                return;
            }

            string selectedProvider =
                cmbProvider.SelectedItem.ToString();

            string selectedService =
                cmbServices.SelectedItem.ToString();

            try
            {
                decimal price =
                    ProviderServiceSqlService.GetServicePrice(
                        selectedProvider,
                        selectedService
                    );

                decimal VAT = price * 0.15m;
                decimal Total = price + VAT;

                lblPrice.Text =
                    "Price: R" + price.ToString("N2");

                lblVAT.Text =
                    "VAT (15%): R" + VAT.ToString("N2");

                lblTotal.Text =
                    "Total: R" + Total.ToString("N2");
            }
            catch (Exception ex)
            {
                lblPrice.Text = "Price: R0.00";
                lblVAT.Text = "VAT (15%): R0.00";
                lblTotal.Text = "Total: R0.00";

                MessageBox.Show(
                    ex.Message,
                    "Price Information"
                );
            }
        }



        private void ServiceRequetsForm_Load(
    object sender,
    EventArgs e)
        {
            try
            {
                LoadProvinces();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load provinces.\n\n" + ex.Message,
                    "Database Error"
                );
            }
        }

        private void cmbProvince_SelectedIndexChanged(
    object sender,
    EventArgs e)
        {
            if (cmbProvince.SelectedValue == null ||
                cmbProvince.SelectedValue is DataRowView)
            {
                cmbCity.DataSource = null;
                cmbCity.Items.Clear();
                cmbCity.Enabled = false;
                return;
            }

            if (!int.TryParse(
                cmbProvince.SelectedValue.ToString(),
                out int provinceID))
            {
                cmbCity.DataSource = null;
                cmbCity.Items.Clear();
                cmbCity.Enabled = false;
                return;
            }

            try
            {
                LoadCities(provinceID);
            }
            catch (Exception ex)
            {
                cmbCity.DataSource = null;
                cmbCity.Items.Clear();
                cmbCity.Enabled = false;

                MessageBox.Show(
                    "Could not load cities.\n\n" + ex.Message,
                    "Database Error"
                );
            }
        }

        private void LoadProvinces()
        {
            DataTable dt = new DataTable();

            string query = @"
        SELECT PROV_ID, Prov_Name
        FROM Provinces
        ORDER BY Prov_Name";

            using (SqlConnection con =
                   DatabaseConnection.GetConnection())
            using (SqlDataAdapter adapter =
                   new SqlDataAdapter(query, con))
            {
                adapter.Fill(dt);
            }

            cmbProvince.DataSource = null;
            cmbProvince.Items.Clear();

            cmbProvince.DisplayMember = "Prov_Name";
            cmbProvince.ValueMember = "PROV_ID";
            cmbProvince.DataSource = dt;
            cmbProvince.SelectedIndex = -1;

            cmbCity.DataSource = null;
            cmbCity.Items.Clear();
            cmbCity.Enabled = false;
        }

        private void LoadCities(int provinceID)
        {
            DataTable dt = new DataTable();

            string query = @"
        SELECT CIT_ID, City_Name
        FROM Cities
        WHERE PROV_ID = @PROV_ID
        ORDER BY City_Name";

            using (SqlConnection con =
                   DatabaseConnection.GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@PROV_ID", provinceID);

                using (SqlDataAdapter adapter =
                       new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            cmbCity.DataSource = null;
            cmbCity.Items.Clear();

            cmbCity.DisplayMember = "City_Name";
            cmbCity.ValueMember = "CIT_ID";
            cmbCity.DataSource = dt;
            cmbCity.SelectedIndex = -1;
            cmbCity.Enabled = dt.Rows.Count > 0;
        }
    }
}
            