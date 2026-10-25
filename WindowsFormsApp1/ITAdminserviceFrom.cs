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
    public partial class ITAdminserviceFrom : Form
    {
        public ITAdminserviceFrom()
        {
            InitializeComponent();
            AppTheme.ApplyToForm(this);
            StyleAdminServicesForm();
        }
        private void StyleAdminServicesForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            if (dgvServices.SelectedRows.Count == 0) {
                MessageBox.Show("Please select a service.");
                return;
            }
            DataGridViewRow row = dgvServices.SelectedRows[0];
            if (!row.IsNewRow)
            {
                row.Cells["ServiceName"].Value = "";
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int nextNumber = 1;
            foreach (DataGridViewRow row in dgvServices.Rows)
            {
                if (row.IsNewRow) continue;
                String ID = row.Cells["ServiceID"].Value?.ToString();
                if (!string.IsNullOrEmpty(ID) && ID.StartsWith("S"))
                {
                    if (int.TryParse(ID.Substring(1), out int number))
                    {
                        if (number >= nextNumber)
                            nextNumber = number + 1;
                    }
                }
            }
            string newServiceID = "S" + nextNumber.ToString("D3");
            dgvServices.Rows.Add(newServiceID, "");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvServices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a service.");
                return;
            }
            if (!dgvServices.SelectedRows[0].IsNewRow)
            {
                dgvServices.Rows.RemoveAt(dgvServices.SelectedRows[0].Index);
            }
        }
    }
}