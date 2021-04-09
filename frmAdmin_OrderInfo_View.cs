using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SMB3_Curbside_Manager
{
    public partial class frmAdmin_OrderInfo_View : Form
    {
        /*private const string CS = @"Server=cstnt.tstc.edu;" +
                                                "Database=inew2330sp21;" +
                                                "User Id=group3sp212330;" +
                                                "Password=3815294;";
        SqlConnection con;
        SqlDataAdapter adapter;
        DataTable dt;
        bool call = true;
        */

        //Set up employee CurrencyManager
        CurrencyManager orderManager;

        //Future variables for state and bookmark
        string myState = "";
        int myBookmark = 0;

        public frmAdmin_OrderInfo_View()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAdmin_OrderInfo_View_Load(object sender, EventArgs e)
        {          
            //Load in the orders table
            ProgOps.FetchOrders(tbxOrderID, tbxCustomerID, tbxOrderDate, tbxTotalPrice, tbxEmployeeID, true);

            //fill the currency manager
            orderManager = (CurrencyManager)this.BindingContext[ProgOps.GetOrdersTable];
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //call = false;
            //ProgOps.DisplayOrderInfo(tbxOrderID, dgvOrderInfo, call);
        }

        private void lblCategoryIDText_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //Move forward one entry
            if (orderManager.Position != orderManager.Count - 1)
                orderManager.Position++;
        }

        private void btnNewest_Click(object sender, EventArgs e)
        {
            //Nove to the last entry
            orderManager.Position = orderManager.Count - 1;
        }

        private void btnOldest_Click(object sender, EventArgs e)
        {
            //Move to the first entry
            orderManager.Position = 0;
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            //Move back one entry
            if (orderManager.Position != 0)
                orderManager.Position--;
        }
        private void tbxOrderID_TabStopChanged(object sender, EventArgs e)
        {

        }

        private void tbxOrderID_TextChanged(object sender, EventArgs e)
        {
            //Fill the data grid view upon a new orderID value
            if(tbxOrderID.Text != "")
            {
                ProgOps.DisplayOrderInfo(tbxOrderID, dgvOrderInfo, false);
            }
        }
    }
}
