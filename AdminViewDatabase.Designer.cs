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
            this.lblDataEntry = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
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
            this.lblDataEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataEntry.Location = new System.Drawing.Point(101, 62);
            this.lblDataEntry.Name = "lblDataEntry";
            this.lblDataEntry.Size = new System.Drawing.Size(356, 29);
            this.lblDataEntry.TabIndex = 0;
            this.lblDataEntry.Text = "ATTENDANCE DATA ENTRY";
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(547, 729);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(221, 42);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "DOWNLOAD";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // dataGVDataEntry
            // 
            this.dataGVDataEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVDataEntry.Location = new System.Drawing.Point(547, 127);
            this.dataGVDataEntry.Name = "dataGVDataEntry";
            this.dataGVDataEntry.RowHeadersWidth = 51;
            this.dataGVDataEntry.RowTemplate.Height = 24;
            this.dataGVDataEntry.Size = new System.Drawing.Size(1029, 556);
            this.dataGVDataEntry.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(101, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 29);
            this.label2.TabIndex = 3;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.Location = new System.Drawing.Point(101, 127);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(169, 36);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date:";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.Location = new System.Drawing.Point(101, 182);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(159, 36);
            this.lblEndDate.TabIndex = 5;
            this.lblEndDate.Text = "End Date:";
            // 
            // lblSearchName
            // 
            this.lblSearchName.AutoSize = true;
            this.lblSearchName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchName.Location = new System.Drawing.Point(101, 246);
            this.lblSearchName.Name = "lblSearchName";
            this.lblSearchName.Size = new System.Drawing.Size(265, 36);
            this.lblSearchName.TabIndex = 6;
            this.lblSearchName.Text = "Search by Name:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(243, 132);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(276, 22);
            this.dtpStartDate.TabIndex = 7;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(243, 187);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(276, 22);
            this.dtpEndDate.TabIndex = 8;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(214, 339);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(161, 52);
            this.btnFilter.TabIndex = 10;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // cmbSearchName
            // 
            this.cmbSearchName.FormattingEnabled = true;
            this.cmbSearchName.Location = new System.Drawing.Point(106, 292);
            this.cmbSearchName.Name = "cmbSearchName";
            this.cmbSearchName.Size = new System.Drawing.Size(413, 24);
            this.cmbSearchName.TabIndex = 11;
            this.cmbSearchName.SelectedIndexChanged += new System.EventHandler(this.cmbSearchName_SelectedIndexChanged);
            // 
            // btnViewAllData
            // 
            this.btnViewAllData.Location = new System.Drawing.Point(405, 253);
            this.btnViewAllData.Name = "btnViewAllData";
            this.btnViewAllData.Size = new System.Drawing.Size(114, 23);
            this.btnViewAllData.TabIndex = 12;
            this.btnViewAllData.Text = "View All Data";
            this.btnViewAllData.UseVisualStyleBackColor = true;
            this.btnViewAllData.Click += new System.EventHandler(this.btnViewAllData_Click);
            // 
            // AdminViewDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1685, 838);
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
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.lblDataEntry);
            this.Name = "AdminViewDatabase";
            this.Text = "AdminViewDatabase";
            ((System.ComponentModel.ISupportInitialize)(this.dataGVDataEntry)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDataEntry;
        private System.Windows.Forms.Button btnDownload;
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