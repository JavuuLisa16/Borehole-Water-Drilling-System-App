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
    public partial class ClerkClientsForm : Form
    {
        public ClerkClientsForm()
        {
            InitializeComponent();
            LoadBookedClients();
            AppTheme.ApplyToForm(this);
            StyleClerkClientsForm();
        }
        private void StyleClerkClientsForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void LoadBookedClients()
        {
            dgvClients.Rows.Clear();

            foreach (ClientProfile client in ClientSqlService.GetAllClients())
            {
                dgvClients.Rows.Add(
                    client.ClientID,
                    client.FullName,
                    client.Gender,
                    client.Email,
                    client.Phone,
                    client.Username,
                    client.Location
                );
            }
        }

        // Other button/event methods go here

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
