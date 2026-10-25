namespace WindowsFormsApp1
{
    partial class ClerkServiceRequestsForm
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
            this.lblServiceRequests = new System.Windows.Forms.Label();
            this.dgvRequests = new System.Windows.Forms.DataGridView();
            this.ClientID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Client = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Service = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Provider = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Addressline1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Addressline2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Province = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.cmbRequestStatus = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).BeginInit();
            this.SuspendLayout();
            // 
            // lblServiceRequests
            // 
            this.lblServiceRequests.AutoSize = true;
            this.lblServiceRequests.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceRequests.Location = new System.Drawing.Point(294, 22);
            this.lblServiceRequests.Name = "lblServiceRequests";
            this.lblServiceRequests.Size = new System.Drawing.Size(130, 16);
            this.lblServiceRequests.TabIndex = 0;
            this.lblServiceRequests.Text = "Service Requests";
            // 
            // dgvRequests
            // 
            this.dgvRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRequests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ClientID,
            this.RequestID,
            this.Client,
            this.Service,
            this.Provider,
            this.Addressline1,
            this.Addressline2,
            this.City,
            this.Province,
            this.RequestDate,
            this.ServiceDate,
            this.Description,
            this.Status});
            this.dgvRequests.Location = new System.Drawing.Point(61, 68);
            this.dgvRequests.MultiSelect = false;
            this.dgvRequests.Name = "dgvRequests";
            this.dgvRequests.RowHeadersWidth = 51;
            this.dgvRequests.RowTemplate.Height = 24;
            this.dgvRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRequests.Size = new System.Drawing.Size(494, 296);
            this.dgvRequests.TabIndex = 1;
            // 
            // ClientID
            // 
            this.ClientID.HeaderText = "Client ID";
            this.ClientID.MinimumWidth = 6;
            this.ClientID.Name = "ClientID";
            this.ClientID.Width = 125;
            // 
            // RequestID
            // 
            this.RequestID.HeaderText = "Request ID";
            this.RequestID.MinimumWidth = 6;
            this.RequestID.Name = "RequestID";
            this.RequestID.Width = 125;
            // 
            // Client
            // 
            this.Client.HeaderText = "Client";
            this.Client.MinimumWidth = 6;
            this.Client.Name = "Client";
            this.Client.Width = 125;
            // 
            // Service
            // 
            this.Service.HeaderText = "Service";
            this.Service.MinimumWidth = 6;
            this.Service.Name = "Service";
            this.Service.Width = 125;
            // 
            // Provider
            // 
            this.Provider.HeaderText = "Provider";
            this.Provider.MinimumWidth = 6;
            this.Provider.Name = "Provider";
            this.Provider.Width = 125;
            // 
            // Addressline1
            // 
            this.Addressline1.HeaderText = "Address line 1";
            this.Addressline1.MinimumWidth = 6;
            this.Addressline1.Name = "Addressline1";
            this.Addressline1.Width = 125;
            // 
            // Addressline2
            // 
            this.Addressline2.HeaderText = "Address line 2";
            this.Addressline2.MinimumWidth = 6;
            this.Addressline2.Name = "Addressline2";
            this.Addressline2.Width = 125;
            // 
            // City
            // 
            this.City.HeaderText = "City";
            this.City.MinimumWidth = 6;
            this.City.Name = "City";
            this.City.Width = 125;
            // 
            // Province
            // 
            this.Province.HeaderText = "Province";
            this.Province.MinimumWidth = 6;
            this.Province.Name = "Province";
            this.Province.Width = 125;
            // 
            // RequestDate
            // 
            this.RequestDate.HeaderText = "Request Date";
            this.RequestDate.MinimumWidth = 6;
            this.RequestDate.Name = "RequestDate";
            this.RequestDate.Width = 125;
            // 
            // ServiceDate
            // 
            this.ServiceDate.HeaderText = "Service Date";
            this.ServiceDate.MinimumWidth = 6;
            this.ServiceDate.Name = "ServiceDate";
            this.ServiceDate.Width = 125;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.MinimumWidth = 6;
            this.Description.Name = "Description";
            this.Description.Width = 125;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.Width = 125;
            // 
            // btnUpdateStatus
            // 
            this.btnUpdateStatus.Location = new System.Drawing.Point(656, 192);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(115, 23);
            this.btnUpdateStatus.TabIndex = 3;
            this.btnUpdateStatus.Text = "Update Status";
            this.btnUpdateStatus.UseVisualStyleBackColor = true;
            this.btnUpdateStatus.Click += new System.EventHandler(this.btnUpdateStatus_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(696, 415);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // cmbRequestStatus
            // 
            this.cmbRequestStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRequestStatus.FormattingEnabled = true;
            this.cmbRequestStatus.Items.AddRange(new object[] {
            "Pending",
            "Approved",
            "Rejected",
            "In Progress",
            "Completed"});
            this.cmbRequestStatus.Location = new System.Drawing.Point(650, 86);
            this.cmbRequestStatus.Name = "cmbRequestStatus";
            this.cmbRequestStatus.Size = new System.Drawing.Size(121, 24);
            this.cmbRequestStatus.TabIndex = 5;
            
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(696, 254);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // ClerkServiceRequestsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.cmbRequestStatus);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnUpdateStatus);
            this.Controls.Add(this.dgvRequests);
            this.Controls.Add(this.lblServiceRequests);
            this.Name = "ClerkServiceRequestsForm";
            this.Text = "ClientServiceRequestsForm";
            this.Activated += new System.EventHandler(this.ClerkServiceRequestsForm_Activated_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblServiceRequests;
        private System.Windows.Forms.DataGridView dgvRequests;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ComboBox cmbRequestStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationColumn;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClientID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client;
        private System.Windows.Forms.DataGridViewTextBoxColumn Service;
        private System.Windows.Forms.DataGridViewTextBoxColumn Provider;
        private System.Windows.Forms.DataGridViewTextBoxColumn Addressline1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Addressline2;
        private System.Windows.Forms.DataGridViewTextBoxColumn City;
        private System.Windows.Forms.DataGridViewTextBoxColumn Province;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}