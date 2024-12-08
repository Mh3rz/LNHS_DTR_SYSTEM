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
using System.Timers;

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
            txtStatus.Text = "Please place your finger on the scanner to record your attendance for today. Thank you!";
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
                txtStatus.Text = "Please place your finger on the scanner to record your attendance for today. Thank you!";
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

                                    // Determine and set the status (IN or OUT) based on the employee's last record for today
                                    DetermineInOutStatus(empID, empName, conn);
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
                UpdateUI("An error occurred while checking fingerprints: " + ex.Message + "Please check the Xampp Application if MySQL has Started.", Color.Red);
            }
        }


        private void DetermineInOutStatus(int empID, string empName, MySqlConnection conn)
        {
            isInsertingAttendance = true; // Set flag to true before inserting

            // Get today's date
            DateTime today = DateTime.Now.Date;

            // Query to count today's attendance records
            string countAttendanceQuery = "SELECT COUNT(*) FROM tbl_attendance_record WHERE empID = @empID AND date = @date";
            int recordCount = 0;

            using (MySqlCommand countCmd = new MySqlCommand(countAttendanceQuery, conn))
            {
                countCmd.Parameters.AddWithValue("@empID", empID);
                countCmd.Parameters.AddWithValue("@date", today);

                recordCount = Convert.ToInt32(countCmd.ExecuteScalar());

                // Log the record count to the console for debugging
                Console.WriteLine($"Record count for employee ID {empID} on {today:yyyy-MM-dd}: {recordCount}");
                if (recordCount >= 4) // If there are already 4 records
                {
                    UpdateUI("Attendance limit reached for today. Cannot record more entries.", Color.Red);
                    isInsertingAttendance = false; // Reset flag after checking
                    return; // Enforce the limit with no bypass option
                }
            }

            // Determine the last status
            string selectAttendanceQuery = "SELECT status, time, entry_rank FROM tbl_attendance_record " +
                                           "WHERE empID = @empID AND date = @date " +
                                           "ORDER BY id DESC LIMIT 1";

            string newStatus = "IN"; // Default to IN status
            string newEntry = "first"; // Default for First Entry per employee
            TimeSpan lastTime = TimeSpan.Zero; // Initialize to zero

            using (MySqlCommand cmd = new MySqlCommand(selectAttendanceQuery, conn))
            {
                cmd.Parameters.AddWithValue("@empID", empID);
                cmd.Parameters.AddWithValue("@date", today);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var lastStatus = reader["status"].ToString();
                        var lastEntry = reader["entry_rank"].ToString();
                        lastTime = (TimeSpan)reader["time"]; // Cast the time correctly

                        TimeSpan currentTime = DateTime.Now.TimeOfDay; // Get the current time

                        if (lastStatus == "IN" && lastEntry == "first")
                        {
                            newStatus = "OUT";
                            newEntry = "second";

                            if (currentTime < new TimeSpan(12, 0, 0) || currentTime > new TimeSpan(13, 0, 0))
                            {
                                //if (!ShowBypassMessage("YOU ALREADY CLOCKED IN!!! You can only CLOCK OUT between 11:00 AM and 1:00 PM. Do you want to bypass this restriction?"))
                                if (!ShowBypassMessage("Time-OUT Restriction Active!!!", "You can only CLOCK OUT at the scheduled time (12:00 PM to 1:00 PM).", "Do you want to proceed with clocking out?"))
                                {
                                    isInsertingAttendance = false; // Reset flag after checking
                                    return;
                                }
                            } 
                        }
                        else if (lastStatus == "OUT" && lastEntry == "second")
                        {
                            newStatus = "IN";
                            newEntry = "third";

                            if (currentTime < new TimeSpan(12, 10, 0) || currentTime >= new TimeSpan(13, 0, 0))
                            {
                                if (!ShowBypassMessage("Time-IN Restriction Active!!!", "You can only CLOCK IN at the scheduled time (12:10 PM to 1:00 PM).", "Do you want to proceed with clocking in?"))
                                {
                                    isInsertingAttendance = false; // Reset flag after checking
                                    return;
                                }
                            }
                        }

                        else if (lastStatus == "IN" && lastEntry == "third")
                        {
                            newStatus = "OUT";
                            newEntry = "fourth";

                            if (currentTime < new TimeSpan(16, 0, 0) || currentTime >= new TimeSpan(18, 0, 0))
                            {
                                if (!ShowBypassMessage("Time-Out Restriction Active!!!", "You can only CLOCK OUT at the scheduled time (4:00 PM to 6:00 PM).", "Do you want to proceed with clocking out?"))
                                {
                                    isInsertingAttendance = false; // Reset flag after checking
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            // Insert the attendance record if all conditions are met or bypassed
            string insertQuery = "INSERT INTO tbl_attendance_record (empID, empName, date, day, time, status, entry_rank) " +
                                 "VALUES (@empID, @empName, @date, @day, @time, @status, @entry_rank)";

            using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
            {
                insertCmd.Parameters.AddWithValue("@empID", empID);
                insertCmd.Parameters.AddWithValue("@empName", empName);
                insertCmd.Parameters.AddWithValue("@date", today);
                insertCmd.Parameters.AddWithValue("@day", DateTime.Now.DayOfWeek.ToString());
                insertCmd.Parameters.AddWithValue("@time", DateTime.Now.TimeOfDay); // Set current time
                insertCmd.Parameters.AddWithValue("@status", newStatus);
                insertCmd.Parameters.AddWithValue("@entry_rank", newEntry);
                insertCmd.ExecuteNonQuery();
            }

            UpdateUI($"Attendance recorded successfully: {newStatus} for {empName}.", Color.Green);
            isInsertingAttendance = false; // Reset flag after insertion
                                           
            //picFPImage.Image = null;// Clear picImageFP by setting its image to null
        }

        // Helper method to show a bypass confirmation message box
        private bool ShowBypassMessage(string boldText, string regularText, string bypassMessage)
        {
            DialogResult result = CustomMessageBox.Show(boldText, regularText, bypassMessage);

            if (result == DialogResult.No)
            {
                // Update txtStatus to display the message
                txtStatus.Text = "Press your finger at the fingerprint scanner.";
                txtStatus.ForeColor = Color.Blue;

                // Clear picImageFP by setting its image to null
                picFPImage.Image = null;

                return false; // Return false since the user chose not to bypass
            }

            return true; // Return true if the user clicks "Yes"
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
