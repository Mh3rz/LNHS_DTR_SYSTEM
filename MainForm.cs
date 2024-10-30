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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBiometricsMain_Click(object sender, EventArgs e)
        {
            // Create an instance of the Biometrics form
            Biometrics biometricsForm = new Biometrics();

            // Show the Biometrics form
            biometricsForm.Show(); // Use ShowDialog() if you want it to be modal
        }

        private void btnRegisterMain_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();

            registerForm.Show();
        }

        private void btnAdminMain_Click(object sender, EventArgs e)
        {
            Admin adminmainForm = new Admin();
            adminmainForm.Show();
        }
    }
}
