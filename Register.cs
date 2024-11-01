using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using libzkfpcsharp;
using MySql.Data.MySqlClient;
using System.IO;
using Sample;

namespace LNHS_DTR_SYSTEM
{
    public partial class Register : Form
    {
        IntPtr mDevHandle = IntPtr.Zero;
        IntPtr mDBHandle = IntPtr.Zero;
        IntPtr FormHandle = IntPtr.Zero;
        bool bIsTimeToDie = false;
        bool IsRegister = false;
        byte[] FPBuffer;
        int RegisterCount = 0;
        const int REGISTER_FINGER_COUNT = 3;

        byte[][] RegTmps = new byte[3][]; // 3 fingerprint samples
        byte[] RegTmp = new byte[2048]; // Final template after merging
        byte[] CapTmp = new byte[2048]; // Captured fingerprint template
        int cbCapTmp = 2048;
        int cbRegTmp = 0;
        int iFid = 1;

        private int mfpWidth = 0;
        private int mfpHeight = 0;

        const int MESSAGE_CAPTURED_OK = 0x0400 + 6;

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        public Register()
        {
            InitializeComponent();
            this.FormClosing += Register_FormClosing; // Add FormClosing event handler
        }

        private void Register_Load(object sender, EventArgs e)
        {
            InitializeFingerprintDevice(); // Initialize the device when the form is created
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
                }
                else
                {
                    zkfp2.Terminate();
                    txtStatus.Text = "No device connected!";
                }
            }
            else
            {
                txtStatus.Text = $"Initialize fail, ret={ret}!";
                MessageBox.Show("Error initializing fingerprint SDK. Please check device connection and SDK libraries.");
            }
        }

        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseDeviceAndReset();
            zkfp2.Terminate();
        }

        private void btnCaptureFP_Click(object sender, EventArgs e)
        {
            int ret = zkfp.ZKFP_ERR_OK;
            if (IntPtr.Zero == (mDevHandle = zkfp2.OpenDevice(cmbIdx.SelectedIndex)))
            {
                txtStatus.Text = "OpenDevice fail";
                return;
            }
            if (IntPtr.Zero == (mDBHandle = zkfp2.DBInit()))
            {
                txtStatus.Text = "Init DB fail";
                zkfp2.CloseDevice(mDevHandle);
                mDevHandle = IntPtr.Zero;
                return;
            }

            RegisterCount = 0;
            cbRegTmp = 0;
            iFid = 1;
            for (int i = 0; i < 3; i++)
            {
                RegTmps[i] = new byte[2048];
            }
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
                txtStatus.Text = "Please press your finger 3 times!";
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
                Thread.Sleep(200);
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
                            if (RegisterCount > 0 && zkfp2.DBMatch(mDBHandle, CapTmp, RegTmps[RegisterCount - 1]) <= 0)
                            {
                                txtStatus.Text = "Please press the same finger 3 times.";
                                return;
                            }
                            Array.Copy(CapTmp, RegTmps[RegisterCount], cbCapTmp);
                            RegisterCount++;
                            if (RegisterCount >= REGISTER_FINGER_COUNT)
                            {
                                RegisterCount = 0;
                                txtStatus.Text = "Checking for existing fingerprints...";
                                CheckExistingFingerprint();
                            }
                            else
                            {
                                txtStatus.Text = "You need to press the fingerprint " + (REGISTER_FINGER_COUNT - RegisterCount) + " more times";
                            }
                        }
                    }
                    break;

                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

        public byte[] Base64ToBlob(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        private void CheckExistingFingerprint()
        {
            try
            {
                string connectionString = "server=localhost;username=root;password=;database=labasan_dtr_system";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string selectQuery = "SELECT empID, fingerprintTemplate FROM tbl_emprecord";
                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int empID = reader.GetInt32(0);
                                string storedFingerprint = reader.GetString(1);

                                byte[] storedTemplate = Base64ToBlob(storedFingerprint);

                                if (zkfp2.DBMatch(mDBHandle, CapTmp, storedTemplate) > 0)
                                {
                                    txtStatus.Text = "Fingerprint already registered for employee " + empID;
                                    return;
                                }
                            }
                        }
                    }

                    if (zkfp2.DBMerge(mDBHandle, RegTmps[0], RegTmps[1], RegTmps[2], RegTmp, ref cbRegTmp) == zkfp.ZKFP_ERR_OK &&
                        zkfp2.DBAdd(mDBHandle, iFid, RegTmp) == zkfp.ZKFP_ERR_OK)
                    {
                        iFid++;
                        txtStatus.Text = "Fingerprint captured and saved successfully!";
                    }
                    else
                    {
                        txtStatus.Text = "New Employee Data Saved";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while checking fingerprints: " + ex.Message);
            }
            finally
            {
                IsRegister = false;
            }
        }

        private void ClearFieldsWithoutResettingFingerprint()
        {
            txtEmpID.Clear();
            txtEmpName.Clear();
            txtStatus.Clear();
            picFPImage.Image = null;
        }

        private void SaveEmployeeData(string base64Fingerprint)
        {
            try
            {
                string connectionString = "server=localhost;username=root;password=;database=labasan_dtr_system";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO tbl_emprecord (empID, empName, fingerprintTemplate) VALUES (@empID, @empName, @fingerprintTemplate)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@empID", txtEmpID.Text);
                        cmd.Parameters.AddWithValue("@empName", txtEmpName.Text);
                        cmd.Parameters.AddWithValue("@fingerprintTemplate", base64Fingerprint);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Employee data saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving employee data: " + ex.Message);
            }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (RegTmp == null || cbRegTmp <= 0 || string.IsNullOrEmpty(txtEmpID.Text) || string.IsNullOrEmpty(txtEmpName.Text))
            {
                MessageBox.Show("Please capture a fingerprint and fill in all employee details.");
                return;
            }

            string base64Fingerprint = zkfp2.BlobToBase64(RegTmp, cbRegTmp);
            SaveEmployeeData(base64Fingerprint);
            ClearFieldsWithoutResettingFingerprint();
        }
    }
}
