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
    public partial class frmCustomer_Merchandise_View : Form
    {
        public frmCustomer_Merchandise_View()
        {
            InitializeComponent();
        }

        private void frmCustomer_Merchandise_View_Load(object sender, EventArgs e)
        {
            ProgOps.DisplayMerchadise(tbxMerchandiseID, cmbCategory ,dgvMerchandise, 0);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (tbxMerchandiseID.Text == "" && cmbCategory.Text == "")
            {
                ProgOps.DisplayMerchadise(tbxMerchandiseID, cmbCategory, dgvMerchandise, 0);
            }
            else if (tbxMerchandiseID.Text != "" && cmbCategory.Text == "")
            {
                ProgOps.DisplayMerchadise(tbxMerchandiseID, cmbCategory, dgvMerchandise, 1);
            }
            else if (tbxMerchandiseID.Text != "" && cmbCategory.Text != "")
            {
                ProgOps.DisplayMerchadise(tbxMerchandiseID, cmbCategory, dgvMerchandise, 2);
            }
            else if (tbxMerchandiseID.Text == "" && cmbCategory.Text != "")
            {
                ProgOps.DisplayMerchadise(tbxMerchandiseID, cmbCategory, dgvMerchandise, 3);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
