namespace WindowsFormsApp1
{
    partial class TechnicianAssignments
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
            this.dgvAssignment = new System.Windows.Forms.DataGridView();
            this.AssignmentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TechnicianID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssignmentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnSiteAssessment = new System.Windows.Forms.Button();
            this.cmbAssignmentStatus = new System.Windows.Forms.ComboBox();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.btnSiteVisit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssignment)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(331, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "My Assignment";
            // 
            // dgvAssignment
            // 
            this.dgvAssignment.AllowUserToAddRows = false;
            this.dgvAssignment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAssignment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AssignmentID,
            this.RequestID,
            this.TechnicianID,
            this.AssignmentDate,
            this.Status});
            this.dgvAssignment.Location = new System.Drawing.Point(31, 82);
            this.dgvAssignment.Name = "dgvAssignment";
            this.dgvAssignment.ReadOnly = true;
            this.dgvAssignment.RowHeadersWidth = 51;
            this.dgvAssignment.RowTemplate.Height = 24;
            this.dgvAssignment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAssignment.Size = new System.Drawing.Size(570, 326);
            this.dgvAssignment.TabIndex = 1;
            this.dgvAssignment.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAssignment_CellClick);
            // 
            // AssignmentID
            // 
            this.AssignmentID.HeaderText = "Assignment ID";
            this.AssignmentID.MinimumWidth = 6;
            this.AssignmentID.Name = "AssignmentID";
            this.AssignmentID.ReadOnly = true;
            this.AssignmentID.Width = 125;
            // 
            // RequestID
            // 
            this.RequestID.HeaderText = "Request ID";
            this.RequestID.MinimumWidth = 6;
            this.RequestID.Name = "RequestID";
            this.RequestID.ReadOnly = true;
            this.RequestID.Width = 125;
            // 
            // TechnicianID
            // 
            this.TechnicianID.HeaderText = "Technician ID";
            this.TechnicianID.MinimumWidth = 6;
            this.TechnicianID.Name = "TechnicianID";
            this.TechnicianID.ReadOnly = true;
            this.TechnicianID.Width = 125;
            // 
            // AssignmentDate
            // 
            this.AssignmentDate.HeaderText = "Assignment Date";
            this.AssignmentDate.MinimumWidth = 6;
            this.AssignmentDate.Name = "AssignmentDate";
            this.AssignmentDate.ReadOnly = true;
            this.AssignmentDate.Width = 125;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 125;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(719, 404);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnSiteAssessment
            // 
            this.btnSiteAssessment.Location = new System.Drawing.Point(645, 317);
            this.btnSiteAssessment.Name = "btnSiteAssessment";
            this.btnSiteAssessment.Size = new System.Drawing.Size(143, 23);
            this.btnSiteAssessment.TabIndex = 3;
            this.btnSiteAssessment.Text = "Site Assessment";
            this.btnSiteAssessment.UseVisualStyleBackColor = true;
            this.btnSiteAssessment.Click += new System.EventHandler(this.btnSiteVisit_Click);
            // 
            // cmbAssignmentStatus
            // 
            this.cmbAssignmentStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssignmentStatus.FormattingEnabled = true;
            this.cmbAssignmentStatus.Items.AddRange(new object[] {
            "Assigned",
            "In progress",
            "Completed",
            "On Hold"});
            this.cmbAssignmentStatus.Location = new System.Drawing.Point(667, 82);
            this.cmbAssignmentStatus.Name = "cmbAssignmentStatus";
            this.cmbAssignmentStatus.Size = new System.Drawing.Size(121, 24);
            this.cmbAssignmentStatus.TabIndex = 4;
            // 
            // btnUpdateStatus
            // 
            this.btnUpdateStatus.Location = new System.Drawing.Point(655, 251);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(133, 23);
            this.btnUpdateStatus.TabIndex = 5;
            this.btnUpdateStatus.Text = "Update Status";
            this.btnUpdateStatus.UseVisualStyleBackColor = true;
            this.btnUpdateStatus.Click += new System.EventHandler(this.btnUpdateStatus_Click);
            // 
            // btnSiteVisit
            // 
            this.btnSiteVisit.Location = new System.Drawing.Point(645, 360);
            this.btnSiteVisit.Name = "btnSiteVisit";
            this.btnSiteVisit.Size = new System.Drawing.Size(143, 23);
            this.btnSiteVisit.TabIndex = 6;
            this.btnSiteVisit.Text = "Site Visit";
            this.btnSiteVisit.UseVisualStyleBackColor = true;
            this.btnSiteVisit.Click += new System.EventHandler(this.btnSitev_Click);
            // 
            // TechnicianAssignments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSiteVisit);
            this.Controls.Add(this.btnUpdateStatus);
            this.Controls.Add(this.cmbAssignmentStatus);
            this.Controls.Add(this.btnSiteAssessment);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dgvAssignment);
            this.Controls.Add(this.label1);
            this.Name = "TechnicianAssignments";
            this.Text = "TechnicianAssignments";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssignment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvAssignment;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssignmentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TechnicianID;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssignmentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Button btnSiteAssessment;
        private System.Windows.Forms.ComboBox cmbAssignmentStatus;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.Button btnSiteVisit;
    }
}