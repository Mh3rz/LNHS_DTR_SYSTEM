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
using System.Data.SqlClient;

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
            // Dynamically get selected employee ID and name from cmbNameSelection
            if (cmbNameSelection.SelectedItem == null || cmbMonthSelection.SelectedItem == null || cmbYearSelection.SelectedItem == null)
            {
                MessageBox.Show("Please select an employee, month, and year.");
                return;
            }

            int empID = GetEmployeeID(cmbNameSelection.SelectedItem.ToString());
            string empName = cmbNameSelection.SelectedItem.ToString();
            int selectedMonth = cmbMonthSelection.SelectedIndex + 1; // Zero-based month index
            int selectedYear = int.Parse(cmbYearSelection.SelectedItem.ToString());

            // Calculate start and end dates dynamically
            var startDate = new DateTime(selectedYear, selectedMonth, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1); // Last day of the month

            // Get attendance data
            DataTable attendanceData = GetAttendanceRecords(empID, selectedYear, selectedMonth);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Generate_DTR");

                // Column Width
                worksheet.Column(1).Width = 2.33;
                worksheet.Column(2).Width = 3.56;
                worksheet.Column(3).Width = 6;
                worksheet.Column(4).Width = 6;
                worksheet.Column(5).Width = 6;
                worksheet.Column(6).Width = 6;
                worksheet.Column(7).Width = 6;
                worksheet.Column(8).Width = 6;
                worksheet.Range("C9:H9").Style.Font.FontSize = 7;

                // Set up Header and Title formatting
                worksheet.Cell("A1").Value = "Civil Service Form No. 48";
                worksheet.Range("A1:H1").Style.Font.FontSize = 10;
                worksheet.Cell("F1").Value = $"Employee No.: {empID}";
                worksheet.Cell("A2").Value = "DAILY TIME RECORD";
                worksheet.Cell("A3").Value = empName; // Dynamically set employee name
                worksheet.Cell("A4").Value = "(Name)";

                worksheet.Range("A1:D1").Merge();
                worksheet.Range("A2:H2").Merge();
                worksheet.Range("A3:H3").Merge();
                worksheet.Range("A4:H4").Merge();
                worksheet.Range("A2:H2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A3:H3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A4:H4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A4:H4").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A4:H4").Style.Border.TopBorderColor = XLColor.Green;

                worksheet.Cell("A5").Value = "For the month of";
                worksheet.Cell("F5").Value = $"{startDate.ToString("MMMM yyyy")}";
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
                worksheet.Cell("A2").Style.Font.Bold = true;

                // Set up Column Headers
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

                // Set up rows for each day
                int row = 10;
                foreach (DateTime date in EachDay(startDate, endDate))
                {
                    worksheet.Cell(row, 1).Value = date.Day; // Day number
                    worksheet.Cell(row, 2).Value = date.ToString("ddd"); // Day name (e.g., Mon, Tue)

                    // Filter rows for the current date
                    var rowsForDate = attendanceData.AsEnumerable()
                        .Where(r => DateTime.TryParse(r["date"].ToString(), out DateTime dateValue) && dateValue.Date == date.Date)
                        .OrderBy(r => int.TryParse(r["entry_rank"].ToString(), out int entryRank) ? entryRank : 0)
                        .ToList();

                    // Populate the columns based on entry_rank
                    for (int i = 0; i < rowsForDate.Count; i++)
                    {
                        // Safely convert the "entry_rank" text value to its respective integer value
                        string entryRankText = rowsForDate[i]["entry_rank"].ToString().ToLower(); // Ensure case-insensitive comparison
                        int entryRank = 0; // Default value in case of unexpected text

                        // Use a series of if-else statements instead of switch expression
                        if (entryRankText == "first")
                        {
                            entryRank = 1;
                        }
                        else if (entryRankText == "second")
                        {
                            entryRank = 2;
                        }
                        else if (entryRankText == "third")
                        {
                            entryRank = 3;
                        }
                        else if (entryRankText == "fourth")
                        {
                            entryRank = 4;
                        }

                        // Get the time from the attendance record (assumed to be in the "time" column)
                        string timeString = rowsForDate[i]["time"].ToString();
                        DateTime timeValue;

                        // Try parsing the time string
                        if (DateTime.TryParse(timeString, out timeValue))
                        {
                            // Format the time as a 12-hour string with AM/PM
                            string formattedTime = timeValue.ToString("hh:mm");  // 12-hour format with out AM/PM and ("hh:mm tt") if with AM/PM

                            // Map the entryRank value to the correct cell
                            switch (entryRank)
                            {
                                case 1:
                                    worksheet.Cell(row, 3).Value = formattedTime; // AM Arrival
                                    break;
                                case 2:
                                    worksheet.Cell(row, 4).Value = formattedTime; // AM Departure
                                    break;
                                case 3:
                                    worksheet.Cell(row, 5).Value = formattedTime; // PM Arrival
                                    break;
                                case 4:
                                    worksheet.Cell(row, 6).Value = formattedTime; // PM Departure
                                    break;
                                default:
                                    // Handle unexpected or invalid "entry_rank" values
                                    // You can log this issue, or simply skip processing for this row
                                    break;
                            }
                        }
                        else
                        {
                            // Handle the case where the time cannot be parsed (optional)
                            Console.WriteLine($"Invalid time format for entry {i} on {rowsForDate[i]["date"]}");
                        }
                    }

                    worksheet.Cell(row, 7).Value = ""; // Leave empty
                    worksheet.Cell(row, 8).Value = ""; // Leave empty

                    // Apply borders and alignment
                    worksheet.Range(row, 1, row, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(row, 1, row, 8).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(row, 1, row, 8).Style.Border.OutsideBorderColor = XLColor.Green;
                    worksheet.Range(row, 1, row, 8).Style.Border.InsideBorderColor = XLColor.Green;
                    worksheet.Range(row, 1, row, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    row++;
                }

                // Footer Text
                worksheet.Cell(row + 0, 1).Value = "I certify on my honor that the above is a true and correct";
                worksheet.Cell(row + 1, 1).Value = "record of the hours of work performed, record of which was";
                worksheet.Cell(row + 2, 1).Value = "made daily at the time of arrival and departure from office.";

                worksheet.Range("A40:H40").Merge();
                worksheet.Range("A41:H41").Merge();
                worksheet.Range("A42:H42").Merge();
                worksheet.Range("A43:H43").Merge();

                worksheet.Cell(row + 4, 1).Value = "Signature";
                worksheet.Cell(row + 5, 1).Value = "Verified as to the prescribed office hours";
                worksheet.Cell(row + 7, 1).Value = "Melomar A. Retanal";
                worksheet.Cell(row + 8, 1).Value = "In Charge";

                worksheet.Range("A44:H44").Merge();
                worksheet.Range("A45:H45").Merge();
                worksheet.Range("A46:H46").Merge();
                worksheet.Range("A47:H47").Merge();
                worksheet.Range("A48:H48").Merge();

                worksheet.Range("A44:H44").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A48:H48").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A44:H44").Style.Border.TopBorderColor = XLColor.Green;
                worksheet.Range("A48:H48").Style.Border.TopBorderColor = XLColor.Green;


                worksheet.Range("A" + (row + 0) + ":H" + (row + 4)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A" + (row + 4) + ":H" + (row + 4)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A" + (row + 5) + ":H" + (row + 5)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A" + (row + 7) + ":H" + (row + 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A" + (row + 8) + ":H" + (row + 8)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Range("A" + (row + 0) + ":H" + (row + 2)).Style.Font.FontSize = 10;
                


                // Save the workbook
                // Prepare the file name based on the employee name, month, and year
                string fileName = $"{empName.Replace(" ", "_")}_{cmbMonthSelection.SelectedItem.ToString()}_{selectedYear}.xlsx";

                // Define the path where the file will be saved (desktop folder)
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                // Save the workbook to the dynamically generated file name
                workbook.SaveAs(path);
                MessageBox.Show($"DTR file saved to {path}");
            }
        }


        private int GetEmployeeID(string empName)
        {
            // Connection string for the database (update with your actual details)
            string connString = "server=localhost;username=root;password=;database=labasan_dtr_system";

            int empID = 0; // Default to 0 if no matching employee is found

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT empID FROM tbl_emprecord WHERE empName = @empName LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@empName", empName);

                        object result = cmd.ExecuteScalar(); // Get the first column of the first row in the result set

                        if (result != null && int.TryParse(result.ToString(), out int id))
                        {
                            empID = id; // Assign the fetched empID
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving employee ID: {ex.Message}");
                }
            }

            return empID;
        }




        private DataTable GetAttendanceRecords(int empID, int year, int month)
        {
            string connString = "server=localhost;username=root;password=;database=labasan_dtr_system";
            DataTable attendanceData = new DataTable();

            // Dynamically calculate the first and last day of the month
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1); // Last day of the month

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
        private IEnumerable<DateTime> EachDay(DateTime start, DateTime end)
        {
            for (var date = start; date <= end; date = date.AddDays(1))
            {
                yield return date;
            }
        }

        private void btnGenerateAll_Click(object sender, EventArgs e)
        {
            // Ensure the month and year are selected
            if (cmbMonthSelection2.SelectedItem == null || cmbYearSelection2.SelectedItem == null)
            {
                MessageBox.Show("Please select a month and year.");
                return;
            }

            // Get selected month and year
            int selectedMonth = cmbMonthSelection2.SelectedIndex + 1; // Zero-based month index
            int selectedYear = int.Parse(cmbYearSelection2.SelectedItem.ToString());

            // Get the list of employee names
            List<string> employeeNames = GetEmployeeNames();

            // Calculate start and end dates dynamically
            var startDate = new DateTime(selectedYear, selectedMonth, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1); // Last day of the month

            // Create a new workbook for all employees' DTR
            using (var workbook = new XLWorkbook())
            {
                foreach (var empName in employeeNames)
                {
                    // Get employee ID for each employee
                    int empID = GetEmployeeID(empName);

                    // Get attendance data for the employee
                    DataTable attendanceData = GetAttendanceRecords(empID, selectedYear, selectedMonth);

                    // Add a new worksheet for the employee
                    var worksheet = workbook.Worksheets.Add(empName);

                    // Column Width
                    worksheet.Column(1).Width = 2.33;
                    worksheet.Column(2).Width = 3.56;
                    worksheet.Column(3).Width = 6;
                    worksheet.Column(4).Width = 6;
                    worksheet.Column(5).Width = 6;
                    worksheet.Column(6).Width = 6;
                    worksheet.Column(7).Width = 6;
                    worksheet.Column(8).Width = 6;
                    worksheet.Range("C9:H9").Style.Font.FontSize = 7;

                    // Set up Header and Title formatting
                    worksheet.Cell("A1").Value = "Civil Service Form No. 48";
                    worksheet.Range("A1:H1").Style.Font.FontSize = 10;
                    worksheet.Cell("F1").Value = $"Employee No.: {empID}";
                    worksheet.Cell("A2").Value = "DAILY TIME RECORD";
                    worksheet.Cell("A3").Value = empName; // Dynamically set employee name
                    worksheet.Cell("A4").Value = "(Name)";

                    worksheet.Range("A1:D1").Merge();
                    worksheet.Range("A2:H2").Merge();
                    worksheet.Range("A3:H3").Merge();
                    worksheet.Range("A4:H4").Merge();
                    worksheet.Range("A2:H2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("A3:H3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("A4:H4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("A4:H4").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A4:H4").Style.Border.TopBorderColor = XLColor.Green;

                    worksheet.Cell("A5").Value = "For the month of";
                    worksheet.Cell("F5").Value = $"{startDate.ToString("MMMM yyyy")}";
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
                    worksheet.Cell("A2").Style.Font.Bold = true;

                    // Set up Column Headers
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

                    // Set up rows for each day
                    int row = 10;
                    foreach (DateTime date in EachDay(startDate, endDate))
                    {
                        worksheet.Cell(row, 1).Value = date.Day; // Day number
                        worksheet.Cell(row, 2).Value = date.ToString("ddd"); // Day name (e.g., Mon, Tue)

                        // Filter rows for the current date
                        var rowsForDate = attendanceData.AsEnumerable()
                            .Where(r => DateTime.TryParse(r["date"].ToString(), out DateTime dateValue) && dateValue.Date == date.Date)
                            .OrderBy(r => int.TryParse(r["entry_rank"].ToString(), out int entryRank) ? entryRank : 0)
                            .ToList();

                        // Populate the columns based on entry_rank
                        for (int i = 0; i < rowsForDate.Count; i++)
                        {
                            // Safely convert the "entry_rank" text value to its respective integer value
                            string entryRankText = rowsForDate[i]["entry_rank"].ToString().ToLower(); // Ensure case-insensitive comparison
                            int entryRank = 0; // Default value in case of unexpected text

                            // Use a series of if-else statements instead of switch expression
                            if (entryRankText == "first")
                            {
                                entryRank = 1;
                            }
                            else if (entryRankText == "second")
                            {
                                entryRank = 2;
                            }
                            else if (entryRankText == "third")
                            {
                                entryRank = 3;
                            }
                            else if (entryRankText == "fourth")
                            {
                                entryRank = 4;
                            }

                            // Get the time from the attendance record (assumed to be in the "time" column)
                            string timeString = rowsForDate[i]["time"].ToString();
                            DateTime timeValue;

                            // Try parsing the time string
                            if (DateTime.TryParse(timeString, out timeValue))
                            {
                                // Format the time as a 12-hour string with AM/PM
                                string formattedTime = timeValue.ToString("hh:mm");  // 12-hour format with out AM/PM and ("hh:mm tt") if with AM/PM

                                // Map the entryRank value to the correct cell
                                switch (entryRank)
                                {
                                    case 1:
                                        worksheet.Cell(row, 3).Value = formattedTime; // AM Arrival
                                        break;
                                    case 2:
                                        worksheet.Cell(row, 4).Value = formattedTime; // AM Departure
                                        break;
                                    case 3:
                                        worksheet.Cell(row, 5).Value = formattedTime; // PM Arrival
                                        break;
                                    case 4:
                                        worksheet.Cell(row, 6).Value = formattedTime; // PM Departure
                                        break;
                                    default:
                                        // Handle unexpected or invalid "entry_rank" values
                                        // You can log this issue, or simply skip processing for this row
                                        break;
                                }
                            }
                            else
                            {
                                // Handle the case where the time cannot be parsed (optional)
                                Console.WriteLine($"Invalid time format for entry {i} on {rowsForDate[i]["date"]}");
                            }
                        }

                        worksheet.Cell(row, 7).Value = ""; // Leave empty
                        worksheet.Cell(row, 8).Value = ""; // Leave empty

                        // Apply borders and alignment
                        worksheet.Range(row, 1, row, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range(row, 1, row, 8).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range(row, 1, row, 8).Style.Border.OutsideBorderColor = XLColor.Green;
                        worksheet.Range(row, 1, row, 8).Style.Border.InsideBorderColor = XLColor.Green;
                        worksheet.Range(row, 1, row, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        row++;
                    }
                    // Footer Text
                    worksheet.Cell(row + 0, 1).Value = "I certify on my honor that the above is a true and correct";
                    worksheet.Cell(row + 1, 1).Value = "record of the hours of work performed, record of which was";
                    worksheet.Cell(row + 2, 1).Value = "made daily at the time of arrival and departure from office.";

                    worksheet.Range("A40:H40").Merge();
                    worksheet.Range("A41:H41").Merge();
                    worksheet.Range("A42:H42").Merge();
                    worksheet.Range("A43:H43").Merge();

                    worksheet.Cell(row + 4, 1).Value = "Signature";
                    worksheet.Cell(row + 5, 1).Value = "Verified as to the prescribed office hours";
                    worksheet.Cell(row + 7, 1).Value = "Melomar A. Retanal";
                    worksheet.Cell(row + 8, 1).Value = "In Charge";

                    worksheet.Range("A44:H44").Merge();
                    worksheet.Range("A45:H45").Merge();
                    worksheet.Range("A46:H46").Merge();
                    worksheet.Range("A47:H47").Merge();
                    worksheet.Range("A48:H48").Merge();

                    worksheet.Range("A44:H44").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A48:H48").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A44:H44").Style.Border.TopBorderColor = XLColor.Green;
                    worksheet.Range("A48:H48").Style.Border.TopBorderColor = XLColor.Green;

                    worksheet.Range("A" + (row + 0) + ":H" + (row + 4)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("A" + (row + 4) + ":H" + (row + 4)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("A" + (row + 5) + ":H" + (row + 5)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("A" + (row + 7) + ":H" + (row + 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("A" + (row + 8) + ":H" + (row + 8)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    worksheet.Range("A" + (row + 0) + ":H" + (row + 2)).Style.Font.FontSize = 10;
                    worksheet.Rows().AdjustToContents();
                }

                // Save the workbook to the specified path
                string fileName = $"{startDate.ToString("MMMMyyyy")}_DTR_AllEmployees.xlsx";
                string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                workbook.SaveAs(savePath);
                MessageBox.Show($"DTR for all employees has been successfully generated and saved to: {savePath}");
            }
        }
        private List<string> GetEmployeeNames()
        {
            List<string> employeeNames = new List<string>();

            try
            {
                // Establish a connection to the database
                using (MySqlConnection connection = new MySqlConnection("server=localhost;username=root;password=;database=labasan_dtr_system"))
                {
                    connection.Open();

                    // Define the SQL query to fetch employee names
                    string query = "SELECT empName FROM tbl_emprecord"; // Adjust table and column names as necessary

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and retrieve the results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Add each employee's name to the list
                                employeeNames.Add(reader["empName"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching employee names: {ex.Message}");
            }

            return employeeNames;
        }


    }
}
