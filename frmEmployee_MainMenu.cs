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
    public partial class frmEmployee_MainMenu : Form
    {
        public frmEmployee_MainMenu()
        {
            InitializeComponent();
        }

        private void btnMerchandiseView_Click(object sender, EventArgs e)
        {
            frmEmployee_Merchandise_View merch = new frmEmployee_Merchandise_View();
            merch.ShowDialog();
        }

        private void btnOrderView_Click(object sender, EventArgs e)
        {
            frmEmployee_Order_View order = new frmEmployee_Order_View();
            order.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Close the form
            this.Close();
        }

        private void lblUserId_Click(object sender, EventArgs e)
        {
            
        }

        private void frmEmployee_MainMenu_Load(object sender, EventArgs e)
        {
            lblUserId.Text = ProgOps.userFirstName.ToString();
        }
    }
}
