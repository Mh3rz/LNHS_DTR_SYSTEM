using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LNHS_DTR_SYSTEM
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

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
    }
}
