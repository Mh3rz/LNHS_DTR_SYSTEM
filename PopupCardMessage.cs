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
    public partial class PopupCardMessage : Form
    {
        public PopupCardMessage(string bodymessage)
        {
            InitializeComponent();

            // Set the text and background color based on the status
            lblBody.Text = bodymessage;
            this.BackColor = Color.Red;
            lblBody.ForeColor = Color.White;

            // Automatically close the popup after 6 seconds
            Timer timer = new Timer { Interval = 6000 }; // 6 seconds
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                this.Close();
            };
            timer.Start();
        }
    }
}
