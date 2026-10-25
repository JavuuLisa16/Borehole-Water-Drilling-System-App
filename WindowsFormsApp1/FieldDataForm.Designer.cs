namespace WindowsFormsApp1
{
    partial class FieldDataForm
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
            this.lblWaterDepth = new System.Windows.Forms.Label();
            this.lblSoilType = new System.Windows.Forms.Label();
            this.lblDrillingStatus = new System.Windows.Forms.Label();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.btnSaveFieldData = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.txtWaterDepth = new System.Windows.Forms.TextBox();
            this.txtSoilType = new System.Windows.Forms.TextBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.cmbDrillingStatus = new System.Windows.Forms.ComboBox();
            this.lblVisitID = new System.Windows.Forms.Label();
            this.DataID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(321, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Field Data";
            // 
            // lblWaterDepth
            // 
            this.lblWaterDepth.AutoSize = true;
            this.lblWaterDepth.Location = new System.Drawing.Point(70, 144);
            this.lblWaterDepth.Name = "lblWaterDepth";
            this.lblWaterDepth.Size = new System.Drawing.Size(85, 16);
            this.lblWaterDepth.TabIndex = 3;
            this.lblWaterDepth.Text = "Water Depth:";
            // 
            // lblSoilType
            // 
            this.lblSoilType.AutoSize = true;
            this.lblSoilType.Location = new System.Drawing.Point(70, 245);
            this.lblSoilType.Name = "lblSoilType";
            this.lblSoilType.Size = new System.Drawing.Size(68, 16);
            this.lblSoilType.TabIndex = 4;
            this.lblSoilType.Text = "Soil Type:";
            // 
            // lblDrillingStatus
            // 
            this.lblDrillingStatus.AutoSize = true;
            this.lblDrillingStatus.Location = new System.Drawing.Point(443, 82);
            this.lblDrillingStatus.Name = "lblDrillingStatus";
            this.lblDrillingStatus.Size = new System.Drawing.Size(91, 16);
            this.lblDrillingStatus.TabIndex = 5;
            this.lblDrillingStatus.Text = "Drilling Status:";
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Location = new System.Drawing.Point(469, 196);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(65, 16);
            this.lblRemarks.TabIndex = 6;
            this.lblRemarks.Text = "Remarks:";
            // 
            // btnSaveFieldData
            // 
            this.btnSaveFieldData.Location = new System.Drawing.Point(30, 415);
            this.btnSaveFieldData.Name = "btnSaveFieldData";
            this.btnSaveFieldData.Size = new System.Drawing.Size(125, 23);
            this.btnSaveFieldData.TabIndex = 7;
            this.btnSaveFieldData.Text = "Save Field Data";
            this.btnSaveFieldData.UseVisualStyleBackColor = true;
            this.btnSaveFieldData.Click += new System.EventHandler(this.btnSaveFieldData_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(206, 415);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(125, 23);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtWaterDepth
            // 
            this.txtWaterDepth.Location = new System.Drawing.Point(161, 141);
            this.txtWaterDepth.Multiline = true;
            this.txtWaterDepth.Name = "txtWaterDepth";
            this.txtWaterDepth.Size = new System.Drawing.Size(122, 71);
            this.txtWaterDepth.TabIndex = 9;
            // 
            // txtSoilType
            // 
            this.txtSoilType.Location = new System.Drawing.Point(162, 245);
            this.txtSoilType.Multiline = true;
            this.txtSoilType.Name = "txtSoilType";
            this.txtSoilType.Size = new System.Drawing.Size(121, 74);
            this.txtSoilType.TabIndex = 10;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(551, 193);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(120, 68);
            this.txtRemarks.TabIndex = 11;
            // 
            // cmbDrillingStatus
            // 
            this.cmbDrillingStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrillingStatus.FormattingEnabled = true;
            this.cmbDrillingStatus.Items.AddRange(new object[] {
            "Not Started",
            "In Progress",
            "Completed",
            "On Hold"});
            this.cmbDrillingStatus.Location = new System.Drawing.Point(573, 78);
            this.cmbDrillingStatus.Name = "cmbDrillingStatus";
            this.cmbDrillingStatus.Size = new System.Drawing.Size(121, 24);
            this.cmbDrillingStatus.TabIndex = 12;
            // 
            // lblVisitID
            // 
            this.lblVisitID.AutoSize = true;
            this.lblVisitID.Location = new System.Drawing.Point(70, 78);
            this.lblVisitID.Name = "lblVisitID";
            this.lblVisitID.Size = new System.Drawing.Size(48, 16);
            this.lblVisitID.TabIndex = 13;
            this.lblVisitID.Text = "VisitID:";
            // 
            // DataID
            // 
            this.DataID.HeaderText = "Data ID";
            this.DataID.MinimumWidth = 6;
            this.DataID.Name = "DataID";
            this.DataID.Width = 125;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Data ID";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Data ID";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 125;
            // 
            // FieldDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblVisitID);
            this.Controls.Add(this.cmbDrillingStatus);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.txtSoilType);
            this.Controls.Add(this.txtWaterDepth);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnSaveFieldData);
            this.Controls.Add(this.lblRemarks);
            this.Controls.Add(this.lblDrillingStatus);
            this.Controls.Add(this.lblSoilType);
            this.Controls.Add(this.lblWaterDepth);
            this.Controls.Add(this.label1);
            this.Name = "FieldDataForm";
            this.Text = "FieldDataForm";
            this.Load += new System.EventHandler(this.FieldDataForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWaterDepth;
        private System.Windows.Forms.Label lblSoilType;
        private System.Windows.Forms.Label lblDrillingStatus;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.Button btnSaveFieldData;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox txtWaterDepth;
        private System.Windows.Forms.TextBox txtSoilType;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.ComboBox cmbDrillingStatus;
        private System.Windows.Forms.Label lblVisitID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}