namespace LNHS_DTR_SYSTEM
{
    partial class AdminLoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminLoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.gbPIN = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtPIN = new System.Windows.Forms.TextBox();
            this.btnEnter = new System.Windows.Forms.Button();
            this.gbFingerprint = new System.Windows.Forms.GroupBox();
            this.picFPImage = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbIdx = new System.Windows.Forms.ComboBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.gbPIN.SuspendLayout();
            this.gbFingerprint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFPImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(349, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "ADMIN LOGIN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbPIN
            // 
            this.gbPIN.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gbPIN.Controls.Add(this.label4);
            this.gbPIN.Controls.Add(this.label3);
            this.gbPIN.Controls.Add(this.txtID);
            this.gbPIN.Controls.Add(this.txtPIN);
            this.gbPIN.Controls.Add(this.btnEnter);
            this.gbPIN.Location = new System.Drawing.Point(101, 138);
            this.gbPIN.Name = "gbPIN";
            this.gbPIN.Size = new System.Drawing.Size(275, 294);
            this.gbPIN.TabIndex = 1;
            this.gbPIN.TabStop = false;
            this.gbPIN.Text = "INSERT ID and PIN";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "PIN:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "ID:";
            // 
            // txtID
            // 
            this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(35, 58);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(199, 30);
            this.txtID.TabIndex = 2;
            // 
            // txtPIN
            // 
            this.txtPIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPIN.Location = new System.Drawing.Point(35, 132);
            this.txtPIN.Name = "txtPIN";
            this.txtPIN.Size = new System.Drawing.Size(199, 30);
            this.txtPIN.TabIndex = 1;
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(90, 185);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(92, 34);
            this.btnEnter.TabIndex = 0;
            this.btnEnter.Text = " ENTER";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // gbFingerprint
            // 
            this.gbFingerprint.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gbFingerprint.Controls.Add(this.picFPImage);
            this.gbFingerprint.Location = new System.Drawing.Point(525, 138);
            this.gbFingerprint.Name = "gbFingerprint";
            this.gbFingerprint.Size = new System.Drawing.Size(275, 294);
            this.gbFingerprint.TabIndex = 2;
            this.gbFingerprint.TabStop = false;
            this.gbFingerprint.Text = "SCAN FINGERPRINT";
            // 
            // picFPImage
            // 
            this.picFPImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFPImage.Location = new System.Drawing.Point(17, 21);
            this.picFPImage.Name = "picFPImage";
            this.picFPImage.Size = new System.Drawing.Size(240, 257);
            this.picFPImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFPImage.TabIndex = 0;
            this.picFPImage.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(427, 289);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "OR";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbIdx
            // 
            this.cmbIdx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbIdx.FormattingEnabled = true;
            this.cmbIdx.Location = new System.Drawing.Point(806, 487);
            this.cmbIdx.Name = "cmbIdx";
            this.cmbIdx.Size = new System.Drawing.Size(77, 24);
            this.cmbIdx.TabIndex = 4;
            this.cmbIdx.Visible = false;
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.SystemColors.Menu;
            this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStatus.Location = new System.Drawing.Point(10, 495);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(776, 15);
            this.txtStatus.TabIndex = 5;
            // 
            // AdminLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 530);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.cmbIdx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gbFingerprint);
            this.Controls.Add(this.gbPIN);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminLoginForm";
            this.Text = "AdminLoginForm";
            this.Load += new System.EventHandler(this.AdminLoginForm_Load);
            this.gbPIN.ResumeLayout(false);
            this.gbPIN.PerformLayout();
            this.gbFingerprint.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFPImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbPIN;
        private System.Windows.Forms.TextBox txtPIN;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.GroupBox gbFingerprint;
        private System.Windows.Forms.PictureBox picFPImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbIdx;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}