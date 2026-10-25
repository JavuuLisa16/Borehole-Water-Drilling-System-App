namespace WindowsFormsApp1
{
    partial class ClientForm
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
            this.btnBookService = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnMyRequets = new System.Windows.Forms.Button();
            this.btnMyReviews = new System.Windows.Forms.Button();
            this.btnMyProfile = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBookService
            // 
            this.btnBookService.Location = new System.Drawing.Point(118, 128);
            this.btnBookService.Name = "btnBookService";
            this.btnBookService.Size = new System.Drawing.Size(175, 41);
            this.btnBookService.TabIndex = 0;
            this.btnBookService.Text = "Book Drilling Service";
            this.btnBookService.UseVisualStyleBackColor = true;
            this.btnBookService.Click += new System.EventHandler(this.btnBookService_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(288, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(123, 16);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Welcome, Client!";
            // 
            // btnMyRequets
            // 
            this.btnMyRequets.Location = new System.Drawing.Point(137, 199);
            this.btnMyRequets.Name = "btnMyRequets";
            this.btnMyRequets.Size = new System.Drawing.Size(127, 34);
            this.btnMyRequets.TabIndex = 2;
            this.btnMyRequets.Text = "My Requests";
            this.btnMyRequets.UseVisualStyleBackColor = true;
            this.btnMyRequets.Click += new System.EventHandler(this.btnMyRequets_Click);
            // 
            // btnMyReviews
            // 
            this.btnMyReviews.Location = new System.Drawing.Point(510, 132);
            this.btnMyReviews.Name = "btnMyReviews";
            this.btnMyReviews.Size = new System.Drawing.Size(127, 37);
            this.btnMyReviews.TabIndex = 3;
            this.btnMyReviews.Text = "Reviews";
            this.btnMyReviews.UseVisualStyleBackColor = true;
            this.btnMyReviews.Click += new System.EventHandler(this.btnMyReviews_Click);
            // 
            // btnMyProfile
            // 
            this.btnMyProfile.Location = new System.Drawing.Point(510, 199);
            this.btnMyProfile.Name = "btnMyProfile";
            this.btnMyProfile.Size = new System.Drawing.Size(127, 33);
            this.btnMyProfile.TabIndex = 4;
            this.btnMyProfile.Text = "My Profile";
            this.btnMyProfile.UseVisualStyleBackColor = true;
            this.btnMyProfile.Click += new System.EventHandler(this.btnMyProfile_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(316, 301);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(127, 23);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnMyProfile);
            this.Controls.Add(this.btnMyReviews);
            this.Controls.Add(this.btnMyRequets);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnBookService);
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBookService;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnMyRequets;
        private System.Windows.Forms.Button btnMyReviews;
        private System.Windows.Forms.Button btnMyProfile;
        private System.Windows.Forms.Button btnLogout;
    }
}