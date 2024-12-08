namespace LNHS_DTR_SYSTEM
{
    partial class AdminAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminAccount));
            this.lblAdminName = new System.Windows.Forms.Label();
            this.lblPin = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtAdminName = new System.Windows.Forms.TextBox();
            this.txtPINStatus = new System.Windows.Forms.Label();
            this.btnSetupPIN = new System.Windows.Forms.Button();
            this.txtPIN = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnChangePIN = new System.Windows.Forms.Button();
            this.btnUpdatePIN = new System.Windows.Forms.Button();
            this.lblAdminID = new System.Windows.Forms.Label();
            this.txtAdminID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblAdminName
            // 
            this.lblAdminName.AutoSize = true;
            this.lblAdminName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdminName.Location = new System.Drawing.Point(52, 127);
            this.lblAdminName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAdminName.Name = "lblAdminName";
            this.lblAdminName.Size = new System.Drawing.Size(115, 20);
            this.lblAdminName.TabIndex = 0;
            this.lblAdminName.Text = "Admin Name:";
            // 
            // lblPin
            // 
            this.lblPin.AutoSize = true;
            this.lblPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPin.Location = new System.Drawing.Point(52, 222);
            this.lblPin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPin.Name = "lblPin";
            this.lblPin.Size = new System.Drawing.Size(43, 20);
            this.lblPin.TabIndex = 1;
            this.lblPin.Text = "PIN:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(22, 19);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(294, 24);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Admin Acccount Configuration";
            // 
            // txtAdminName
            // 
            this.txtAdminName.Enabled = false;
            this.txtAdminName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdminName.Location = new System.Drawing.Point(56, 150);
            this.txtAdminName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAdminName.Name = "txtAdminName";
            this.txtAdminName.ReadOnly = true;
            this.txtAdminName.Size = new System.Drawing.Size(336, 26);
            this.txtAdminName.TabIndex = 3;
            // 
            // txtPINStatus
            // 
            this.txtPINStatus.AutoSize = true;
            this.txtPINStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPINStatus.ForeColor = System.Drawing.Color.Red;
            this.txtPINStatus.Location = new System.Drawing.Point(52, 193);
            this.txtPINStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtPINStatus.Name = "txtPINStatus";
            this.txtPINStatus.Size = new System.Drawing.Size(320, 18);
            this.txtPINStatus.TabIndex = 4;
            this.txtPINStatus.Text = "You have not yet setup a PIN. Click on \"Set-up\".";
            // 
            // btnSetupPIN
            // 
            this.btnSetupPIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetupPIN.Location = new System.Drawing.Point(250, 219);
            this.btnSetupPIN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSetupPIN.Name = "btnSetupPIN";
            this.btnSetupPIN.Size = new System.Drawing.Size(74, 30);
            this.btnSetupPIN.TabIndex = 5;
            this.btnSetupPIN.Text = "Set-up";
            this.btnSetupPIN.UseVisualStyleBackColor = true;
            this.btnSetupPIN.Click += new System.EventHandler(this.btnSetupPIN_Click);
            // 
            // txtPIN
            // 
            this.txtPIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPIN.Location = new System.Drawing.Point(94, 222);
            this.txtPIN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPIN.Name = "txtPIN";
            this.txtPIN.Size = new System.Drawing.Size(152, 26);
            this.txtPIN.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(361, 289);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 43);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnChangePIN
            // 
            this.btnChangePIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePIN.Location = new System.Drawing.Point(94, 251);
            this.btnChangePIN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnChangePIN.Name = "btnChangePIN";
            this.btnChangePIN.Size = new System.Drawing.Size(74, 29);
            this.btnChangePIN.TabIndex = 8;
            this.btnChangePIN.Text = "Change";
            this.btnChangePIN.UseVisualStyleBackColor = true;
            this.btnChangePIN.Click += new System.EventHandler(this.btnChangePIN_Click);
            // 
            // btnUpdatePIN
            // 
            this.btnUpdatePIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdatePIN.Location = new System.Drawing.Point(172, 250);
            this.btnUpdatePIN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnUpdatePIN.Name = "btnUpdatePIN";
            this.btnUpdatePIN.Size = new System.Drawing.Size(74, 30);
            this.btnUpdatePIN.TabIndex = 9;
            this.btnUpdatePIN.Text = "Update";
            this.btnUpdatePIN.UseVisualStyleBackColor = true;
            this.btnUpdatePIN.Click += new System.EventHandler(this.btnUpdatePIN_Click);
            // 
            // lblAdminID
            // 
            this.lblAdminID.AutoSize = true;
            this.lblAdminID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdminID.Location = new System.Drawing.Point(52, 55);
            this.lblAdminID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAdminID.Name = "lblAdminID";
            this.lblAdminID.Size = new System.Drawing.Size(88, 20);
            this.lblAdminID.TabIndex = 10;
            this.lblAdminID.Text = "Admin ID:";
            // 
            // txtAdminID
            // 
            this.txtAdminID.Enabled = false;
            this.txtAdminID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdminID.Location = new System.Drawing.Point(56, 78);
            this.txtAdminID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAdminID.Name = "txtAdminID";
            this.txtAdminID.ReadOnly = true;
            this.txtAdminID.Size = new System.Drawing.Size(336, 26);
            this.txtAdminID.TabIndex = 11;
            // 
            // AdminAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 349);
            this.Controls.Add(this.txtAdminID);
            this.Controls.Add(this.lblAdminID);
            this.Controls.Add(this.btnUpdatePIN);
            this.Controls.Add(this.btnChangePIN);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtPIN);
            this.Controls.Add(this.btnSetupPIN);
            this.Controls.Add(this.txtPINStatus);
            this.Controls.Add(this.txtAdminName);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblPin);
            this.Controls.Add(this.lblAdminName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AdminAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminAccount";
            this.Load += new System.EventHandler(this.AdminAccount_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAdminName;
        private System.Windows.Forms.Label lblPin;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtAdminName;
        private System.Windows.Forms.Label txtPINStatus;
        private System.Windows.Forms.Button btnSetupPIN;
        private System.Windows.Forms.TextBox txtPIN;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnChangePIN;
        private System.Windows.Forms.Button btnUpdatePIN;
        private System.Windows.Forms.Label lblAdminID;
        private System.Windows.Forms.TextBox txtAdminID;
    }
}