using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LNHS_DTR_SYSTEM
{
    public partial class CustomMessageBox : Form
    {
        public CustomMessageBox(string boldText, string regularText, string bypassMessage)
        {
            InitializeComponent();

            // Bold message
            lblBoldMessage.Text = boldText;
            lblBoldMessage.Font = new Font("Arial", 14, FontStyle.Bold);
            lblBoldMessage.TextAlign = ContentAlignment.MiddleCenter;

            // Regular message
            lblRegularMessage.Text = regularText;
            lblRegularMessage.Font = new Font("Arial", 12, FontStyle.Regular);
            lblRegularMessage.TextAlign = ContentAlignment.MiddleCenter;

            // Message
            lblMessage.Text = bypassMessage;
            lblMessage.Font = new Font("Arial", 12, FontStyle.Bold);
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;

            // Set up Yes and No buttons
            btnYes.Text = "Yes";
            btnNo.Text = "No";

            // Center the form on the screen
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public static DialogResult Show(string boldText, string regularText, string bypassMessage)
        {
            using (var form = new CustomMessageBox(boldText, regularText, bypassMessage))
            {
                return form.ShowDialog();
            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        
    }

}
