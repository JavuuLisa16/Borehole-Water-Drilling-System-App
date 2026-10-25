namespace WindowsFormsApp1
{
    partial class OrganisationTechnicianForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvTechnician = new System.Windows.Forms.DataGridView();
            this.TechnicianID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TechnicianName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Specialisation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvServiceRequests = new System.Windows.Forms.DataGridView();
            this.RequestID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClientID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Client = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Service = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Provider = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnViewRequest = new System.Windows.Forms.Button();
            this.btnAssignTechnician = new System.Windows.Forms.Button();
            this.btnback = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTechnician)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiceRequests)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(255, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Technician & Service Requests";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Drilling Service Provider:";
            // 
            // cmbProvider
            // 
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Items.AddRange(new object[] {
            "AB Pumps",
            "Vula Amanzi Borehole Drilling",
            "Xomoyi Borehole Drilling"});
            this.cmbProvider.Location = new System.Drawing.Point(225, 77);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(121, 24);
            this.cmbProvider.TabIndex = 2;
            this.cmbProvider.SelectedIndexChanged += new System.EventHandler(this.cmbProvider_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Technician";
            // 
            // dgvTechnician
            // 
            this.dgvTechnician.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTechnician.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TechnicianID,
            this.TechnicianName,
            this.Phone,
            this.Specialisation,
            this.Status});
            this.dgvTechnician.Location = new System.Drawing.Point(38, 155);
            this.dgvTechnician.MultiSelect = false;
            this.dgvTechnician.Name = "dgvTechnician";
            this.dgvTechnician.RowHeadersWidth = 51;
            this.dgvTechnician.RowTemplate.Height = 24;
            this.dgvTechnician.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTechnician.Size = new System.Drawing.Size(266, 229);
            this.dgvTechnician.TabIndex = 4;
            // 
            // TechnicianID
            // 
            this.TechnicianID.HeaderText = "Technician ID";
            this.TechnicianID.MinimumWidth = 6;
            this.TechnicianID.Name = "TechnicianID";
            this.TechnicianID.Width = 125;
            // 
            // TechnicianName
            // 
            this.TechnicianName.HeaderText = "Technician Name";
            this.TechnicianName.MinimumWidth = 6;
            this.TechnicianName.Name = "TechnicianName";
            this.TechnicianName.Width = 125;
            // 
            // Phone
            // 
            this.Phone.HeaderText = "Phone";
            this.Phone.MinimumWidth = 6;
            this.Phone.Name = "Phone";
            this.Phone.Width = 125;
            // 
            // Specialisation
            // 
            this.Specialisation.HeaderText = "Specialisation";
            this.Specialisation.MinimumWidth = 6;
            this.Specialisation.Name = "Specialisation";
            this.Specialisation.Width = 125;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.Width = 125;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(576, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Service Requests";
            // 
            // dgvServiceRequests
            // 
            this.dgvServiceRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServiceRequests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RequestID,
            this.ClientID,
            this.Client,
            this.Service,
            this.Provider,
            this.LocationColumn,
            this.RequestDate,
            this.dataGridViewTextBoxColumn1});
            this.dgvServiceRequests.Location = new System.Drawing.Point(507, 155);
            this.dgvServiceRequests.MultiSelect = false;
            this.dgvServiceRequests.Name = "dgvServiceRequests";
            this.dgvServiceRequests.RowHeadersWidth = 51;
            this.dgvServiceRequests.RowTemplate.Height = 24;
            this.dgvServiceRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServiceRequests.Size = new System.Drawing.Size(281, 229);
            this.dgvServiceRequests.TabIndex = 6;
            // 
            // RequestID
            // 
            this.RequestID.HeaderText = "Request ID";
            this.RequestID.MinimumWidth = 6;
            this.RequestID.Name = "RequestID";
            this.RequestID.Width = 125;
            // 
            // ClientID
            // 
            this.ClientID.HeaderText = "Client ID";
            this.ClientID.MinimumWidth = 6;
            this.ClientID.Name = "ClientID";
            this.ClientID.Width = 125;
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
            // LocationColumn
            // 
            this.LocationColumn.HeaderText = "Location";
            this.LocationColumn.MinimumWidth = 6;
            this.LocationColumn.Name = "Location";
            this.LocationColumn.Width = 125;
            // 
            // RequestDate
            // 
            this.RequestDate.HeaderText = "Request Date";
            this.RequestDate.MinimumWidth = 6;
            this.RequestDate.Name = "RequestDate";
            this.RequestDate.Width = 125;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Status";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(379, 155);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 38);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnViewRequest
            // 
            this.btnViewRequest.Location = new System.Drawing.Point(379, 214);
            this.btnViewRequest.Name = "btnViewRequest";
            this.btnViewRequest.Size = new System.Drawing.Size(75, 57);
            this.btnViewRequest.TabIndex = 8;
            this.btnViewRequest.Text = "View Request";
            this.btnViewRequest.UseVisualStyleBackColor = true;
            this.btnViewRequest.Click += new System.EventHandler(this.btnViewRequest_Click);
            // 
            // btnAssignTechnician
            // 
            this.btnAssignTechnician.Location = new System.Drawing.Point(355, 293);
            this.btnAssignTechnician.Name = "btnAssignTechnician";
            this.btnAssignTechnician.Size = new System.Drawing.Size(114, 48);
            this.btnAssignTechnician.TabIndex = 9;
            this.btnAssignTechnician.Text = "Assign Technician";
            this.btnAssignTechnician.UseVisualStyleBackColor = true;
            this.btnAssignTechnician.Click += new System.EventHandler(this.btnAssignTechnician_Click);
            // 
            // btnback
            // 
            this.btnback.Location = new System.Drawing.Point(688, 415);
            this.btnback.Name = "btnback";
            this.btnback.Size = new System.Drawing.Size(75, 23);
            this.btnback.TabIndex = 10;
            this.btnback.Text = "back";
            this.btnback.UseVisualStyleBackColor = true;
            this.btnback.Click += new System.EventHandler(this.btnback_Click);
            // 
            // OrganisationTechnicianForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnback);
            this.Controls.Add(this.btnAssignTechnician);
            this.Controls.Add(this.btnViewRequest);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvServiceRequests);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvTechnician);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbProvider);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "OrganisationTechnicianForm";
            this.Text = "OrganisationTechnicianForm";
            this.Activated += new System.EventHandler(this.OrganisationTechnicianForm_Activated);
            this.Load += new System.EventHandler(this.OrganisationTechnicianForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTechnician)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiceRequests)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvTechnician;
        private System.Windows.Forms.DataGridViewTextBoxColumn TechnicianID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TechnicianName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn Specialisation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvServiceRequests;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnViewRequest;
        private System.Windows.Forms.Button btnAssignTechnician;
        private System.Windows.Forms.Button btnback;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClientID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client;
        private System.Windows.Forms.DataGridViewTextBoxColumn Service;
        private System.Windows.Forms.DataGridViewTextBoxColumn Provider;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

    }
}