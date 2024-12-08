using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LNHS_DTR_SYSTEM
{
    public partial class AdminAccount : Form
    {
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public int EmpPIN { get; set; }
        private readonly string connectionString = "server=localhost;username=root;password=;database=labasan_dtr_system";

        public AdminAccount()
        {
            InitializeComponent();
        }

        private void AdminAccount_Load(object sender, EventArgs e)
        {
            txtAdminID.Text = EmpID;
            txtAdminName.Text = EmpName;

            // Fetch latest PIN from database in case it wasn't properly passed
            if (EmpPIN == 0)
            {
                EmpPIN = FetchEmpPINFromDatabase(EmpID);
            }

            if (EmpPIN == 0) // Still invalid
            {
                txtPIN.Text = "PIN not set";
                txtPIN.ForeColor = Color.Red;
                txtPIN.Font = new Font("Courier New", txtPIN.Font.Size);

                btnSetupPIN.Visible = true;
                btnChangePIN.Visible = false; // Hide Change button
                btnUpdatePIN.Visible = false;
            }
            else
            {
                txtPIN.PasswordChar = '*'; // Mask PIN
                txtPIN.Text = EmpPIN.ToString();
                txtPINStatus.Text = "You can change your PIN anytime, press Change";
                txtPINStatus.ForeColor = Color.Blue;

                btnSetupPIN.Visible = false;
                btnChangePIN.Visible = true;
                btnUpdatePIN.Visible = false;
            }

            txtPIN.ReadOnly = true;
        }


        private void btnSetupPIN_Click(object sender, EventArgs e)
        {
            txtPIN.Text = "Insert your PIN";
            txtPIN.ForeColor = Color.Black;
            txtPIN.ReadOnly = false;

            txtPINStatus.Text = "Press Update to save the PIN";
            txtPINStatus.ForeColor = Color.Green;

            btnSetupPIN.Visible = false;
            btnUpdatePIN.Visible = true;
        }

        private void btnChangePIN_Click(object sender, EventArgs e)
        {
            txtPIN.ReadOnly = false;
            btnChangePIN.Visible = false;
            btnUpdatePIN.Visible = true;
        }

        private void btnUpdatePIN_Click(object sender, EventArgs e)
        {
            if (!IsValidPIN(txtPIN.Text))
                return;

            EmpPIN = int.Parse(txtPIN.Text);
            SavePINToDatabase(EmpPIN);

            txtPIN.ReadOnly = true;
            txtPINStatus.Text = "PIN updated successfully!";
            txtPINStatus.ForeColor = Color.Blue;

            btnUpdatePIN.Visible = false;
            btnChangePIN.Visible = true;
        }

        private bool IsValidPIN(string pinText)
        {
            if (string.IsNullOrWhiteSpace(pinText))
            {
                MessageBox.Show("PIN cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(pinText, out int newPIN) || newPIN <= 0)
            {
                MessageBox.Show("PIN must be a positive number!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void SavePINToDatabase(int newPIN)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE tbl_emprecord SET pin = @EmpPIN WHERE empID = @EmpID";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmpPIN", newPIN);
                        cmd.Parameters.AddWithValue("@EmpID", EmpID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("PIN updated in the database successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update PIN. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int FetchEmpPINFromDatabase(string empID)
        {
            int pin = 0;
            string query = "SELECT pin FROM tbl_emprecord WHERE empID = @EmpID";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmpID", empID);
                        pin = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching PIN: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return pin;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (btnUpdatePIN.Visible)
            {
                DialogResult result = MessageBox.Show(
                    "You have unsaved changes. Are you sure you want to close without saving?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.No)
                    return;
            }

            this.Close();
        }
    }
}
