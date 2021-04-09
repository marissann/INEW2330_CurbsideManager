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
    public partial class frmCustomer_MainMenu : Form
    {
        public frmCustomer_MainMenu()
        {
            InitializeComponent();
        }

        private void btnOrderCreate_Click(object sender, EventArgs e)
        {
            frmCustomer_OrderCreate frmOrderCreate = new frmCustomer_OrderCreate();
            frmOrderCreate.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMerchandiseView_Click(object sender, EventArgs e)
        {
            frmCustomer_Merchandise_View frmMerchandiseView = new frmCustomer_Merchandise_View();
            frmMerchandiseView.ShowDialog();

        }
    }
}
