namespace LNHS_DTR_SYSTEM
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnBiometricsMain = new System.Windows.Forms.Button();
            this.btnRegisterMain = new System.Windows.Forms.Button();
            this.btnAdminMain = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBiometricsMain
            // 
            this.btnBiometricsMain.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBiometricsMain.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnBiometricsMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBiometricsMain.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBiometricsMain.Location = new System.Drawing.Point(443, 22);
            this.btnBiometricsMain.Name = "btnBiometricsMain";
            this.btnBiometricsMain.Size = new System.Drawing.Size(404, 184);
            this.btnBiometricsMain.TabIndex = 0;
            this.btnBiometricsMain.Text = "BIOMETRICS";
            this.btnBiometricsMain.UseVisualStyleBackColor = false;
            this.btnBiometricsMain.Click += new System.EventHandler(this.btnBiometricsMain_Click);
            // 
            // btnRegisterMain
            // 
            this.btnRegisterMain.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRegisterMain.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnRegisterMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegisterMain.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRegisterMain.Location = new System.Drawing.Point(443, 248);
            this.btnRegisterMain.Name = "btnRegisterMain";
            this.btnRegisterMain.Size = new System.Drawing.Size(404, 184);
            this.btnRegisterMain.TabIndex = 1;
            this.btnRegisterMain.Text = "REGISTER";
            this.btnRegisterMain.UseVisualStyleBackColor = false;
            this.btnRegisterMain.Click += new System.EventHandler(this.btnRegisterMain_Click);
            // 
            // btnAdminMain
            // 
            this.btnAdminMain.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAdminMain.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnAdminMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdminMain.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAdminMain.Location = new System.Drawing.Point(443, 474);
            this.btnAdminMain.Name = "btnAdminMain";
            this.btnAdminMain.Size = new System.Drawing.Size(404, 184);
            this.btnAdminMain.TabIndex = 2;
            this.btnAdminMain.Text = "ADMIN";
            this.btnAdminMain.UseVisualStyleBackColor = false;
            this.btnAdminMain.Click += new System.EventHandler(this.btnAdminMain_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.btnAdminMain);
            this.Controls.Add(this.btnRegisterMain);
            this.Controls.Add(this.btnBiometricsMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LNSH Biometrics";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBiometricsMain;
        private System.Windows.Forms.Button btnRegisterMain;
        private System.Windows.Forms.Button btnAdminMain;
    }
}

