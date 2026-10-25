namespace WindowsFormsApp1
{
    partial class ClerkReviewRequestForm
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
            this.lblReviews = new System.Windows.Forms.Label();
            this.dgvReviews = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtClerkResponse = new System.Windows.Forms.TextBox();
            this.btnRespond = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.cmbReviewsStatus = new System.Windows.Forms.ComboBox();
            this.ReviewID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClientID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Client = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReviewDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClerkResponse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResponseDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReviews
            // 
            this.lblReviews.AutoSize = true;
            this.lblReviews.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReviews.Location = new System.Drawing.Point(318, 24);
            this.lblReviews.Name = "lblReviews";
            this.lblReviews.Size = new System.Drawing.Size(153, 16);
            this.lblReviews.TabIndex = 0;
            this.lblReviews.Text = "REVIEW REQUESTS";
            // 
            // dgvReviews
            // 
            this.dgvReviews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReviews.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReviewID,
            this.ClientID,
            this.Client,
            this.Description,
            this.Rating,
            this.ReviewDate,
            this.Status,
            this.ClerkResponse,
            this.ResponseDate});
            this.dgvReviews.Location = new System.Drawing.Point(46, 65);
            this.dgvReviews.MultiSelect = false;
            this.dgvReviews.Name = "dgvReviews";
            this.dgvReviews.ReadOnly = true;
            this.dgvReviews.RowHeadersWidth = 51;
            this.dgvReviews.RowTemplate.Height = 24;
            this.dgvReviews.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReviews.Size = new System.Drawing.Size(432, 300);
            this.dgvReviews.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(525, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Clerk Response:";
            // 
            // txtClerkResponse
            // 
            this.txtClerkResponse.Location = new System.Drawing.Point(638, 86);
            this.txtClerkResponse.Multiline = true;
            this.txtClerkResponse.Name = "txtClerkResponse";
            this.txtClerkResponse.Size = new System.Drawing.Size(130, 119);
            this.txtClerkResponse.TabIndex = 3;
            // 
            // btnRespond
            // 
            this.btnRespond.Location = new System.Drawing.Point(638, 263);
            this.btnRespond.Name = "btnRespond";
            this.btnRespond.Size = new System.Drawing.Size(104, 23);
            this.btnRespond.TabIndex = 4;
            this.btnRespond.Text = "Respond";
            this.btnRespond.UseVisualStyleBackColor = true;
            this.btnRespond.Click += new System.EventHandler(this.btnRespond_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(693, 403);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 5;
            this.btnBack.Text = "back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // cmbReviewsStatus
            // 
            this.cmbReviewsStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReviewsStatus.FormattingEnabled = true;
            this.cmbReviewsStatus.Items.AddRange(new object[] {
            "Pending",
            "Reviewed",
            "Responding",
            "Closed"});
            this.cmbReviewsStatus.Location = new System.Drawing.Point(201, 381);
            this.cmbReviewsStatus.Name = "cmbReviewsStatus";
            this.cmbReviewsStatus.Size = new System.Drawing.Size(121, 24);
            this.cmbReviewsStatus.TabIndex = 6;
            // 
            // ReviewID
            // 
            this.ReviewID.HeaderText = "Review ID";
            this.ReviewID.MinimumWidth = 6;
            this.ReviewID.Name = "ReviewID";
            this.ReviewID.ReadOnly = true;
            this.ReviewID.Width = 125;
            // 
            // ClientID
            // 
            this.ClientID.HeaderText = "Client ID";
            this.ClientID.MinimumWidth = 6;
            this.ClientID.Name = "ClientID";
            this.ClientID.ReadOnly = true;
            this.ClientID.Width = 125;
            // 
            // Client
            // 
            this.Client.HeaderText = "Client";
            this.Client.MinimumWidth = 6;
            this.Client.Name = "Client";
            this.Client.ReadOnly = true;
            this.Client.Width = 125;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.MinimumWidth = 6;
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 125;
            // 
            // Rating
            // 
            this.Rating.HeaderText = "Rating";
            this.Rating.MinimumWidth = 6;
            this.Rating.Name = "Rating";
            this.Rating.ReadOnly = true;
            this.Rating.Width = 125;
            // 
            // ReviewDate
            // 
            this.ReviewDate.HeaderText = "Review Date";
            this.ReviewDate.MinimumWidth = 6;
            this.ReviewDate.Name = "ReviewDate";
            this.ReviewDate.ReadOnly = true;
            this.ReviewDate.Width = 125;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 125;
            // 
            // ClerkResponse
            // 
            this.ClerkResponse.HeaderText = "Clerk Response";
            this.ClerkResponse.MinimumWidth = 6;
            this.ClerkResponse.Name = "ClerkResponse";
            this.ClerkResponse.ReadOnly = true;
            this.ClerkResponse.Width = 125;
            // 
            // ResponseDate
            // 
            this.ResponseDate.HeaderText = "Response Date";
            this.ResponseDate.MinimumWidth = 6;
            this.ResponseDate.Name = "ResponseDate";
            this.ResponseDate.ReadOnly = true;
            this.ResponseDate.Width = 125;
            // 
            // ClerkReviewRequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbReviewsStatus);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnRespond);
            this.Controls.Add(this.txtClerkResponse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvReviews);
            this.Controls.Add(this.lblReviews);
            this.Name = "ClerkReviewRequestForm";
            this.Text = "ClerkReviewRequestForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReviews;
        private System.Windows.Forms.DataGridView dgvReviews;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClerkResponse;
        private System.Windows.Forms.Button btnRespond;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ComboBox cmbReviewsStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReviewID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClientID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rating;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReviewDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClerkResponse;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResponseDate;
    }
}