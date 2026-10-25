namespace WindowsFormsApp1
{
    partial class SiteVisitForm
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
            this.lblRequestID = new System.Windows.Forms.Label();
            this.lblVisitDate = new System.Windows.Forms.Label();
            this.dtpVisitDate = new System.Windows.Forms.DateTimePicker();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtVisitNotes = new System.Windows.Forms.TextBox();
            this.btnSaveVisit = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblAssignmentID = new System.Windows.Forms.Label();
            this.btnFieldData = new System.Windows.Forms.Button();
            this.lblVisitID = new System.Windows.Forms.Label();
            this.dgvSiteVisits = new System.Windows.Forms.DataGridView();
            this.VisitID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VisitDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VisitNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssignID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSiteVisits)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(329, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Site Visit";
            // 
            // lblRequestID
            // 
            this.lblRequestID.AutoSize = true;
            this.lblRequestID.Location = new System.Drawing.Point(75, 84);
            this.lblRequestID.Name = "lblRequestID";
            this.lblRequestID.Size = new System.Drawing.Size(77, 16);
            this.lblRequestID.TabIndex = 1;
            this.lblRequestID.Text = "Request ID:";
            // 
            // lblVisitDate
            // 
            this.lblVisitDate.AutoSize = true;
            this.lblVisitDate.Location = new System.Drawing.Point(78, 152);
            this.lblVisitDate.Name = "lblVisitDate";
            this.lblVisitDate.Size = new System.Drawing.Size(67, 16);
            this.lblVisitDate.TabIndex = 3;
            this.lblVisitDate.Text = "Visit Date:";
            // 
            // dtpVisitDate
            // 
            this.dtpVisitDate.Location = new System.Drawing.Point(175, 152);
            this.dtpVisitDate.Name = "dtpVisitDate";
            this.dtpVisitDate.Size = new System.Drawing.Size(200, 22);
            this.dtpVisitDate.TabIndex = 4;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(81, 212);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(74, 16);
            this.lblNotes.TabIndex = 5;
            this.lblNotes.Text = "Visit Notes:";
            // 
            // txtVisitNotes
            // 
            this.txtVisitNotes.Location = new System.Drawing.Point(175, 212);
            this.txtVisitNotes.Multiline = true;
            this.txtVisitNotes.Name = "txtVisitNotes";
            this.txtVisitNotes.Size = new System.Drawing.Size(121, 80);
            this.txtVisitNotes.TabIndex = 6;
            // 
            // btnSaveVisit
            // 
            this.btnSaveVisit.Location = new System.Drawing.Point(220, 361);
            this.btnSaveVisit.Name = "btnSaveVisit";
            this.btnSaveVisit.Size = new System.Drawing.Size(116, 23);
            this.btnSaveVisit.TabIndex = 7;
            this.btnSaveVisit.Text = "Save Visit";
            this.btnSaveVisit.UseVisualStyleBackColor = true;
            this.btnSaveVisit.Click += new System.EventHandler(this.btnSaveVisit_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(709, 415);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblAssignmentID
            // 
            this.lblAssignmentID.AutoSize = true;
            this.lblAssignmentID.Location = new System.Drawing.Point(471, 83);
            this.lblAssignmentID.Name = "lblAssignmentID";
            this.lblAssignmentID.Size = new System.Drawing.Size(96, 16);
            this.lblAssignmentID.TabIndex = 9;
            this.lblAssignmentID.Text = "Assignment ID:";
            // 
            // btnFieldData
            // 
            this.btnFieldData.Location = new System.Drawing.Point(387, 360);
            this.btnFieldData.Name = "btnFieldData";
            this.btnFieldData.Size = new System.Drawing.Size(136, 23);
            this.btnFieldData.TabIndex = 10;
            this.btnFieldData.Text = "Field Data";
            this.btnFieldData.UseVisualStyleBackColor = true;
            this.btnFieldData.Click += new System.EventHandler(this.btnFieldData_Click);
            // 
            // lblVisitID
            // 
            this.lblVisitID.AutoSize = true;
            this.lblVisitID.Location = new System.Drawing.Point(78, 44);
            this.lblVisitID.Name = "lblVisitID";
            this.lblVisitID.Size = new System.Drawing.Size(48, 16);
            this.lblVisitID.TabIndex = 11;
            this.lblVisitID.Text = "VisitID:";
            // 
            // dgvSiteVisits
            // 
            this.dgvSiteVisits.AllowUserToAddRows = false;
            this.dgvSiteVisits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSiteVisits.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VisitID,
            this.RequestID,
            this.VisitDate,
            this.VisitNotes,
            this.AssignID});
            this.dgvSiteVisits.Location = new System.Drawing.Point(429, 142);
            this.dgvSiteVisits.MultiSelect = false;
            this.dgvSiteVisits.Name = "dgvSiteVisits";
            this.dgvSiteVisits.ReadOnly = true;
            this.dgvSiteVisits.RowHeadersWidth = 51;
            this.dgvSiteVisits.RowTemplate.Height = 24;
            this.dgvSiteVisits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSiteVisits.Size = new System.Drawing.Size(355, 174);
            this.dgvSiteVisits.TabIndex = 12;
            // 
            // VisitID
            // 
            this.VisitID.HeaderText = "Visit ID";
            this.VisitID.MinimumWidth = 6;
            this.VisitID.Name = "VisitID";
            this.VisitID.ReadOnly = true;
            this.VisitID.Width = 125;
            // 
            // RequestID
            // 
            this.RequestID.HeaderText = "Request ID";
            this.RequestID.MinimumWidth = 6;
            this.RequestID.Name = "RequestID";
            this.RequestID.ReadOnly = true;
            this.RequestID.Width = 125;
            // 
            // VisitDate
            // 
            this.VisitDate.HeaderText = "Visit Date";
            this.VisitDate.MinimumWidth = 6;
            this.VisitDate.Name = "VisitDate";
            this.VisitDate.ReadOnly = true;
            this.VisitDate.Width = 125;
            // 
            // VisitNotes
            // 
            this.VisitNotes.HeaderText = "Visit Notes";
            this.VisitNotes.MinimumWidth = 6;
            this.VisitNotes.Name = "VisitNotes";
            this.VisitNotes.ReadOnly = true;
            this.VisitNotes.Width = 125;
            // 
            // AssignID
            // 
            this.AssignID.HeaderText = "Assign ID";
            this.AssignID.MinimumWidth = 6;
            this.AssignID.Name = "AssignID";
            this.AssignID.ReadOnly = true;
            this.AssignID.Width = 125;
            // 
            // SiteVisitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvSiteVisits);
            this.Controls.Add(this.lblVisitID);
            this.Controls.Add(this.btnFieldData);
            this.Controls.Add(this.lblAssignmentID);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnSaveVisit);
            this.Controls.Add(this.txtVisitNotes);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.dtpVisitDate);
            this.Controls.Add(this.lblVisitDate);
            this.Controls.Add(this.lblRequestID);
            this.Controls.Add(this.label1);
            this.Name = "SiteVisitForm";
            this.Text = "SiteVisitForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSiteVisits)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRequestID;
        private System.Windows.Forms.Label lblVisitDate;
        private System.Windows.Forms.DateTimePicker dtpVisitDate;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtVisitNotes;
        private System.Windows.Forms.Button btnSaveVisit;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblAssignmentID;
        private System.Windows.Forms.Button btnFieldData;
        private System.Windows.Forms.Label lblVisitID;
        private System.Windows.Forms.DataGridView dgvSiteVisits;
        private System.Windows.Forms.DataGridViewTextBoxColumn VisitID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestID;
        private System.Windows.Forms.DataGridViewTextBoxColumn VisitDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn VisitNotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssignID;
    }
}