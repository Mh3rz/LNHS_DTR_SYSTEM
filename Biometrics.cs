using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using libzkfpcsharp;
using MySql.Data.MySqlClient;
using System.IO;
using Sample;
using System.Runtime.InteropServices;
using System.Threading;
using System.Media;
using WMPLib;

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

        // Add a Timer object as a class-level variable
        private System.Windows.Forms.Timer resetTimer = new System.Windows.Forms.Timer();

        public Biometrics()
        {
            InitializeComponent();
            this.FormClosing += Biometrics_FormClosing;

            // Set up the reset Timer
            resetTimer.Interval = 5000; // 5 seconds
            resetTimer.Tick += ResetTimer_Tick;

            // Set up the Timer for date and time updates
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        // Timer Tick event to reset the UI after 5 seconds
        private void ResetTimer_Tick(object sender, EventArgs e)
        {
            // Stop the timer to prevent repetitive execution
            resetTimer.Stop();

            // Clear the fingerprint image and reset the status message
            picFPImage.Image = null;
            txtStatus.Text = "Please have your finger scanned for your Daily Time Record.";
            txtStatus.ForeColor = Color.Blue;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the day, date, and time
            txtDay.Text = DateTime.Now.DayOfWeek.ToString();
            txtDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
            txtTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
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
                txtStatus.Text = "ZKTeco Sensor connection failed. Please verify that the device is properly connected and then restart the application.";
                txtStatus.ForeColor = Color.OrangeRed;
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
                txtStatus.Text = "Please have your finger scanned for your Daily Time Record.";
                txtStatus.ForeColor = Color.Blue;
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
                            Array.Copy(CapTmp, RegTmp, cbCapTmp);
                            RegisterCount++;

                            if (RegisterCount >= REGISTER_FINGER_COUNT)
                            {
                                RegisterCount = 0;
                                txtStatus.Text = "Checking the database for your record. Please wait...";
                                txtStatus.ForeColor = Color.BlueViolet;
                                CheckExistingFingerprint();
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

                                    // Handle attendance logic for matched fingerprint ALSO CHECK TIME SO WE CAN COMPARE IF 5 MINS IS ELAPSED FROm THE TIME NOW
                                    HandleAttendance(empID, empName, conn);
                                    break;
                                }
                            }

                            if (!matchFound)
                            {
                                UpdateUI("No match found. Please try again.", Color.Red);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateUI("An error occurred while checking fingerprints: " + ex.Message + " Please check the XAMPP Application to ensure MySQL is started.", Color.Red);
            }
        }

        // edit here the time interval
        private void HandleAttendance(int empID, string empName, MySqlConnection conn)
        {
            DateTime now = DateTime.Now;
            string today = now.ToString("yyyy-MM-dd");

            // Fetch the latest attendance record for the user
            MySqlCommand latestAttendanceCmd = new MySqlCommand(
                "SELECT time FROM tbl_attendance_record WHERE empID = @empID AND date = @today ORDER BY time DESC LIMIT 1",
                conn
            );
            latestAttendanceCmd.Parameters.AddWithValue("@empID", empID);
            latestAttendanceCmd.Parameters.AddWithValue("@today", today);

            object lastAttendanceObj = latestAttendanceCmd.ExecuteScalar();

            if (lastAttendanceObj != null)
            {
                DateTime lastAttendanceTime = DateTime.Parse(lastAttendanceObj.ToString());
                TimeSpan timeDifference = now - lastAttendanceTime;

                if (timeDifference.TotalMinutes < 3)
                {
                    using (PopupCardMessage popup = new PopupCardMessage($"Your attendance has been recorded earlier. Please wait {3 - (int)timeDifference.TotalMinutes} minutes before scanning again."))
                    {
                        popup.ShowDialog();
                    }
                    return;
                }
            }

            // Insert new record into tbl_attendance_record
            MySqlCommand insertCmd = new MySqlCommand(
                "INSERT INTO tbl_attendance_record (empID, empName, date, day, time) VALUES (@empID, @empName, @date, @day, @time)",
                conn
            );
            insertCmd.Parameters.AddWithValue("@empID", empID);
            insertCmd.Parameters.AddWithValue("@empName", empName);
            insertCmd.Parameters.AddWithValue("@date", today);
            insertCmd.Parameters.AddWithValue("@day", now.DayOfWeek.ToString());
            insertCmd.Parameters.AddWithValue("@time", now.ToString("HH:mm:ss"));

            int rowsAffected = insertCmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                PlaySound("C:\\Users\\Mherwin Retanal\\VISUAL BASIC APPLCIATION\\LNHS_DTR_SYSTEM\\sound.mp3");
                using (PopupCard popup = new PopupCard(empName))
                {
                    popup.ShowDialog();
                }
                UpdateUI($"Your attendance has been recorded successfully, {empName}.", Color.Green);
            }
            else
            {
                UpdateUI($"Failed to record your attendance, {empName}.", Color.Red);
            }
        }

        private void PlaySound(string filePath)
        {
            // Check file extension to decide playback method
            string extension = System.IO.Path.GetExtension(filePath).ToLower();

            if (extension == ".wav")
            {
                // Use SoundPlayer for .wav files
                SoundPlayer player = new SoundPlayer(filePath);
                player.Play();
            }
            else if (extension == ".mp3")
            {
                // Use Windows Media Player for .mp3 files
                WindowsMediaPlayer player = new WindowsMediaPlayer();
                player.URL = filePath;
                player.controls.play();
            }
            else
            {
                // Handle unsupported file formats
                UpdateUI("Unsupported sound file format.", Color.Red);
            }
        }
       



        private void UpdateUI(string message, Color color)
        {
            txtStatus.Text = message;
            txtStatus.ForeColor = color;
            // Optionally, you can display the empID and empName in other UI elements as needed

            // Start the reset timer after updating the UI
            resetTimer.Start();
        }
    }
}
