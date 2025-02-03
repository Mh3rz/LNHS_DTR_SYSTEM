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

        //private void CheckExistingFingerprint()
        //{
        //    try
        //    {
        //        string connectionString = "server=localhost;username=root;password=;database=labasan_dtr_system";
        //        using (MySqlConnection conn = new MySqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            string selectQuery = "SELECT empID, empName, fingerprintTemplate FROM tbl_emprecord";
        //            using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
        //            {
        //                using (MySqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    bool matchFound = false;

        //                    while (reader.Read())
        //                    {
        //                        int empID = reader.GetInt32(0);
        //                        string empName = reader.GetString(1);
        //                        string storedFingerprint = reader.GetString(2);
        //                        byte[] storedTemplate = Convert.FromBase64String(storedFingerprint);

        //                        if (zkfp2.DBMatch(mDBHandle, CapTmp, storedTemplate) > 0)
        //                        {
        //                            matchFound = true;

        //                            // Close the reader before proceeding to check the latest attendance record
        //                            reader.Close();

        //                            // Determine and set the status (IN or OUT) based on the employee's last record for today
        //                            DetermineInOutStatus(empID, empName, conn);
        //                            break;
        //                        }
        //                    }

        //                    if (!matchFound)
        //                    {
        //                        UpdateUI("No match found. Please try again.", Color.Red);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        UpdateUI("An error occurred while checking fingerprints: " + ex.Message + "Please check the Xampp Application if MySQL has Started.", Color.Red);
        //    }
        //}

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

                                    // Handle attendance logic for matched fingerprint
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

        private void HandleAttendance(int empID, string empName, MySqlConnection conn)
        {
            DateTime now = DateTime.Now;
            string today = now.ToString("yyyy-MM-dd");

            // Fetch today's attendance for the user
            MySqlCommand attendanceCmd = new MySqlCommand(
                "SELECT entry_rank FROM tbl_attendance_record WHERE empID = @empID AND date = @today",
                conn
            );
            attendanceCmd.Parameters.AddWithValue("@empID", empID);
            attendanceCmd.Parameters.AddWithValue("@today", today);

            MySqlDataReader attendanceReader = attendanceCmd.ExecuteReader();
            List<string> entryRanks = new List<string>();
            while (attendanceReader.Read())
            {
                entryRanks.Add(attendanceReader["entry_rank"].ToString());
            }
            attendanceReader.Close();

            // Check if "Fourth" entry already exists
            if (entryRanks.Contains("Fourth"))
            {
                UpdateUI($"You have completed your attendance for today, {empName}.", Color.Red);
                return;
            }

            // Determine the new entry rank
            var (entryRank, status) = DetermineEntryRank(entryRanks, now);

            if (entryRank == null)
            {
                UpdateUI($"Invalid time for attendance, {empName}.", Color.Red);
                return;
            }

            // Insert new record into tbl_attendance_record
            MySqlCommand insertCmd = new MySqlCommand(
                "INSERT INTO tbl_attendance_record (empID, empName, date, day, time, status, entry_rank) VALUES (@empID, @empName, @date, @day, @time, @status, @entry_rank)",
                conn
            );
            insertCmd.Parameters.AddWithValue("@empID", empID);
            insertCmd.Parameters.AddWithValue("@empName", empName);
            insertCmd.Parameters.AddWithValue("@date", today);
            insertCmd.Parameters.AddWithValue("@day", now.DayOfWeek.ToString());
            insertCmd.Parameters.AddWithValue("@time", now.ToString("HH:mm:ss"));
            insertCmd.Parameters.AddWithValue("@status", status);
            insertCmd.Parameters.AddWithValue("@entry_rank", entryRank);

            // Execute the insert command
            int rowsAffected = insertCmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                // Play a sound if the data insertion is successful
                PlaySound("C:\\Users\\Mherwin Retanal\\VISUAL BASIC APPLCIATION\\LNHS_DTR_SYSTEM\\sound.mp3");
                using (PopupCard popup = new PopupCard(status))
{
    popup.ShowDialog();
}
                // Success message
                UpdateUI($"Your '{entryRank}' entry has been recorded successfully, {empName}.", Color.Green);
            }
            else
            {
                // Failure message if insertion fails
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

        private (string, string) DetermineEntryRank(List<string> entryRanks, DateTime now)
        {
            TimeSpan currentTime = now.TimeOfDay;

            //if (!entryRanks.Contains("First") && currentTime >= TimeSpan.FromHours(6) && currentTime <= TimeSpan.FromHours(10.99))
            //    return ("First", "IN");
            //if (entryRanks.Contains("First") && !entryRanks.Contains("Second") && currentTime >= TimeSpan.FromHours(11) && currentTime <= TimeSpan.FromHours(12.99))
            //    return ("Second", "OUT");
            //if (entryRanks.Contains("Second") && !entryRanks.Contains("Third") && currentTime >= TimeSpan.FromHours(12.10) && currentTime <= TimeSpan.FromHours(13))
            //    return ("Third", "IN");
            //if (entryRanks.Contains("Third") && !entryRanks.Contains("Fourth") && currentTime >= TimeSpan.FromHours(16) && currentTime <= TimeSpan.FromHours(18))
            //    return ("Fourth", "OUT");

            if (!entryRanks.Contains("First") && currentTime >= TimeSpan.FromHours(6) && currentTime <= TimeSpan.FromHours(10.99))
                return ("First", "IN");
            if (entryRanks.Contains("First") && !entryRanks.Contains("Second") && currentTime >= TimeSpan.FromHours(11) && currentTime <= TimeSpan.FromHours(12.99))
                return ("Second", "OUT");
            if (!entryRanks.Contains("First") && !entryRanks.Contains("Third") && currentTime >= TimeSpan.FromHours(12.10) && currentTime <= TimeSpan.FromHours(13))
                return ("Third", "IN");
            if (!entryRanks.Contains("Third") && currentTime >= TimeSpan.FromHours(12.10) && currentTime <= TimeSpan.FromHours(13))
                return ("Third", "IN");
            if (!entryRanks.Contains("Fourth") && currentTime >= TimeSpan.FromHours(1) && currentTime <= TimeSpan.FromHours(18))
                return ("Fourth", "OUT");



            // If no conditions match, return null values
            return (null, null);
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
