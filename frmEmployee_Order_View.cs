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
    public partial class frmEmployee_Order_View : Form
    {
        //Set up order CurrencyManager
        CurrencyManager orderManager;

        //String to keep track of state
        string myState;

        public frmEmployee_Order_View()
        {
            InitializeComponent();
        }

        private void frmEmployee_Order_View_Load(object sender, EventArgs e)
        {
            //Load in the orders table
            ProgOps.FetchOrders(tbxOrderID, tbxCustomerID, tbxOrderDate, tbxTotalPrice, tbxEmployeeID, false);

            //fill the currency manager
            orderManager = (CurrencyManager)this.BindingContext[ProgOps.GetOrdersTable];

            //Set state to view
            SetState("View");
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Close the form
            this.Close();
        }

        private void tbxOrderID_TextChanged(object sender, EventArgs e)
        {
            //Fill the data grid view upon a new orderID value
            if (tbxOrderID.Text != "")
            {
                ProgOps.DisplayOrderInfo(tbxOrderID, dgvOrderInfo, false);
            }
        }

        private void btnHandle_Click(object sender, EventArgs e)
        {
            Console.WriteLine(tbxEmployeeID.Text);
            if (tbxEmployeeID.Text == "")
            {
                //Display instruction
                MessageBox.Show("Please enter the EmployeeID of the employee handling this order.", "Order Handling",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Enable editing
                SetState("Edit");
            }
            else
            {
                //Reject handle
                MessageBox.Show("This order has already been handled.", "Order Handling",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Variables to hold the position of the saved row
            int savedOrderID = int.Parse(tbxOrderID.Text);
            int savedRow;

            //Have the currency manager end the edit and re-sort the table
            orderManager.EndCurrentEdit();
            ProgOps.GetOrdersTable.DefaultView.Sort = "OrderID";

            savedRow = ProgOps.GetOrdersTable.DefaultView.Find(savedOrderID);

            orderManager.Position = savedRow;

            //Return to view state
            SetState("View");

            //Display confirming message box
            MessageBox.Show("Record saved successfully.", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Cancel the edit
            orderManager.CancelCurrentEdit();

            //Return to view mode
            SetState("View");
        }

        void SetState(String state)
        {
            //Set the myState
            myState = state;

            switch (state)
            {
                case "Edit":
                    //Turn off all buttons except save and cancel
                    btnOldest.Enabled = false;
                    btnLeft.Enabled = false;
                    btnNext.Enabled = false;
                    btnNewest.Enabled = false;
                    btnHandle.Enabled = false;

                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;

                    //Focus on EmployeeID textbox and turn off Read Only
                    tbxEmployeeID.ReadOnly = false;
                    tbxEmployeeID.Focus();
                    break;
                default:
                    //Turn off all buttons except save and cancel
                    btnOldest.Enabled = true;
                    btnLeft.Enabled = true;
                    btnNext.Enabled = true;
                    btnNewest.Enabled = true;
                    btnHandle.Enabled = true;

                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;

                    //Set EmployeeID textbox back to read only
                    tbxEmployeeID.ReadOnly = true;
                    break;
            }
        }

        private void frmEmployee_Order_View_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Check if the user is not in view mode
            if (myState != "View")
            {
                //Display notification and cancel close
                MessageBox.Show("You must finish the current edit before closing the application.", "Finish Edit",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {
                //Update database
                ProgOps.UpdateOrderOnClose();
                ProgOps.DisposeAllOrderObjects();
            }
        }
    }
}
