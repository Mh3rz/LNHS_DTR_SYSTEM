namespace LNHS_DTR_SYSTEM
{
    partial class AdminGenerateDTR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminGenerateDTR));
            this.gbPerEmployeeDTR = new System.Windows.Forms.GroupBox();
            this.lblEmpName = new System.Windows.Forms.Label();
            this.btnGenerateOne = new System.Windows.Forms.Button();
            this.cmbNameSelection = new System.Windows.Forms.ComboBox();
            this.cmbYearSelection = new System.Windows.Forms.ComboBox();
            this.cmbMonthSelection = new System.Windows.Forms.ComboBox();
            this.gbAllDTR = new System.Windows.Forms.GroupBox();
            this.btnGenerateAll = new System.Windows.Forms.Button();
            this.cmbYearSelection2 = new System.Windows.Forms.ComboBox();
            this.cmbMonthSelection2 = new System.Windows.Forms.ComboBox();
            this.gbPerEmployeeDTR.SuspendLayout();
            this.gbAllDTR.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPerEmployeeDTR
            // 
            this.gbPerEmployeeDTR.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gbPerEmployeeDTR.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.gbPerEmployeeDTR.Controls.Add(this.lblEmpName);
            this.gbPerEmployeeDTR.Controls.Add(this.btnGenerateOne);
            this.gbPerEmployeeDTR.Controls.Add(this.cmbNameSelection);
            this.gbPerEmployeeDTR.Controls.Add(this.cmbYearSelection);
            this.gbPerEmployeeDTR.Controls.Add(this.cmbMonthSelection);
            this.gbPerEmployeeDTR.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPerEmployeeDTR.Location = new System.Drawing.Point(625, 164);
            this.gbPerEmployeeDTR.Margin = new System.Windows.Forms.Padding(2);
            this.gbPerEmployeeDTR.Name = "gbPerEmployeeDTR";
            this.gbPerEmployeeDTR.Padding = new System.Windows.Forms.Padding(2);
            this.gbPerEmployeeDTR.Size = new System.Drawing.Size(452, 325);
            this.gbPerEmployeeDTR.TabIndex = 1;
            this.gbPerEmployeeDTR.TabStop = false;
            this.gbPerEmployeeDTR.Text = "Generate DTR Per Employee";
            // 
            // lblEmpName
            // 
            this.lblEmpName.AutoSize = true;
            this.lblEmpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpName.Location = new System.Drawing.Point(35, 71);
            this.lblEmpName.Name = "lblEmpName";
            this.lblEmpName.Size = new System.Drawing.Size(82, 24);
            this.lblEmpName.TabIndex = 6;
            this.lblEmpName.Text = "NAME: ";
            // 
            // btnGenerateOne
            // 
            this.btnGenerateOne.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnGenerateOne.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGenerateOne.Location = new System.Drawing.Point(136, 225);
            this.btnGenerateOne.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerateOne.Name = "btnGenerateOne";
            this.btnGenerateOne.Size = new System.Drawing.Size(176, 48);
            this.btnGenerateOne.TabIndex = 5;
            this.btnGenerateOne.Text = "GENERATE";
            this.btnGenerateOne.UseVisualStyleBackColor = false;
            this.btnGenerateOne.Click += new System.EventHandler(this.btnGenerateOne_Click);
            // 
            // cmbNameSelection
            // 
            this.cmbNameSelection.Location = new System.Drawing.Point(39, 102);
            this.cmbNameSelection.Margin = new System.Windows.Forms.Padding(2);
            this.cmbNameSelection.Name = "cmbNameSelection";
            this.cmbNameSelection.Size = new System.Drawing.Size(380, 28);
            this.cmbNameSelection.TabIndex = 7;
            // 
            // cmbYearSelection
            // 
            this.cmbYearSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbYearSelection.FormattingEnabled = true;
            this.cmbYearSelection.Location = new System.Drawing.Point(305, 158);
            this.cmbYearSelection.Margin = new System.Windows.Forms.Padding(2);
            this.cmbYearSelection.Name = "cmbYearSelection";
            this.cmbYearSelection.Size = new System.Drawing.Size(114, 30);
            this.cmbYearSelection.TabIndex = 1;
            // 
            // cmbMonthSelection
            // 
            this.cmbMonthSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMonthSelection.FormattingEnabled = true;
            this.cmbMonthSelection.Location = new System.Drawing.Point(39, 158);
            this.cmbMonthSelection.Margin = new System.Windows.Forms.Padding(2);
            this.cmbMonthSelection.Name = "cmbMonthSelection";
            this.cmbMonthSelection.Size = new System.Drawing.Size(263, 30);
            this.cmbMonthSelection.TabIndex = 2;
            // 
            // gbAllDTR
            // 
            this.gbAllDTR.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gbAllDTR.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gbAllDTR.Controls.Add(this.btnGenerateAll);
            this.gbAllDTR.Controls.Add(this.cmbYearSelection2);
            this.gbAllDTR.Controls.Add(this.cmbMonthSelection2);
            this.gbAllDTR.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAllDTR.Location = new System.Drawing.Point(185, 164);
            this.gbAllDTR.Margin = new System.Windows.Forms.Padding(2);
            this.gbAllDTR.Name = "gbAllDTR";
            this.gbAllDTR.Padding = new System.Windows.Forms.Padding(2);
            this.gbAllDTR.Size = new System.Drawing.Size(381, 325);
            this.gbAllDTR.TabIndex = 2;
            this.gbAllDTR.TabStop = false;
            this.gbAllDTR.Text = "Generate All DTR";
            // 
            // btnGenerateAll
            // 
            this.btnGenerateAll.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnGenerateAll.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGenerateAll.Location = new System.Drawing.Point(100, 225);
            this.btnGenerateAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerateAll.Name = "btnGenerateAll";
            this.btnGenerateAll.Size = new System.Drawing.Size(176, 48);
            this.btnGenerateAll.TabIndex = 5;
            this.btnGenerateAll.Text = "GENERATE";
            this.btnGenerateAll.UseVisualStyleBackColor = false;
            this.btnGenerateAll.Click += new System.EventHandler(this.btnGenerateAll_Click);
            // 
            // cmbYearSelection2
            // 
            this.cmbYearSelection2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbYearSelection2.FormattingEnabled = true;
            this.cmbYearSelection2.Location = new System.Drawing.Point(70, 134);
            this.cmbYearSelection2.Margin = new System.Windows.Forms.Padding(2);
            this.cmbYearSelection2.Name = "cmbYearSelection2";
            this.cmbYearSelection2.Size = new System.Drawing.Size(239, 30);
            this.cmbYearSelection2.TabIndex = 1;
            // 
            // cmbMonthSelection2
            // 
            this.cmbMonthSelection2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMonthSelection2.FormattingEnabled = true;
            this.cmbMonthSelection2.Location = new System.Drawing.Point(70, 64);
            this.cmbMonthSelection2.Margin = new System.Windows.Forms.Padding(2);
            this.cmbMonthSelection2.Name = "cmbMonthSelection2";
            this.cmbMonthSelection2.Size = new System.Drawing.Size(239, 30);
            this.cmbMonthSelection2.TabIndex = 2;
            // 
            // AdminGenerateDTR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.gbAllDTR);
            this.Controls.Add(this.gbPerEmployeeDTR);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AdminGenerateDTR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminGenerateDTR";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AdminGenerateDTR_Load);
            this.gbPerEmployeeDTR.ResumeLayout(false);
            this.gbPerEmployeeDTR.PerformLayout();
            this.gbAllDTR.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbPerEmployeeDTR;
        private System.Windows.Forms.ComboBox cmbYearSelection;
        private System.Windows.Forms.ComboBox cmbMonthSelection;
        private System.Windows.Forms.ComboBox cmbNameSelection;
        private System.Windows.Forms.Button btnGenerateOne;
        private System.Windows.Forms.Label lblEmpName;
        private System.Windows.Forms.GroupBox gbAllDTR;
        private System.Windows.Forms.Button btnGenerateAll;
        private System.Windows.Forms.ComboBox cmbYearSelection2;
        private System.Windows.Forms.ComboBox cmbMonthSelection2;
    }
}