using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LNHS_DTR_SYSTEM
{
    public partial class AdminGenerateDTR : Form
    {
        public AdminGenerateDTR()
        {
            InitializeComponent();
            this.Load += AdminGenerateDTR_Load;
        }

        private void AdminGenerateDTR_Load(object sender, EventArgs e)
        {
            // Set the year range
            int startYear = 2024;
            int endYear = 3000;

            // Disable IntegralHeight and set a fixed dropdown height for year selection
            cmbYearSelection.IntegralHeight = false;
            cmbYearSelection.DropDownHeight = 100; // Adjust this value as needed

            // Populate the ComboBox with years from 2024 to 3000
            for (int year = startYear; year <= endYear; year++)
            {
                cmbYearSelection.Items.Add(year);
            }

            // Set the current year as the default selected year
            int currentYear = DateTime.Now.Year;
            int yearIndex = cmbYearSelection.Items.IndexOf(currentYear);

            if (yearIndex != -1)
            {
                cmbYearSelection.SelectedIndex = yearIndex;
            }

            // Disable IntegralHeight and set a fixed dropdown height for the second year selector
            cmbYearSelection2.IntegralHeight = false;
            cmbYearSelection2.DropDownHeight = 100; // Adjust this value as needed

            for (int year = startYear; year <= endYear; year++)
            {
                cmbYearSelection2.Items.Add(year);
            }

            if (yearIndex != -1)
            {
                cmbYearSelection2.SelectedIndex = yearIndex;
            }

            // Populate the ComboBox with months from January to December
            string[] months = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            cmbMonthSelection.Items.AddRange(months);
            cmbMonthSelection2.Items.AddRange(months);

            // Set the current month as the default selected month
            int currentMonth = DateTime.Now.Month - 1; // Zero-based index for months
            cmbMonthSelection.SelectedIndex = currentMonth;
            cmbMonthSelection2.SelectedIndex = currentMonth;

            // Populate cmbNameSelection with employee names
            PopulateEmployeeNames();
        }

        private void PopulateEmployeeNames()
        {
            string connectionString = "server=localhost;username=root;password=;database=labasan_dtr_system";
            
            // Clear existing items to avoid duplicates
            cmbNameSelection.Items.Clear();


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT empName FROM tbl_emprecord";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Add each name to the ComboBox
                        cmbNameSelection.Items.Add(reader["empName"].ToString());
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading employee names: " + ex.Message);
                }
            }
        }

        private void btnGenerateOne_Click(object sender, EventArgs e)
        {
            var empID = 1001;
            var startDate = new DateTime(2024, 11, 1);
            var endDate = new DateTime(2024, 11, 30);
            DataTable attendanceData = GetAttendanceRecords(empID, startDate, endDate);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Generate_DTR");

                // Column Width
                worksheet.Column(1).Width = 2.33; // Set the width of column A to 2.33
                worksheet.Column(2).Width = 3.56; // Set the width of column B to 3.56
                worksheet.Column(3).Width = 8; // Set the width of column C to 8
                worksheet.Column(4).Width = 8; // Set the width of column D to 8
                worksheet.Column(5).Width = 8; // Set the width of column E to 8
                worksheet.Column(6).Width = 8; // Set the width of column F to 8
                worksheet.Column(7).Width = 8; // Set the width of column G to 8
                worksheet.Column(8).Width = 8; // Set the width of column H to 8

                // Set up Header and Title formatting
                worksheet.Cell("A1").Value = "Civil Service Form No. 48";
                worksheet.Cell("F1").Value = ">>>>OFFICE'S COPY";
                worksheet.Cell("A2").Value = "DAILY TIME RECORD";
                worksheet.Cell("A3").Value = "Cloi Solsnitzhin D. Castro"; // to change dynamically
                worksheet.Cell("A4").Value = "(Name)";

                worksheet.Range("A1:D1").Merge();
                worksheet.Range("A2:H2").Merge();
                worksheet.Range("A3:H3").Merge();
                worksheet.Range("A4:H4").Merge();
                worksheet.Range("A2:H2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A3:H3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A4:H4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell("A3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("A5").Value = "For the month of";
                worksheet.Cell("F5").Value = "March 2023"; // to change dynamically
                worksheet.Cell("A6").Value = "Official hours for arrival";
                worksheet.Cell("F6").Value = "Regular days 7:30-4:30";
                worksheet.Cell("A7").Value = "and departure";
                worksheet.Cell("F7").Value = "(Saturdays ___)";

                worksheet.Range("A5:E5").Merge();
                worksheet.Range("A6:E6").Merge();
                worksheet.Range("A7:E7").Merge();
                worksheet.Range("A5:E7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A1:H1").Style.Font.Bold = true;
                worksheet.Cell("A2").Style.Font.FontSize = 14;
                worksheet.Cell("F1").Style.Font.FontSize = 10;
                worksheet.Cell("A2").Style.Font.Bold = true;

                // Set up Column Headers with merged cells
                worksheet.Cell("A8").Value = "Day";
                worksheet.Cell("C8").Value = "AM";
                worksheet.Cell("E8").Value = "PM";
                worksheet.Cell("G8").Value = "UNDER TIME";

                worksheet.Cell("A9").Value = "Date";
                worksheet.Cell("C9").Value = "Arrival";
                worksheet.Cell("D9").Value = "Departure";
                worksheet.Cell("E9").Value = "Arrival";
                worksheet.Cell("F9").Value = "Departure";
                worksheet.Cell("G9").Value = "Hours";
                worksheet.Cell("H9").Value = "Minutes";

                worksheet.Range("A8:B8").Merge();
                worksheet.Range("A9:B9").Merge();
                worksheet.Range("C8:D8").Merge();
                worksheet.Range("E8:F8").Merge();
                worksheet.Range("G8:H8").Merge();
                worksheet.Range("A8:H9").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A8:H9").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A8:H9").Style.Border.OutsideBorderColor = XLColor.Green;
                worksheet.Range("A8:H9").Style.Border.InsideBorderColor = XLColor.Green;
                worksheet.Range("A8:H9").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A8:H9").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                // Set up rows for each day with numbering and sample data
                int row = 10;
                for (int day = 1; day <= 31; day++)
                {
                    worksheet.Cell(row, 1).Value = day;  // Day number
                    worksheet.Cell(row, 2).Value = "";   // Corresponding day name Ex. MON, TUR, WED, THU, FRI 
                    worksheet.Cell(row, 3).Value = attendanceData.Rows.Count > day - 1 ? Convert.ToString(attendanceData.Rows[day - 1]["time"]) : ""; // Convert to string or appropriate type AM Arrival
                    worksheet.Cell(row, 4).Value = "PM Departure"; // PM Departure
                    worksheet.Cell(row, 5).Value = "PM Arrival"; // PM Arrival
                    worksheet.Cell(row, 6).Value = "PM Departure"; // PM Departure
                    worksheet.Cell(row, 7).Value = "Under Time Hours"; // Under Time Hours
                    worksheet.Cell(row, 8).Value = "Under Time Minutes"; // Under Time Minutes

                    worksheet.Range(row, 1, row, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(row, 1, row, 8).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(row, 1, row, 8).Style.Border.InsideBorderColor = XLColor.Green;
                    worksheet.Range(row, 1, row, 8).Style.Border.OutsideBorderColor = XLColor.Green;
                    worksheet.Range(row, 1, row, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    row++;
                }

                // Footer Text
                worksheet.Cell(row + 0, 1).Value = "I certify on my honor that the above is a true and correct";
                worksheet.Cell(row + 1, 1).Value = "record of the hours of work performed, record of which was";
                worksheet.Cell(row + 2, 1).Value = "made daily at the time of arrival and departure from office.";

                worksheet.Range("A41:H41").Merge();
                worksheet.Range("A42:H42").Merge();
                worksheet.Range("A43:H43").Merge();

                worksheet.Cell(row + 4, 1).Value = "Signature";
                worksheet.Cell(row + 5, 1).Value = "Verified as to the prescribed office hours";
                worksheet.Cell(row + 7, 1).Value = "In Charge";

                worksheet.Range("A44:H44").Merge();
                worksheet.Range("A45:H45").Merge();
                worksheet.Range("A46:H46").Merge();
                worksheet.Range("A47:H47").Merge();
                worksheet.Range("A48:H48").Merge();

                worksheet.Range("A" + (row + 0) + ":H" + (row + 4)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A" + (row + 4) + ":H" + (row + 4)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A" + (row + 5) + ":H" + (row + 5)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A" + (row + 7) + ":H" + (row + 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Save the workbook to a file
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DTR.xlsx");
                workbook.SaveAs(path);
                MessageBox.Show("DTR file saved to " + path);
            }
        }


        private DataTable GetAttendanceRecords(int empID, DateTime startDate, DateTime endDate)
        {
            string connString = "server=localhost;username=root;password=;database=labasan_dtr_system";
            DataTable attendanceData = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT * FROM tbl_attendance_record WHERE empID = @empID AND date BETWEEN @startDate AND @endDate ORDER BY date, time";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@empID", empID);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(attendanceData);
                    }
                }
            }
            return attendanceData;
        }
    }
}
