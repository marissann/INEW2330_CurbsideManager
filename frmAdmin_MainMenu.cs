using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMB3_Curbside_Manager
{
    public partial class frmAdmin_MainMenu : Form
    {
        public frmAdmin_MainMenu()
        {
            InitializeComponent();
        }

        private void frmAdminMainMenu_Load(object sender, EventArgs e)
        {
            
        }

        private void btnEmployeeView_Click(object sender, EventArgs e)
        {
            //Open the EmployeeView form
            frmAdmin_Employee_View ev = new frmAdmin_Employee_View();
            ev.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMerchandiseView_Click(object sender, EventArgs e)
        {
            frmAdmin_Merchandise_View mi = new frmAdmin_Merchandise_View();
            mi.Show();
        }

        private void btnOrderView_Click(object sender, EventArgs e)
        {
            frmAdmin_OrderInfo_View frmOrderInfoView = new frmAdmin_OrderInfo_View();
            frmOrderInfoView.ShowDialog();
        }
    }
}
