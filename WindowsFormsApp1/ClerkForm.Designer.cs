namespace WindowsFormsApp1
{
    partial class ClerkForm
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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnServiceRequests = new System.Windows.Forms.Button();
            this.btnClients = new System.Windows.Forms.Button();
            this.btnReviewRequests = new System.Windows.Forms.Button();
            this.btnMyProfile = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(288, 9);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(142, 16);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "WELCOME, CLERK!";
            // 
            // btnServiceRequests
            // 
            this.btnServiceRequests.Location = new System.Drawing.Point(244, 89);
            this.btnServiceRequests.Name = "btnServiceRequests";
            this.btnServiceRequests.Size = new System.Drawing.Size(216, 37);
            this.btnServiceRequests.TabIndex = 1;
            this.btnServiceRequests.Text = "Service Requests";
            this.btnServiceRequests.UseVisualStyleBackColor = true;
            this.btnServiceRequests.Click += new System.EventHandler(this.btnServiceRequests_Click);
            // 
            // btnClients
            // 
            this.btnClients.Location = new System.Drawing.Point(291, 146);
            this.btnClients.Name = "btnClients";
            this.btnClients.Size = new System.Drawing.Size(141, 37);
            this.btnClients.TabIndex = 2;
            this.btnClients.Text = "Clients";
            this.btnClients.UseVisualStyleBackColor = true;
            this.btnClients.Click += new System.EventHandler(this.btnClients_Click);
            // 
            // btnReviewRequests
            // 
            this.btnReviewRequests.Location = new System.Drawing.Point(256, 221);
            this.btnReviewRequests.Name = "btnReviewRequests";
            this.btnReviewRequests.Size = new System.Drawing.Size(204, 47);
            this.btnReviewRequests.TabIndex = 3;
            this.btnReviewRequests.Text = "Review Requests";
            this.btnReviewRequests.UseVisualStyleBackColor = true;
            this.btnReviewRequests.Click += new System.EventHandler(this.btnReviewRequests_Click);
            // 
            // btnMyProfile
            // 
            this.btnMyProfile.Location = new System.Drawing.Point(301, 302);
            this.btnMyProfile.Name = "btnMyProfile";
            this.btnMyProfile.Size = new System.Drawing.Size(129, 39);
            this.btnMyProfile.TabIndex = 4;
            this.btnMyProfile.Text = "My Profile";
            this.btnMyProfile.UseVisualStyleBackColor = true;
            this.btnMyProfile.Click += new System.EventHandler(this.btnMyProfile_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(643, 406);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(103, 32);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // ClerkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnMyProfile);
            this.Controls.Add(this.btnReviewRequests);
            this.Controls.Add(this.btnClients);
            this.Controls.Add(this.btnServiceRequests);
            this.Controls.Add(this.lblWelcome);
            this.Name = "ClerkForm";
            this.Text = "ClerkForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnServiceRequests;
        private System.Windows.Forms.Button btnClients;
        private System.Windows.Forms.Button btnReviewRequests;
        private System.Windows.Forms.Button btnMyProfile;
        private System.Windows.Forms.Button btnLogout;
    }
}