namespace WindowsFormsApp1
{
    partial class TechnicianForm
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
            this.lblTehnician = new System.Windows.Forms.Label();
            this.btnAssignments = new System.Windows.Forms.Button();
            this.btnTechProfile = new System.Windows.Forms.Button();
            this.btnFieldData = new System.Windows.Forms.Button();
            this.btnSiteVisits = new System.Windows.Forms.Button();
            this.btnLogout2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTehnician
            // 
            this.lblTehnician.AutoSize = true;
            this.lblTehnician.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTehnician.Location = new System.Drawing.Point(281, 23);
            this.lblTehnician.Name = "lblTehnician";
            this.lblTehnician.Size = new System.Drawing.Size(174, 18);
            this.lblTehnician.TabIndex = 0;
            this.lblTehnician.Text = "Welcome, Technician!";
            // 
            // btnAssignments
            // 
            this.btnAssignments.Location = new System.Drawing.Point(56, 98);
            this.btnAssignments.Name = "btnAssignments";
            this.btnAssignments.Size = new System.Drawing.Size(144, 31);
            this.btnAssignments.TabIndex = 1;
            this.btnAssignments.Text = "My Assignments";
            this.btnAssignments.UseVisualStyleBackColor = true;
            this.btnAssignments.Click += new System.EventHandler(this.btnAssignments_Click);
            // 
            // btnTechProfile
            // 
            this.btnTechProfile.Location = new System.Drawing.Point(56, 308);
            this.btnTechProfile.Name = "btnTechProfile";
            this.btnTechProfile.Size = new System.Drawing.Size(144, 26);
            this.btnTechProfile.TabIndex = 2;
            this.btnTechProfile.Text = "My Profile";
            this.btnTechProfile.UseVisualStyleBackColor = true;
            this.btnTechProfile.Click += new System.EventHandler(this.btnTechProfile_Click);
            // 
            // btnFieldData
            // 
            this.btnFieldData.Location = new System.Drawing.Point(56, 226);
            this.btnFieldData.Name = "btnFieldData";
            this.btnFieldData.Size = new System.Drawing.Size(144, 23);
            this.btnFieldData.TabIndex = 3;
            this.btnFieldData.Text = "Field Data";
            this.btnFieldData.UseVisualStyleBackColor = true;
            this.btnFieldData.Click += new System.EventHandler(this.btnFieldData_Click);
            // 
            // btnSiteVisits
            // 
            this.btnSiteVisits.Location = new System.Drawing.Point(56, 161);
            this.btnSiteVisits.Name = "btnSiteVisits";
            this.btnSiteVisits.Size = new System.Drawing.Size(144, 23);
            this.btnSiteVisits.TabIndex = 4;
            this.btnSiteVisits.Text = "Site Visits";
            this.btnSiteVisits.UseVisualStyleBackColor = true;
            this.btnSiteVisits.Click += new System.EventHandler(this.btnSiteVisits_Click);
            // 
            // btnLogout2
            // 
            this.btnLogout2.Location = new System.Drawing.Point(311, 370);
            this.btnLogout2.Name = "btnLogout2";
            this.btnLogout2.Size = new System.Drawing.Size(144, 26);
            this.btnLogout2.TabIndex = 5;
            this.btnLogout2.Text = "Logout";
            this.btnLogout2.UseVisualStyleBackColor = true;
            this.btnLogout2.Click += new System.EventHandler(this.btnLogout2_Click);
            // 
            // TechnicianForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLogout2);
            this.Controls.Add(this.btnSiteVisits);
            this.Controls.Add(this.btnFieldData);
            this.Controls.Add(this.btnTechProfile);
            this.Controls.Add(this.btnAssignments);
            this.Controls.Add(this.lblTehnician);
            this.Name = "TechnicianForm";
            this.Text = "TechnicianForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTehnician;
        private System.Windows.Forms.Button btnAssignments;
        private System.Windows.Forms.Button btnTechProfile;
        private System.Windows.Forms.Button btnFieldData;
        private System.Windows.Forms.Button btnSiteVisits;
        private System.Windows.Forms.Button btnLogout2;
    }
}