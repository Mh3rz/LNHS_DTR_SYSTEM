using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace LNHS_DTR_SYSTEM
{
    public partial class AdminEmployeeList : Form
    {
        private MySqlConnection conn;

        public AdminEmployeeList()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            LoadEmployeeData();

            // Set up privilege ComboBox with "member" and "admin" options
            cmbPrivilege.Items.AddRange(new string[] { "member", "admin" });
            cmbPrivilege.SelectedIndex = 0;  // Default to "member"
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = "server=localhost;username=root;password=;database=labasan_dtr_system";
            conn = new MySqlConnection(connectionString);
        }

        private void LoadEmployeeData()
        {
            try
            {
                string query = "SELECT id AS 'ID', empID AS 'Employee ID', empName AS 'Employee Name', privilege AS 'Privilege' FROM tbl_emprecord";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGVEmpList.DataSource = dt;

                // Hide the ID column (primary key)
                dataGVEmpList.Columns["ID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employee data: " + ex.Message);
            }
        }

        private void dataGVEmpList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGVEmpList.Rows[e.RowIndex];

                // Populate the text boxes with the selected row data
                txtHiddenID.Text = row.Cells["ID"].Value?.ToString();
                txtEmpID.Text = row.Cells["Employee ID"].Value?.ToString();
                txtxEmpName.Text = row.Cells["Employee Name"].Value?.ToString();

                // Set ComboBox to the value in the database (either "member" or "admin")
                string privilegeValue = row.Cells["Privilege"].Value?.ToString();
                if (privilegeValue == "member" || privilegeValue == "admin")
                {
                    cmbPrivilege.SelectedItem = privilegeValue;
                }
                else
                {
                    cmbPrivilege.SelectedIndex = 0; // Default to "member" if value is invalid
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadEmployeeData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Enable text boxes for editing
            txtEmpID.Enabled = true;
            txtxEmpName.Enabled = true;
            cmbPrivilege.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHiddenID.Text))
            {
                MessageBox.Show("Please select an employee record to update.");
                return;
            }

            string query = "UPDATE tbl_emprecord SET empID = @empID, empName = @empName, privilege = @privilege WHERE id = @id";

            using (MySqlConnection connection = new MySqlConnection(conn.ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", txtHiddenID.Text);
                cmd.Parameters.AddWithValue("@empID", txtEmpID.Text);
                cmd.Parameters.AddWithValue("@empName", txtxEmpName.Text);
                cmd.Parameters.AddWithValue("@privilege", cmbPrivilege.SelectedItem.ToString());

                try
                {
                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record updated successfully!");
                        LoadEmployeeData(); // Refresh the data grid view
                    }
                    else
                    {
                        MessageBox.Show("Update failed. Please check the employee record.");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            // Enable text boxes for editing
            txtEmpID.Enabled = false;
            txtxEmpName.Enabled = false;
            cmbPrivilege.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHiddenID.Text))
            {
                MessageBox.Show("Please select an employee record to delete.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM tbl_emprecord WHERE id = @id";

                using (MySqlConnection connection = new MySqlConnection(conn.ConnectionString))
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", txtHiddenID.Text);

                    try
                    {
                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record deleted successfully!");
                            ClearTextBoxes(); // Clear input fields
                            LoadEmployeeData(); // Refresh the data grid view
                        }
                        else
                        {
                            MessageBox.Show("Delete failed. Please check the employee record.");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void ClearTextBoxes()
        {
            txtHiddenID.Clear();
            txtEmpID.Clear();
            txtxEmpName.Clear();
            cmbPrivilege.SelectedIndex = 0;  // Reset to "member"
            txtEmpID.Enabled = false;
            txtxEmpName.Enabled = false;
            cmbPrivilege.Enabled = false;
        }
    }
}