using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace LNHS_DTR_SYSTEM
{
    public partial class AdminViewDatabase : Form
    {
        public AdminViewDatabase()
        {
            InitializeComponent();
            LoadEmployeeNames(); // Populate ComboBox with employee names
            LoadData();          // Load data on form initialization
            btnViewAllData.Visible = false; // Initially hide "View All Data" button
        }

        // Method to load distinct employee names into ComboBox
        private void LoadEmployeeNames()
        {
            string connectionString = "server=localhost;user=root;password=;database=labasan_dtr_system";
            string query = "SELECT DISTINCT empName FROM tbl_attendance_record ORDER BY empName";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbSearchName.Items.Add(reader["empName"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading employee names: " + ex.Message);
                }
            }
        }

        private void LoadData(DateTime? startDate = null, DateTime? endDate = null, string name = null)
        {
            string connectionString = "server=localhost;user=root;password=;database=labasan_dtr_system";
            string query = "SELECT * FROM tbl_attendance_record WHERE 1=1";

            if (startDate.HasValue)
                query += " AND date >= @StartDate";
            if (endDate.HasValue)
                query += " AND date <= @EndDate";
            if (!string.IsNullOrEmpty(name))
                query += " AND empName = @Name";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                if (startDate.HasValue)
                    cmd.Parameters.AddWithValue("@StartDate", startDate.Value);
                if (endDate.HasValue)
                    cmd.Parameters.AddWithValue("@EndDate", endDate.Value);
                if (!string.IsNullOrEmpty(name))
                    cmd.Parameters.AddWithValue("@Name", name);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGVDataEntry.DataSource = dt;
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (dataGVDataEntry.Rows.Count > 0)
            {
                StringBuilder csvData = new StringBuilder();

                foreach (DataGridViewColumn column in dataGVDataEntry.Columns)
                {
                    csvData.Append(column.HeaderText + ",");
                }
                csvData.AppendLine();

                foreach (DataGridViewRow row in dataGVDataEntry.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        csvData.Append(cell.Value?.ToString().Replace(",", " ") + ",");
                    }
                    csvData.AppendLine();
                }

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "AttendanceData.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, csvData.ToString());
                    MessageBox.Show("Data Exported Successfully", "Info");
                }
            }
            else
            {
                MessageBox.Show("No data available to download.", "Info");
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime? startDate = dtpStartDate.Checked ? dtpStartDate.Value : (DateTime?)null;
            DateTime? endDate = dtpEndDate.Checked ? dtpEndDate.Value : (DateTime?)null;
            string name = cmbSearchName.SelectedItem?.ToString();

            LoadData(startDate, endDate, name);

            // Show "View All Data" button if a specific name is selected
            btnViewAllData.Visible = !string.IsNullOrEmpty(name);
        }

        private void btnViewAllData_Click_(object sender, EventArgs e)
        {
            cmbSearchName.SelectedIndex = -1; // Clear ComboBox selection
            LoadData();                       // Reload all data
            btnViewAllData.Visible = false;   // Hide "View All Data" button
        }

        private void cmbSearchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnViewAllData.Visible = cmbSearchName.SelectedIndex != -1;
        }

        private void btnViewAllData_Click(object sender, EventArgs e)
        {
            cmbSearchName.SelectedIndex = -1; // Clear ComboBox selection
            LoadData();                       // Reload all data
            btnViewAllData.Visible = false;   // Hide "View All Data" button
        }

        
    }
}
