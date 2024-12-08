namespace LNHS_DTR_SYSTEM
{
    partial class AdminViewDatabase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminViewDatabase));
            this.lblDataEntry = new System.Windows.Forms.Label();
            this.dataGVDataEntry = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblSearchName = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnFilter = new System.Windows.Forms.Button();
            this.cmbSearchName = new System.Windows.Forms.ComboBox();
            this.btnViewAllData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVDataEntry)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDataEntry
            // 
            this.lblDataEntry.AutoSize = true;
            this.lblDataEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataEntry.Location = new System.Drawing.Point(26, 24);
            this.lblDataEntry.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDataEntry.Name = "lblDataEntry";
            this.lblDataEntry.Size = new System.Drawing.Size(356, 29);
            this.lblDataEntry.TabIndex = 0;
            this.lblDataEntry.Text = "ATTENDANCE DATA ENTRY";
            // 
            // dataGVDataEntry
            // 
            this.dataGVDataEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGVDataEntry.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGVDataEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVDataEntry.Location = new System.Drawing.Point(448, 24);
            this.dataGVDataEntry.Margin = new System.Windows.Forms.Padding(2);
            this.dataGVDataEntry.Name = "dataGVDataEntry";
            this.dataGVDataEntry.RowHeadersWidth = 51;
            this.dataGVDataEntry.RowTemplate.Height = 24;
            this.dataGVDataEntry.Size = new System.Drawing.Size(796, 629);
            this.dataGVDataEntry.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(-38, 103);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 24);
            this.label2.TabIndex = 3;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.Location = new System.Drawing.Point(26, 77);
            this.lblStartDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(105, 24);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date:";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.Location = new System.Drawing.Point(26, 133);
            this.lblEndDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(102, 24);
            this.lblEndDate.TabIndex = 5;
            this.lblEndDate.Text = "End Date:";
            // 
            // lblSearchName
            // 
            this.lblSearchName.AutoSize = true;
            this.lblSearchName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchName.Location = new System.Drawing.Point(26, 207);
            this.lblSearchName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSearchName.Name = "lblSearchName";
            this.lblSearchName.Size = new System.Drawing.Size(171, 24);
            this.lblSearchName.TabIndex = 6;
            this.lblSearchName.Text = "Search by Name:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Location = new System.Drawing.Point(134, 82);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(297, 26);
            this.dtpStartDate.TabIndex = 7;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Location = new System.Drawing.Point(134, 138);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(297, 26);
            this.dtpEndDate.TabIndex = 8;
            // 
            // btnFilter
            // 
            this.btnFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.Location = new System.Drawing.Point(165, 297);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(121, 42);
            this.btnFilter.TabIndex = 10;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // cmbSearchName
            // 
            this.cmbSearchName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSearchName.FormattingEnabled = true;
            this.cmbSearchName.Location = new System.Drawing.Point(31, 244);
            this.cmbSearchName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSearchName.Name = "cmbSearchName";
            this.cmbSearchName.Size = new System.Drawing.Size(400, 34);
            this.cmbSearchName.TabIndex = 11;
            this.cmbSearchName.SelectedIndexChanged += new System.EventHandler(this.cmbSearchName_SelectedIndexChanged);
            // 
            // btnViewAllData
            // 
            this.btnViewAllData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewAllData.Location = new System.Drawing.Point(319, 198);
            this.btnViewAllData.Margin = new System.Windows.Forms.Padding(2);
            this.btnViewAllData.Name = "btnViewAllData";
            this.btnViewAllData.Size = new System.Drawing.Size(112, 33);
            this.btnViewAllData.TabIndex = 12;
            this.btnViewAllData.Text = "View All Data";
            this.btnViewAllData.UseVisualStyleBackColor = true;
            this.btnViewAllData.Click += new System.EventHandler(this.btnViewAllData_Click);
            // 
            // AdminViewDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.btnViewAllData);
            this.Controls.Add(this.cmbSearchName);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.lblSearchName);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGVDataEntry);
            this.Controls.Add(this.lblDataEntry);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AdminViewDatabase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminViewDatabase";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGVDataEntry)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDataEntry;
        private System.Windows.Forms.DataGridView dataGVDataEntry;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblSearchName;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ComboBox cmbSearchName;
        private System.Windows.Forms.Button btnViewAllData;
    }
}