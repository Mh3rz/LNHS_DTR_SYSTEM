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
            this.lblEmpId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpId.Location = new System.Drawing.Point(108, 94);
            this.lblEmpId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmpId.Name = "lblEmpId";
            this.lblEmpId.Size = new System.Drawing.Size(211, 31);
            this.lblEmpId.TabIndex = 9;
            this.lblEmpId.Text = "EMPLOYEE ID: ";
            // 
            // lblEmpName
            // 
            this.lblEmpName.AutoSize = true;
            this.lblEmpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpName.Location = new System.Drawing.Point(108, 156);
            this.lblEmpName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmpName.Name = "lblEmpName";
            this.lblEmpName.Size = new System.Drawing.Size(263, 31);
            this.lblEmpName.TabIndex = 10;
            this.lblEmpName.Text = "EMPLOYEE NAME: ";
            // 
            // lblPrivilege
            // 
            this.lblPrivilege.AutoSize = true;
            this.lblPrivilege.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrivilege.Location = new System.Drawing.Point(108, 224);
            this.lblPrivilege.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrivilege.Name = "lblPrivilege";
            this.lblPrivilege.Size = new System.Drawing.Size(170, 31);
            this.lblPrivilege.TabIndex = 11;
            this.lblPrivilege.Text = "PRIVILEGE: ";
            // 
            // lblEmpList
            // 
            this.lblEmpList.AutoSize = true;
            this.lblEmpList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpList.Location = new System.Drawing.Point(108, 300);
            this.lblEmpList.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmpList.Name = "lblEmpList";
            this.lblEmpList.Size = new System.Drawing.Size(222, 29);
            this.lblEmpList.TabIndex = 12;
            this.lblEmpList.Text = "EMPLOYEE LIST ";
            // 
            // dataGVEmpList
            // 
            this.dataGVEmpList.AllowUserToOrderColumns = true;
            this.dataGVEmpList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGVEmpList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGVEmpList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVEmpList.Location = new System.Drawing.Point(113, 339);
            this.dataGVEmpList.Name = "dataGVEmpList";
            this.dataGVEmpList.RowHeadersWidth = 51;
            this.dataGVEmpList.RowTemplate.Height = 24;
            this.dataGVEmpList.Size = new System.Drawing.Size(780, 411);
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
            this.txtEmpID.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpID.Location = new System.Drawing.Point(351, 85);
            this.txtEmpID.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmpID.Name = "txtEmpID";
            this.txtEmpID.Size = new System.Drawing.Size(542, 41);
            this.txtEmpID.TabIndex = 14;
            // 
            // txtxEmpName
            // 
            this.txtxEmpName.Enabled = false;
            this.txtxEmpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtxEmpName.Location = new System.Drawing.Point(351, 151);
            this.txtxEmpName.Margin = new System.Windows.Forms.Padding(4);
            this.txtxEmpName.Name = "txtxEmpName";
            this.txtxEmpName.Size = new System.Drawing.Size(542, 41);
            this.txtxEmpName.TabIndex = 15;
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(1053, 85);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(510, 71);
            this.btnEdit.TabIndex = 17;
            this.btnEdit.Text = "EDIT";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(1053, 185);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(510, 71);
            this.btnUpdate.TabIndex = 18;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(1053, 290);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(510, 71);
            this.btnDelete.TabIndex = 19;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(1053, 391);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(510, 71);
            this.btnRefresh.TabIndex = 20;
            this.btnRefresh.Text = "REFRESH";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtHiddenID
            // 
            this.txtHiddenID.Location = new System.Drawing.Point(351, 56);
            this.txtHiddenID.Name = "txtHiddenID";
            this.txtHiddenID.Size = new System.Drawing.Size(68, 22);
            this.txtHiddenID.TabIndex = 21;
            this.txtHiddenID.Visible = false;
            // 
            // cmbPrivilege
            // 
            this.cmbPrivilege.Enabled = false;
            this.cmbPrivilege.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPrivilege.FormattingEnabled = true;
            this.cmbPrivilege.Location = new System.Drawing.Point(351, 215);
            this.cmbPrivilege.Name = "cmbPrivilege";
            this.cmbPrivilege.Size = new System.Drawing.Size(218, 44);
            this.cmbPrivilege.TabIndex = 22;
            // 
            // AdminEmployeeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1686, 839);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AdminEmployeeList";
            this.Text = "AdminEmployeeList";
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