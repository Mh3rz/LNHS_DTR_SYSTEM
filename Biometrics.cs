using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libzkfpcsharp;
using MySql.Data.MySqlClient;
using System.IO;
using Sample;
using System.Runtime.InteropServices;
using System.Threading;

namespace LNHS_DTR_SYSTEM
{
    public partial class Biometrics : Form
    {
        IntPtr mDevHandle = IntPtr.Zero;
        IntPtr mDBHandle = IntPtr.Zero;
        IntPtr FormHandle = IntPtr.Zero;
        bool bIsTimeToDie = false;
        bool IsRegister = false;
        byte[] FPBuffer;
        int RegisterCount = 0;
        const int REGISTER_FINGER_COUNT = 1;

        byte[] RegTmp = new byte[2048]; // Final template after capturing one fingerprint
        byte[] CapTmp = new byte[2048]; // Captured fingerprint template
        int cbCapTmp = 2048;
        int cbRegTmp = 0;
        int iFid = 1;

        private int mfpWidth = 0;
        private int mfpHeight = 0;

        const int MESSAGE_CAPTURED_OK = 0x0400 + 6;

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        public Biometrics()
        {
            InitializeComponent();
            this.FormClosing += Biometrics_FormClosing;

            // Display the current day in the txtDay TextBox
            txtDay.Text = DateTime.Now.DayOfWeek.ToString();

            // Display the current date in the txtDate TextBox
            txtDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");

            // Display the current time in the txtTime TextBox
            txtTime.Text = DateTime.Now.ToString("hh:mm tt");
        }

        private void Biometrics_Load(object sender, EventArgs e)
        {
            InitializeFingerprintDevice();
        }

        private void InitializeFingerprintDevice()
        {
            FormHandle = this.Handle;
            cmbIdx.Items.Clear();
            int ret = zkfperrdef.ZKFP_ERR_OK;
            ret = zkfp2.Init();

            if (ret == zkfperrdef.ZKFP_ERR_OK)
            {
                int nCount = zkfp2.GetDeviceCount();
                if (nCount > 0)
                {
                    for (int i = 0; i < nCount; i++)
                    {
                        cmbIdx.Items.Add(i.ToString());
                    }
                    cmbIdx.SelectedIndex = 0;
                    txtStatus.Text = $"Initialize Successful, ret={ret}!";
                }
                else
                {
                    zkfp2.Terminate();
                    txtStatus.Text = "No device connected!";
                }
            }
            else
            {
                txtStatus.Text = $"Initialize failed, ret={ret}!";
                MessageBox.Show("Error initializing fingerprint SDK. Please check device connection and SDK libraries.");
            }

            if (IntPtr.Zero == (mDevHandle = zkfp2.OpenDevice(cmbIdx.SelectedIndex)))
            {
                txtStatus.Text = "OpenDevice failed";
                return;
            }
            if (IntPtr.Zero == (mDBHandle = zkfp2.DBInit()))
            {
                txtStatus.Text = "Init DB failed";
                zkfp2.CloseDevice(mDevHandle);
                mDevHandle = IntPtr.Zero;
                return;
            }

            RegisterCount = 0;
            cbRegTmp = 0;
            iFid = 1;

            byte[] paramValue = new byte[4];
            int size = 4;
            zkfp2.GetParameters(mDevHandle, 1, paramValue, ref size);
            zkfp2.ByteArray2Int(paramValue, ref mfpWidth);

            size = 4;
            zkfp2.GetParameters(mDevHandle, 2, paramValue, ref size);
            zkfp2.ByteArray2Int(paramValue, ref mfpHeight);
            FPBuffer = new byte[mfpWidth * mfpHeight];

            Thread captureThread = new Thread(new ThreadStart(DoCapture));
            captureThread.IsBackground = true;
            captureThread.Start();
            bIsTimeToDie = false;
            txtStatus.Text = "Ready to capture fingerprint";

            if (!IsRegister)
            {
                IsRegister = true;
                RegisterCount = 0;
                cbRegTmp = 0;
                txtStatus.Text = "Please press your finger!";
            }
        }

        private void Biometrics_FormClosing(object sender, FormClosingEventArgs e)
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

        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case MESSAGE_CAPTURED_OK:
                    {
                        MemoryStream ms = new MemoryStream();
                        BitmapFormat.GetBitmap(FPBuffer, mfpWidth, mfpHeight, ref ms);
                        Bitmap bmp = new Bitmap(ms);
                        this.picFPImage.Image = bmp;

                        if (IsRegister)
                        {
                            int ret = zkfp.ZKFP_ERR_OK;
                            int fid = 0, score = 0;

                            ret = zkfp2.DBIdentify(mDBHandle, CapTmp, ref fid, ref score);
                            if (zkfp.ZKFP_ERR_OK == ret)
                            {
                                txtStatus.Text = "This finger was already registered by " + fid + "!";
                                return;
                            }

                            if (RegisterCount > 0 && zkfp2.DBMatch(mDBHandle, CapTmp, RegTmp) <= 0)
                            {
                                txtStatus.Text = "Please press the same finger again.";
                                return;
                            }

                            Array.Copy(CapTmp, RegTmp, cbCapTmp);
                            RegisterCount++;

                            if (RegisterCount >= REGISTER_FINGER_COUNT)
                            {
                                RegisterCount = 0;
                                txtStatus.Text = "Checking for existing fingerprints...";
                                CheckExistingFingerprint();
                            }
                            else
                            {
                                txtStatus.Text = "You need to press the fingerprint " + (REGISTER_FINGER_COUNT - RegisterCount) + " more time(s)";
                            }
                        }

                    }
                    break;

                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

        private void DoCapture()
        {
            while (!bIsTimeToDie)
            {
                cbCapTmp = 2048;
                int ret = zkfp2.AcquireFingerprint(mDevHandle, FPBuffer, CapTmp, ref cbCapTmp);
                if (ret == zkfp.ZKFP_ERR_OK)
                {
                    SendMessage(FormHandle, MESSAGE_CAPTURED_OK, IntPtr.Zero, IntPtr.Zero);
                }
            }
        }

        private bool isInsertingAttendance = false; // Flag to prevent concurrent inserts

        private void CheckExistingFingerprint()
        {
            try
            {
                string connectionString = "server=localhost;username=root;password=;database=labasan_dtr_system";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string selectQuery = "SELECT empID, empName, fingerprintTemplate FROM tbl_emprecord";
                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            bool matchFound = false;

                            while (reader.Read())
                            {
                                int empID = reader.GetInt32(0);
                                string empName = reader.GetString(1);
                                string storedFingerprint = reader.GetString(2);
                                byte[] storedTemplate = Convert.FromBase64String(storedFingerprint);

                                if (zkfp2.DBMatch(mDBHandle, CapTmp, storedTemplate) > 0)
                                {
                                    matchFound = true;

                                    // Close the reader before proceeding to check the latest attendance record
                                    reader.Close();

                                    // Determine and set the status (IN or OUT) based on the employee's last record for today
                                    DetermineInOutStatus(empID, empName, conn);
                                    break;
                                }
                            }

                            if (!matchFound)
                            {
                                UpdateUI("No match found. Please try again.", Color.Red, string.Empty, string.Empty);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateUI("An error occurred while checking fingerprints: " + ex.Message, Color.Red, string.Empty, string.Empty);
            }
        }


        private void DetermineInOutStatus(int empID, string empName, MySqlConnection conn)
        {
            isInsertingAttendance = true; // Set flag to true before inserting

            // Query to check if there is an existing entry for this employee today
            string selectAttendanceQuery = "SELECT status FROM tbl_attendance_record " +
                                           "WHERE empID = @empID AND date = @date " +
                                           "ORDER BY id DESC LIMIT 1";
            string newStatus = "IN"; // Default to IN status

            using (MySqlCommand cmd = new MySqlCommand(selectAttendanceQuery, conn))
            {
                cmd.Parameters.AddWithValue("@empID", empID);
                cmd.Parameters.AddWithValue("@date", DateTime.Now.Date);

                var lastStatus = cmd.ExecuteScalar();
                if (lastStatus != null && lastStatus.ToString() == "IN")
                {
                    newStatus = "OUT"; // If the last status was IN, set new status to OUT
                }
            }

            // Insert the attendance record
            string insertQuery = "INSERT INTO tbl_attendance_record (empID, empName, date, day, time, status) " +
                                 "VALUES (@empID, @empName, @date, @day, @time, @status)";

            using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
            {
                insertCmd.Parameters.AddWithValue("@empID", empID);
                insertCmd.Parameters.AddWithValue("@empName", empName);
                insertCmd.Parameters.AddWithValue("@date", DateTime.Now.Date);
                insertCmd.Parameters.AddWithValue("@day", DateTime.Now.DayOfWeek.ToString());
                insertCmd.Parameters.AddWithValue("@time", DateTime.Now.TimeOfDay);
                insertCmd.Parameters.AddWithValue("@status", newStatus);
                insertCmd.ExecuteNonQuery();
            }

            UpdateUI($"Attendance recorded successfully: {newStatus} for {empName}.", Color.Green, empID.ToString(), empName);
            isInsertingAttendance = false; // Reset flag after insertion
        }

        private void UpdateUI(string message, Color color, string empID, string empName)
        {
            txtStatus.Text = message;
            txtStatus.ForeColor = color;
            // Optionally, you can display the empID and empName in other UI elements as needed
        }
    }
}
