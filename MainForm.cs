using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace LNHS_DTR_SYSTEM
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            // Open and close XAMPP automatically
            OpenAndCloseXampp();
        }


        // Opening Xampp Automatically
        private void OpenAndCloseXampp()
        {
            try
            {
                // Start the XAMPP application
                Process xamppProcess = Process.Start(@"C:\xampp\xampp-control.exe");

                // Wait for a certain amount of time (e.g., 5 seconds)
                Thread.Sleep(5000);

                // Close the XAMPP application
                //if (xamppProcess != null && !xamppProcess.HasExited)
                //{
                //    xamppProcess.Kill(); // Forcefully terminate the process
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error managing XAMPP: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Declare a private variable to track the Biometrics form instance
        private Biometrics biometricsForm;

        // Declare private variables to track the Register and AdminLoginForm forms
        private Register registerForm;
        private AdminLoginForm adminLoginForm;
        
        //private Admin adminmainForm;

        private void btnBiometricsMain_Click(object sender, EventArgs e)
        {
            // Check if the form is already open
            if (biometricsForm == null || biometricsForm.IsDisposed)
            {
                // If it's null or disposed, create a new instance
                biometricsForm = new Biometrics();
                biometricsForm.FormClosed += (s, args) => biometricsForm = null; // Reset the reference when the form is closed
                biometricsForm.Show(); // Use ShowDialog() if modal is needed
            }
            else
            {
                // Bring the existing form to the foreground
                biometricsForm.BringToFront();
                biometricsForm.Focus();
            }
        }

        private void btnRegisterMain_Click(object sender, EventArgs e)
        {
            // Check if the Register form is already open
            if (registerForm == null || registerForm.IsDisposed)
            {
                // Create a new instance if it's null or disposed
                registerForm = new Register();
                registerForm.FormClosed += (s, args) => registerForm = null; // Reset the reference when the form is closed
                registerForm.Show(); // Use ShowDialog() if modal is needed
            }
            else
            {
                // Bring the existing form to the foreground
                registerForm.BringToFront();
                registerForm.Focus();
            }
        }

        private void btnAdminMain_Click(object sender, EventArgs e)
        {
            // Check if the AdminLogin form is null, disposed, or hidden
            if (adminLoginForm == null || adminLoginForm.IsDisposed)
            {
                // Create a new instance if it's null or disposed
                adminLoginForm = new AdminLoginForm();
                adminLoginForm.FormClosed += (s, args) => adminLoginForm = null; // Reset the reference when the form is closed
                adminLoginForm.Show(); // Open the form
            }
            else if (!adminLoginForm.Visible)
            {
                // If the form exists but is hidden, simply show it
                adminLoginForm.Show();
            }
            else
            {
                // Bring the existing form to the foreground if already open
                adminLoginForm.BringToFront();
                adminLoginForm.Focus();
            }
        }


    }
}
