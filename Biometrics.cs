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
    public partial class Biometrics : Form
    {
        public Biometrics()
        {
            InitializeComponent();

            // Display the current day in the txtDay TextBox
            txtDay.Text = DateTime.Now.DayOfWeek.ToString();

            // Display the current date in the txtDate TextBox (e.g., September 17, 2024)
            txtDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");

            // Display the current time in the txtTime TextBox (e.g., 01:30 PM)
            txtTime.Text = DateTime.Now.ToString("hh:mm tt");

        }

    }
}
