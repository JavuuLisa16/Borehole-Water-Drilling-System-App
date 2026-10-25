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
    public partial class MyProfileTechnicianForm : Form
    {
        public MyProfileTechnicianForm()
        {
            InitializeComponent();
            AppTheme.ApplyToForm(this);
            StyleTechnicianProfileForm();
        }

        private void StyleTechnicianProfileForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
