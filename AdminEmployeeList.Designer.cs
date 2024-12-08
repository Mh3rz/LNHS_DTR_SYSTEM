using System.Windows.Forms;

namespace LNHS_DTR_SYSTEM
{
    partial class AdminEmployeeList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminEmployeeList));
            this.lblEmpId = new System.Windows.Forms.Label();
            this.lblEmpName = new System.Windows.Forms.Label();
            this.lblPrivilege = new System.Windows.Forms.Label();
            this.lblEmpList = new System.Windows.Forms.Label();
            this.dataGVEmpList = new System.Windows.Forms.DataGridView();
            this.empID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.privilege = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtEmpID = new System.Windows.Forms.TextBox();
            this.txtxEmpName = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtHiddenID = new System.Windows.Forms.TextBox();
            this.cmbPrivilege = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVEmpList)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEmpId
            // 
            this.lblEmpId.AutoSize = true;
            this.lblEmpId.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpId.Location = new System.Drawing.Point(43, 47);
            this.lblEmpId.Name = "lblEmpId";
            this.lblEmpId.Size = new System.Drawing.Size(199, 29);
            this.lblEmpId.TabIndex = 9;
            this.lblEmpId.Text = "EMPLOYEE ID: ";
            // 
            // lblEmpName
            // 
            this.lblEmpName.AutoSize = true;
            this.lblEmpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpName.Location = new System.Drawing.Point(43, 97);
            this.lblEmpName.Name = "lblEmpName";
            this.lblEmpName.Size = new System.Drawing.Size(247, 29);
            this.lblEmpName.TabIndex = 10;
            this.lblEmpName.Text = "EMPLOYEE NAME: ";
            // 
            // lblPrivilege
            // 
            this.lblPrivilege.AutoSize = true;
            this.lblPrivilege.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrivilege.Location = new System.Drawing.Point(43, 152);
            this.lblPrivilege.Name = "lblPrivilege";
            this.lblPrivilege.Size = new System.Drawing.Size(159, 29);
            this.lblPrivilege.TabIndex = 11;
            this.lblPrivilege.Text = "PRIVILEGE: ";
            // 
            // lblEmpList
            // 
            this.lblEmpList.AutoSize = true;
            this.lblEmpList.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpList.Location = new System.Drawing.Point(42, 214);
            this.lblEmpList.Name = "lblEmpList";
            this.lblEmpList.Size = new System.Drawing.Size(248, 31);
            this.lblEmpList.TabIndex = 12;
            this.lblEmpList.Text = "EMPLOYEE LIST ";
            // 
            // dataGVEmpList
            // 
            this.dataGVEmpList.AllowUserToOrderColumns = true;
            this.dataGVEmpList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGVEmpList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGVEmpList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVEmpList.Location = new System.Drawing.Point(48, 257);
            this.dataGVEmpList.Margin = new System.Windows.Forms.Padding(2);
            this.dataGVEmpList.Name = "dataGVEmpList";
            this.dataGVEmpList.RowHeadersWidth = 51;
            this.dataGVEmpList.RowTemplate.Height = 24;
            this.dataGVEmpList.Size = new System.Drawing.Size(984, 391);
            this.dataGVEmpList.TabIndex = 13;
            this.dataGVEmpList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGVEmpList_CellClick);
            // 
            // empID
            // 
            this.empID.HeaderText = "Employee ID";
            this.empID.MinimumWidth = 6;
            this.empID.Name = "empID";
            this.empID.Width = 125;
            // 
            // empName
            // 
            this.empName.HeaderText = "Employee Name";
            this.empName.MinimumWidth = 6;
            this.empName.Name = "empName";
            this.empName.Width = 125;
            // 
            // privilege
            // 
            this.privilege.HeaderText = "Privilege";
            this.privilege.MinimumWidth = 6;
            this.privilege.Name = "privilege";
            this.privilege.Width = 125;
            // 
            // txtEmpID
            // 
            this.txtEmpID.Enabled = false;
            this.txtEmpID.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpID.Location = new System.Drawing.Point(304, 47);
            this.txtEmpID.Name = "txtEmpID";
            this.txtEmpID.Size = new System.Drawing.Size(442, 38);
            this.txtEmpID.TabIndex = 14;
            // 
            // txtxEmpName
            // 
            this.txtxEmpName.Enabled = false;
            this.txtxEmpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtxEmpName.Location = new System.Drawing.Point(304, 97);
            this.txtxEmpName.Name = "txtxEmpName";
            this.txtxEmpName.Size = new System.Drawing.Size(442, 38);
            this.txtxEmpName.TabIndex = 15;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(1062, 256);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(168, 58);
            this.btnEdit.TabIndex = 17;
            this.btnEdit.Text = "EDIT";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(1062, 336);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(168, 58);
            this.btnUpdate.TabIndex = 18;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(1062, 417);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(168, 58);
            this.btnDelete.TabIndex = 19;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(1062, 499);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(168, 58);
            this.btnRefresh.TabIndex = 20;
            this.btnRefresh.Text = "REFRESH";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtHiddenID
            // 
            this.txtHiddenID.Location = new System.Drawing.Point(304, 22);
            this.txtHiddenID.Margin = new System.Windows.Forms.Padding(2);
            this.txtHiddenID.Name = "txtHiddenID";
            this.txtHiddenID.Size = new System.Drawing.Size(52, 20);
            this.txtHiddenID.TabIndex = 21;
            this.txtHiddenID.Visible = false;
            // 
            // cmbPrivilege
            // 
            this.cmbPrivilege.Enabled = false;
            this.cmbPrivilege.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPrivilege.FormattingEnabled = true;
            this.cmbPrivilege.Location = new System.Drawing.Point(304, 152);
            this.cmbPrivilege.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPrivilege.Name = "cmbPrivilege";
            this.cmbPrivilege.Size = new System.Drawing.Size(198, 39);
            this.cmbPrivilege.TabIndex = 22;
            // 
            // AdminEmployeeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.cmbPrivilege);
            this.Controls.Add(this.txtHiddenID);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.txtxEmpName);
            this.Controls.Add(this.txtEmpID);
            this.Controls.Add(this.dataGVEmpList);
            this.Controls.Add(this.lblEmpList);
            this.Controls.Add(this.lblPrivilege);
            this.Controls.Add(this.lblEmpName);
            this.Controls.Add(this.lblEmpId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AdminEmployeeList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminEmployeeList";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGVEmpList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEmpId;
        private System.Windows.Forms.Label lblEmpName;
        private System.Windows.Forms.Label lblPrivilege;
        private System.Windows.Forms.Label lblEmpList;
        private System.Windows.Forms.DataGridView dataGVEmpList;
        private System.Windows.Forms.TextBox txtEmpID;
        private System.Windows.Forms.TextBox txtxEmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn empID;
        private System.Windows.Forms.DataGridViewTextBoxColumn empName;
        private System.Windows.Forms.DataGridViewTextBoxColumn privilege;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private TextBox txtHiddenID;
        private ComboBox cmbPrivilege;
    }
}