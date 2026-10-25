namespace WindowsFormsApp1
{
    partial class ITAdminForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClients = new System.Windows.Forms.Button();
            this.btnClerks = new System.Windows.Forms.Button();
            this.btnTechnicians = new System.Windows.Forms.Button();
            this.btnServices = new System.Windows.Forms.Button();
            this.btnDSPs = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(313, 35);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(231, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ADMINISTRATOR DASHBOARD";
            // 
            // btnClients
            // 
            this.btnClients.Location = new System.Drawing.Point(197, 91);
            this.btnClients.Name = "btnClients";
            this.btnClients.Size = new System.Drawing.Size(135, 33);
            this.btnClients.TabIndex = 1;
            this.btnClients.Text = "Clients";
            this.btnClients.UseVisualStyleBackColor = true;
            this.btnClients.Click += new System.EventHandler(this.btnClients_Click);
            // 
            // btnClerks
            // 
            this.btnClerks.Location = new System.Drawing.Point(197, 148);
            this.btnClerks.Name = "btnClerks";
            this.btnClerks.Size = new System.Drawing.Size(135, 28);
            this.btnClerks.TabIndex = 2;
            this.btnClerks.Text = "Clerks";
            this.btnClerks.UseVisualStyleBackColor = true;
            this.btnClerks.Click += new System.EventHandler(this.btnClerks_Click);
            // 
            // btnTechnicians
            // 
            this.btnTechnicians.Location = new System.Drawing.Point(197, 203);
            this.btnTechnicians.Name = "btnTechnicians";
            this.btnTechnicians.Size = new System.Drawing.Size(135, 29);
            this.btnTechnicians.TabIndex = 3;
            this.btnTechnicians.Text = "Technicians";
            this.btnTechnicians.UseVisualStyleBackColor = true;
            this.btnTechnicians.Click += new System.EventHandler(this.btnTechnicians_Click);
            // 
            // btnServices
            // 
            this.btnServices.Location = new System.Drawing.Point(197, 255);
            this.btnServices.Name = "btnServices";
            this.btnServices.Size = new System.Drawing.Size(135, 28);
            this.btnServices.TabIndex = 4;
            this.btnServices.Text = "Services";
            this.btnServices.UseVisualStyleBackColor = true;
            this.btnServices.Click += new System.EventHandler(this.btnServices_Click);
            // 
            // btnDSPs
            // 
            this.btnDSPs.Location = new System.Drawing.Point(510, 89);
            this.btnDSPs.Name = "btnDSPs";
            this.btnDSPs.Size = new System.Drawing.Size(176, 36);
            this.btnDSPs.TabIndex = 8;
            this.btnDSPs.Text = "Drilling Service Providers";
            this.btnDSPs.UseVisualStyleBackColor = true;
            this.btnDSPs.Click += new System.EventHandler(this.btnDSPs_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(627, 391);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(121, 35);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // ITAdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnDSPs);
            this.Controls.Add(this.btnServices);
            this.Controls.Add(this.btnTechnicians);
            this.Controls.Add(this.btnClerks);
            this.Controls.Add(this.btnClients);
            this.Controls.Add(this.lblTitle);
            this.Name = "ITAdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administrator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClients;
        private System.Windows.Forms.Button btnClerks;
        private System.Windows.Forms.Button btnTechnicians;
        private System.Windows.Forms.Button btnServices;
        private System.Windows.Forms.Button btnDSPs;
        private System.Windows.Forms.Button btnLogout;
    }
}