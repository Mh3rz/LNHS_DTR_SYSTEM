namespace LNHS_DTR_SYSTEM
{
    partial class Admin
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
            this.lblEmpId = new System.Windows.Forms.Label();
            this.btnGenerateDTR = new System.Windows.Forms.Button();
            this.btnEmployeeList = new System.Windows.Forms.Button();
            this.btnViewDatabase = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblEmpId
            // 
            this.lblEmpId.AutoSize = true;
            this.lblEmpId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpId.Location = new System.Drawing.Point(29, 27);
            this.lblEmpId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmpId.Name = "lblEmpId";
            this.lblEmpId.Size = new System.Drawing.Size(398, 29);
            this.lblEmpId.TabIndex = 9;
            this.lblEmpId.Text = "ADMINISTRATION - CONTROLS";
            // 
            // btnGenerateDTR
            // 
            this.btnGenerateDTR.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnGenerateDTR.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateDTR.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGenerateDTR.Location = new System.Drawing.Point(127, 316);
            this.btnGenerateDTR.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerateDTR.Name = "btnGenerateDTR";
            this.btnGenerateDTR.Size = new System.Drawing.Size(441, 206);
            this.btnGenerateDTR.TabIndex = 10;
            this.btnGenerateDTR.Text = "GENERATE DTR";
            this.btnGenerateDTR.UseVisualStyleBackColor = false;
            this.btnGenerateDTR.Click += new System.EventHandler(this.btnGenerateDTR_Click);
            // 
            // btnEmployeeList
            // 
            this.btnEmployeeList.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnEmployeeList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmployeeList.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnEmployeeList.Location = new System.Drawing.Point(627, 316);
            this.btnEmployeeList.Margin = new System.Windows.Forms.Padding(4);
            this.btnEmployeeList.Name = "btnEmployeeList";
            this.btnEmployeeList.Size = new System.Drawing.Size(441, 206);
            this.btnEmployeeList.TabIndex = 11;
            this.btnEmployeeList.Text = "EMPLOYEE LIST";
            this.btnEmployeeList.UseVisualStyleBackColor = false;
            this.btnEmployeeList.Click += new System.EventHandler(this.btnEmployeeList_Click);
            // 
            // btnViewDatabase
            // 
            this.btnViewDatabase.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnViewDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewDatabase.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnViewDatabase.Location = new System.Drawing.Point(1123, 316);
            this.btnViewDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.btnViewDatabase.Name = "btnViewDatabase";
            this.btnViewDatabase.Size = new System.Drawing.Size(441, 206);
            this.btnViewDatabase.TabIndex = 12;
            this.btnViewDatabase.Text = "VIEW DATABASE";
            this.btnViewDatabase.UseVisualStyleBackColor = false;
            this.btnViewDatabase.Click += new System.EventHandler(this.btnViewDatabase_Click);
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1685, 838);
            this.Controls.Add(this.btnViewDatabase);
            this.Controls.Add(this.btnEmployeeList);
            this.Controls.Add(this.btnGenerateDTR);
            this.Controls.Add(this.lblEmpId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Admin";
            this.Text = "Admin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEmpId;
        private System.Windows.Forms.Button btnGenerateDTR;
        private System.Windows.Forms.Button btnEmployeeList;
        private System.Windows.Forms.Button btnViewDatabase;
    }
}