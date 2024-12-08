using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using System.IO;

namespace LNHS_DTR_SYSTEM
{
    public partial class Admin : Form
    {
        //data from adminlogin
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public int EmpPIN { get; set; }

        public Admin()
        {
            InitializeComponent();
        }

        private AdminAccount adminAccountForm;

        private void btnEmployeeList_Click(object sender, EventArgs e)
        {
            AdminEmployeeList adminEmployeeList = new AdminEmployeeList();

            adminEmployeeList.Show();
        }

        private void btnViewDatabase_Click(object sender, EventArgs e)
        {
            AdminViewDatabase adminViewDatabase = new AdminViewDatabase();
            
            adminViewDatabase.Show();
        }

        private void btnGenerateDTR_Click(object sender, EventArgs e)
        {
            AdminGenerateDTR adminGenerateDTR = new AdminGenerateDTR();

            adminGenerateDTR.Show();


            
        }

        private void btnAdminAccount_Click(object sender, EventArgs e)
        {
            // Fetch the latest employee details
            LoadEmployeeDetails();

            // Check if the AdminAccount form is already open
            if (adminAccountForm == null || adminAccountForm.IsDisposed)
            {
                adminAccountForm = new AdminAccount
                {
                    EmpID = EmpID,
                    EmpName = EmpName,
                    EmpPIN = EmpPIN
                };

                adminAccountForm.FormClosed += (s, args) => adminAccountForm = null; // Reset reference when form is closed
                adminAccountForm.Show();
            }
            else
            {
                adminAccountForm.BringToFront();
                adminAccountForm.Focus();
            }
        }

        private void LoadEmployeeDetails()
        {
            string query = "SELECT empID, empName, pin FROM tbl_emprecord WHERE empID = @EmpID";

            try
            {
                using (MySqlConnection conn = new MySqlConnection("server=localhost;username=root;password=;database=labasan_dtr_system"))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmpID", EmpID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                EmpName = reader["empName"].ToString();
                                EmpPIN = reader["pin"] != DBNull.Value ? Convert.ToInt32(reader["pin"]) : 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employee details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
