namespace WindowsFormsApp1
{
    partial class SiteAssessmentForm
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
            this.lblTechnicianID = new System.Windows.Forms.Label();
            this.dtpAssessmentDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSaveAssessment = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAssessmentNotes = new System.Windows.Forms.TextBox();
            this.cmbSiteCondition = new System.Windows.Forms.ComboBox();
            this.cmbAccessibility = new System.Windows.Forms.ComboBox();
            this.cmbSuitability = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(310, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Site Assessment";
            // 
            // lblRequestID
            // 
            this.lblRequestID.AutoSize = true;
            this.lblRequestID.Location = new System.Drawing.Point(65, 71);
            this.lblRequestID.Name = "lblRequestID";
            this.lblRequestID.Size = new System.Drawing.Size(77, 16);
            this.lblRequestID.TabIndex = 1;
            this.lblRequestID.Text = "Request ID:";
            // 
            // lblTechnicianID
            // 
            this.lblTechnicianID.AutoSize = true;
            this.lblTechnicianID.Location = new System.Drawing.Point(65, 114);
            this.lblTechnicianID.Name = "lblTechnicianID";
            this.lblTechnicianID.Size = new System.Drawing.Size(92, 16);
            this.lblTechnicianID.TabIndex = 2;
            this.lblTechnicianID.Text = "Technician ID:";
            // 
            // dtpAssessmentDate
            // 
            this.dtpAssessmentDate.Location = new System.Drawing.Point(196, 155);
            this.dtpAssessmentDate.Name = "dtpAssessmentDate";
            this.dtpAssessmentDate.Size = new System.Drawing.Size(200, 22);
            this.dtpAssessmentDate.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Assessment Date:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Site Condition:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(65, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Accessibilty:";
            // 
            // btnSaveAssessment
            // 
            this.btnSaveAssessment.Location = new System.Drawing.Point(313, 340);
            this.btnSaveAssessment.Name = "btnSaveAssessment";
            this.btnSaveAssessment.Size = new System.Drawing.Size(146, 23);
            this.btnSaveAssessment.TabIndex = 7;
            this.btnSaveAssessment.Text = "Save Assessment";
            this.btnSaveAssessment.UseVisualStyleBackColor = true;
            this.btnSaveAssessment.Click += new System.EventHandler(this.btnSaveAssessment_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(702, 415);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(65, 295);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 16);
            this.label7.TabIndex = 9;
            this.label7.Text = "Assessment Result:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(541, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "Technicians notes:";
            // 
            // txtAssessmentNotes
            // 
            this.txtAssessmentNotes.Location = new System.Drawing.Point(677, 71);
            this.txtAssessmentNotes.Multiline = true;
            this.txtAssessmentNotes.Name = "txtAssessmentNotes";
            this.txtAssessmentNotes.Size = new System.Drawing.Size(100, 74);
            this.txtAssessmentNotes.TabIndex = 11;
            // 
            // cmbSiteCondition
            // 
            this.cmbSiteCondition.FormattingEnabled = true;
            this.cmbSiteCondition.Location = new System.Drawing.Point(163, 202);
            this.cmbSiteCondition.Name = "cmbSiteCondition";
            this.cmbSiteCondition.Size = new System.Drawing.Size(121, 24);
            this.cmbSiteCondition.TabIndex = 12;
            // 
            // cmbAccessibility
            // 
            this.cmbAccessibility.FormattingEnabled = true;
            this.cmbAccessibility.Location = new System.Drawing.Point(153, 247);
            this.cmbAccessibility.Name = "cmbAccessibility";
            this.cmbAccessibility.Size = new System.Drawing.Size(121, 24);
            this.cmbAccessibility.TabIndex = 13;
            // 
            // cmbSuitability
            // 
            this.cmbSuitability.FormattingEnabled = true;
            this.cmbSuitability.Location = new System.Drawing.Point(196, 292);
            this.cmbSuitability.Name = "cmbSuitability";
            this.cmbSuitability.Size = new System.Drawing.Size(121, 24);
            this.cmbSuitability.TabIndex = 14;
            // 
            // SiteAssessmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbSuitability);
            this.Controls.Add(this.cmbAccessibility);
            this.Controls.Add(this.cmbSiteCondition);
            this.Controls.Add(this.txtAssessmentNotes);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnSaveAssessment);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpAssessmentDate);
            this.Controls.Add(this.lblTechnicianID);
            this.Controls.Add(this.lblRequestID);
            this.Controls.Add(this.label1);
            this.Name = "SiteAssessmentForm";
            this.Text = "SiteAssessmentForm";
            this.Load += new System.EventHandler(this.SiteAssessmentForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRequestID;
        private System.Windows.Forms.Label lblTechnicianID;
        private System.Windows.Forms.DateTimePicker dtpAssessmentDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSaveAssessment;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAssessmentNotes;
        private System.Windows.Forms.ComboBox cmbSiteCondition;
        private System.Windows.Forms.ComboBox cmbAccessibility;
        private System.Windows.Forms.ComboBox cmbSuitability;
    }
}