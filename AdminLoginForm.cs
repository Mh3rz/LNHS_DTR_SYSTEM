using libzkfpcsharp;
using MySql.Data.MySqlClient;
using Sample;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace LNHS_DTR_SYSTEM
{
    public partial class AdminLoginForm : Form
    {
        private IntPtr mDevHandle = IntPtr.Zero;
        private IntPtr mDBHandle = IntPtr.Zero;
        private IntPtr FormHandle = IntPtr.Zero;

        private byte[] FPBuffer;
        private byte[] RegTmp = new byte[2048]; // Final template
        private byte[] CapTmp = new byte[2048]; // Captured template
        private int mfpWidth = 0, mfpHeight = 0;

        private const int REGISTER_FINGER_COUNT = 1;
        private const int MESSAGE_CAPTURED_OK = 0x0400 + 6;

        private bool bIsTimeToDie = false;
        private bool IsRegister = false;
        private int RegisterCount = 0, cbCapTmp = 2048, cbRegTmp = 0, iFid = 1;

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        private Admin adminForm;

        public AdminLoginForm()
        {
            InitializeComponent();
            this.FormClosing += AdminLoginForm_FormClosing;
        }

        private void AdminLoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeFingerprintDevice();
            }
            catch (Exception ex)
            {
                //UpdateUI($"Error initializing fingerprint device: {ex.Message}", Color.Red);
                UpdateUI($"Error initializing fingerprint device: Please verify that the ZKTeco Sensor is properly connected", Color.Red);
            }
        }

        private void InitializeFingerprintDevice()
        {
            FormHandle = this.Handle;
            cmbIdx.Items.Clear();

            if (zkfp2.Init() != zkfperrdef.ZKFP_ERR_OK)
                throw new Exception("Fingerprint SDK initialization failed.");

            int deviceCount = zkfp2.GetDeviceCount();
            if (deviceCount <= 0)
            {
                zkfp2.Terminate();
                throw new Exception("No fingerprint devices connected.");
            }

            for (int i = 0; i < deviceCount; i++)
                cmbIdx.Items.Add(i.ToString());

            cmbIdx.SelectedIndex = 0;

            mDevHandle = zkfp2.OpenDevice(cmbIdx.SelectedIndex);
            if (mDevHandle == IntPtr.Zero)
                throw new Exception("Failed to open fingerprint scanner device.");

            mDBHandle = zkfp2.DBInit();
            if (mDBHandle == IntPtr.Zero)
            {
                zkfp2.CloseDevice(mDevHandle);
                throw new Exception("Failed to initialize fingerprint database.");
            }

            InitializeFingerprintBuffer();
            StartCaptureThread();

            IsRegister = true;
            RegisterCount = 0;
            cbRegTmp = 0;
            UpdateUI("Place your finger on the scanner to login or insert PIN.", Color.BlueViolet);
        }

        private void InitializeFingerprintBuffer()
        {
            byte[] paramValue = new byte[4];
            int size = 4;

            zkfp2.GetParameters(mDevHandle, 1, paramValue, ref size);
            zkfp2.ByteArray2Int(paramValue, ref mfpWidth);

            size = 4;
            zkfp2.GetParameters(mDevHandle, 2, paramValue, ref size);
            zkfp2.ByteArray2Int(paramValue, ref mfpHeight);

            FPBuffer = new byte[mfpWidth * mfpHeight];
        }

        private void StartCaptureThread()
        {
            bIsTimeToDie = false;
            Thread captureThread = new Thread(DoCapture) { IsBackground = true };
            captureThread.Start();
        }

        private void DoCapture()
        {
            while (!bIsTimeToDie)
            {
                cbCapTmp = 2048;
                if (zkfp2.AcquireFingerprint(mDevHandle, FPBuffer, CapTmp, ref cbCapTmp) == zkfp.ZKFP_ERR_OK)
                {
                    SendMessage(FormHandle, MESSAGE_CAPTURED_OK, IntPtr.Zero, IntPtr.Zero);
                }
            }
        }

        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == MESSAGE_CAPTURED_OK)
            {
                MemoryStream ms = new MemoryStream();
                BitmapFormat.GetBitmap(FPBuffer, mfpWidth, mfpHeight, ref ms);
                picFPImage.Image = new Bitmap(ms);

                if (IsRegister)
                {
                    Array.Copy(CapTmp, RegTmp, cbCapTmp);
                    RegisterCount++;

                    if (RegisterCount >= REGISTER_FINGER_COUNT)
                    {
                        RegisterCount = 0;
                        CheckExistingFingerprint();
                    }
                }
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }

        private void CheckExistingFingerprint()
        {
            try
            {
                using (var conn = new MySqlConnection("server=localhost;username=root;password=;database=labasan_dtr_system"))
                {
                    conn.Open();
                    var query = "SELECT fingerprintTemplate FROM tbl_emprecord";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var storedTemplate = Convert.FromBase64String(reader.GetString(0));
                            if (zkfp2.DBMatch(mDBHandle, CapTmp, storedTemplate) > 0)
                            {
                                reader.Close();
                                DetermineAdmin(storedTemplate, conn);
                                return;
                            }
                        }
                        UpdateUI("No matching fingerprint found. Please register your fingerprint.", Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateUI($"Error during fingerprint verification: {ex.Message}", Color.Red);
            }
        }

        private void DetermineAdmin(byte[] storedFingerprint, MySqlConnection conn)
        {
            const string query = "SELECT empID, empName, privilege FROM tbl_emprecord WHERE fingerprintTemplate = @fingerprintTemplate";

            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@fingerprintTemplate", Convert.ToBase64String(storedFingerprint));
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string privilege = reader["privilege"].ToString();
                        if (privilege == "admin")
                        {
                            OpenAdminForm(reader["empID"].ToString(), reader["empName"].ToString());
                        }
                        else
                        {
                            UpdateUI("Access denied. You are not an admin.", Color.Red);
                        }
                    }
                }
            }
        }

        private void OpenAdminForm(string empID, string empName)
        {
            if (adminForm == null || adminForm.IsDisposed)
            {
                adminForm = new Admin
                {
                    EmpID = empID,
                    EmpName = empName
                };
                adminForm.FormClosed += (s, e) => adminForm = null;
                adminForm.Show();
                picFPImage.Image = null;
                this.Hide(); // Optionally hide the current form
            }
            else
            {
                adminForm.BringToFront();
                picFPImage.Image = null;
            }
        }

        private void UpdateUI(string message, Color color)
        {
            txtStatus.Text = message;
            txtStatus.ForeColor = color;
        }

        private void AdminLoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseDeviceAndReset();
            zkfp2.Terminate();
        }

        private void CloseDeviceAndReset()
        {
            bIsTimeToDie = true;

            if (mDBHandle != IntPtr.Zero)
            {
                zkfp2.DBFree(mDBHandle);
                mDBHandle = IntPtr.Zero;
            }

            if (mDevHandle != IntPtr.Zero)
            {
                zkfp2.CloseDevice(mDevHandle);
                mDevHandle = IntPtr.Zero;
            }
        }
        private void btnEnter_Click(object sender, EventArgs e)
        {
            // Get values from textboxes
            string enteredID = txtID.Text.Trim();
            string enteredPIN = txtPIN.Text.Trim();

            // Check if fields are empty
            if (string.IsNullOrEmpty(enteredID) || string.IsNullOrEmpty(enteredPIN))
            {
                MessageBox.Show("Please enter both ID and PIN.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Convert PIN to integer (if needed)
            if (!int.TryParse(enteredPIN, out int pin))
            {
                MessageBox.Show("Invalid PIN. Please enter a numeric value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Database query to check if ID and PIN exist and privilege is admin
            string query = "SELECT empID, empName, privilege FROM tbl_emprecord WHERE empID = @empID AND pin = @pin";

            using (MySqlConnection conn = new MySqlConnection("server=localhost;username=root;password=;database=labasan_dtr_system"))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@empID", enteredID);
                        cmd.Parameters.AddWithValue("@pin", pin);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Extract data
                                string empID = reader["empID"].ToString();
                                string empName = reader["empName"].ToString();
                                string privilege = reader["privilege"].ToString();

                                // Check privilege
                                if (privilege == "admin")
                                {
                                    // Pass data to Admin form using public properties
                                    Admin adminForm = new Admin
                                    {
                                        EmpID = empID,
                                        EmpName = empName,
                                        EmpPIN = pin
                                    };
                                    adminForm.Show(); // Open Admin form
                                    this.Hide(); // Optionally hide the current form
                                }
                                else
                                {
                                    MessageBox.Show("Access Denied: You do not have admin privileges.", "Access Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid ID or PIN. Please try again.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            // Clear fields when the form becomes visible
            if (this.Visible)
            {
                txtID.Text = string.Empty;
                txtPIN.Text = string.Empty;
            }
        }
    }
}
