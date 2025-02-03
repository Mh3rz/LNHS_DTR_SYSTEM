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
    public partial class PopupCard : Form
    {
        public PopupCard(string status)
        {
            InitializeComponent();

            // Set the text and background color based on the status
            lblStatus.Text = status;
            if (status.Equals("IN", StringComparison.OrdinalIgnoreCase))
            {
                this.BackColor = Color.Blue;
                lblStatus.ForeColor = Color.White;
            }
            else if (status.Equals("OUT", StringComparison.OrdinalIgnoreCase))
            {
                this.BackColor = Color.Red;
                lblStatus.ForeColor = Color.White;
            }

            // Automatically close the popup after 4 seconds
            Timer timer = new Timer { Interval = 4000 }; // 4 seconds
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                this.Close();
            };
            timer.Start();
        }
    }

}
