namespace LNHS_DTR_SYSTEM
{
    partial class CustomMessageBox
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
            this.lblBoldMessage = new System.Windows.Forms.Label();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.lblRegularText = new System.Windows.Forms.Label();
            this.lblRegularMessage = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblBoldMessage
            // 
            this.lblBoldMessage.Location = new System.Drawing.Point(113, 56);
            this.lblBoldMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBoldMessage.Name = "lblBoldMessage";
            this.lblBoldMessage.Size = new System.Drawing.Size(397, 30);
            this.lblBoldMessage.TabIndex = 0;
            this.lblBoldMessage.Text = "YOU ALREADY CLOCKED IN!!! ";
            this.lblBoldMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnYes
            // 
            this.btnYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYes.Location = new System.Drawing.Point(147, 273);
            this.btnYes.Margin = new System.Windows.Forms.Padding(2);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(144, 46);
            this.btnYes.TabIndex = 1;
            this.btnYes.Text = "YES";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNo.Location = new System.Drawing.Point(336, 273);
            this.btnNo.Margin = new System.Windows.Forms.Padding(2);
            this.btnNo.Name = "btnNo";
            this.btnNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnNo.Size = new System.Drawing.Size(144, 46);
            this.btnNo.TabIndex = 2;
            this.btnNo.Text = "NO";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // lblRegularText
            // 
            this.lblRegularText.Location = new System.Drawing.Point(264, 190);
            this.lblRegularText.Name = "lblRegularText";
            this.lblRegularText.Size = new System.Drawing.Size(100, 23);
            this.lblRegularText.TabIndex = 3;
            this.lblRegularText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRegularMessage
            // 
            this.lblRegularMessage.Location = new System.Drawing.Point(101, 125);
            this.lblRegularMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRegularMessage.Name = "lblRegularMessage";
            this.lblRegularMessage.Size = new System.Drawing.Size(409, 42);
            this.lblRegularMessage.TabIndex = 4;
            this.lblRegularMessage.Text = "You can only CLOCK OUT between the window for (SECOND ENTRY). ";
            this.lblRegularMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(113, 213);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(397, 42);
            this.lblMessage.TabIndex = 5;
            this.lblMessage.Text = "Do you want to bypass this restriction?";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CustomMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblRegularMessage);
            this.Controls.Add(this.lblRegularText);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.lblBoldMessage);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CustomMessageBox";
            this.Text = "Alert Message";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblBoldMessage;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Label lblRegularText;
        private System.Windows.Forms.Label lblRegularMessage;
        private System.Windows.Forms.Label lblMessage;
    }
}