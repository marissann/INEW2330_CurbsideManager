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
    public partial class frmAdmin_Merchandise_View : Form
    {
        public frmAdmin_Merchandise_View()
        {
            InitializeComponent();
        }

        //Set up merchandise CurrencyManager
        CurrencyManager merchManager;

        //Future variables for state and bookmark
        string myState = "";
        int myBookmark = 0;

        private void frmMerchandiseInfo_Load(object sender, EventArgs e)
        {
            try
            {
                //Load in the Products table from the database
                ProgOps.FetchMerchandise(tbxProductID, tbxCategoryID, tbxProductName, tbxQuantity, tbxPrice, tbxInStock);

                //Fill the currency manager
                merchManager = (CurrencyManager)this.BindingContext[ProgOps.GetMerchandiseTable];

                //Start in view state
                SetState("View");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error : Products Table ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            //Move back one entry if currency manager is not at the first
            if (merchManager.Position != 0)
                merchManager.Position--;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            //Move forward one entry if currency manager is not at the last
            if (merchManager.Position != merchManager.Count - 1)
                merchManager.Position++;
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            //Move to the first entry
            merchManager.Position = 0;
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            //Move to the last entry
            merchManager.Position = merchManager.Count - 1;
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try 
            {
                myBookmark = merchManager.Position;
                merchManager.AddNew();
                SetState("Add");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error : In Add New ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //Set the form into "Edit" mode
            SetState("Edit");
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            /*Post-alpha Statement
             *Statement that would check if each textbox has valid data
             * if(!ValidationFunction([parameters])
             *      return;
            */

            //variables to hold save data
            string savedName = tbxProductName.Text;
            int savedRow;

            //Have the currency manager end the edit and re-sort the table
            merchManager.EndCurrentEdit();
            ProgOps.GetMerchandiseTable.DefaultView.Sort = "ProductName";

            savedRow = ProgOps.GetMerchandiseTable.DefaultView.Find(savedName);

            merchManager.Position = savedRow;

            //Return to view state
            SetState("View");

            //Display confirming message box
            MessageBox.Show("Record saved successfully.", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Cancel the edit
            merchManager.CancelCurrentEdit();

            //If cancelling from adding an entry, return to saved position
            if(myState == "Add")
                merchManager.Position = myBookmark;

            //Return to view mode
            SetState("View");
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Set up a dialog response to confirm deleting a record
            DialogResult confirmDelete = MessageBox.Show("Are you sure you want to delete this record?", "Delete Record",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //Delete the entry if the user clicked yes
            if (confirmDelete == DialogResult.Yes)
                merchManager.RemoveAt(merchManager.Position);
        }

        private void btnClose_Click(object sender, EventArgs e)
            {
                //Close the form
                this.Close();
            }

        private void SetState(string state)
        {
        myState = state;

            switch (state)
            {
            case "View":
                //Buttons
                btnPrevious.Enabled = true;
                btnNext.Enabled = true;
                btnLast.Enabled = true;
                btnFirst.Enabled = true;
                btnAddNew.Enabled = true;
                btnEdit.Enabled = true;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnDelete.Enabled = true;
                btnClose.Enabled = true;
            //textbox
                //tbxProductID.ReadOnly = true;
                tbxCategoryID.ReadOnly = true;
                tbxProductName.ReadOnly = true;
                tbxQuantity.ReadOnly = true;
                tbxPrice.ReadOnly = true;
            tbxInStock.ReadOnly = true;
                break;
            default:
                //Buttons
                btnPrevious.Enabled = false;
                btnNext.Enabled = false;
                btnLast.Enabled = false;
                btnFirst.Enabled = false;
                btnAddNew.Enabled = false;
                btnEdit.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                btnDelete.Enabled = false;
                btnClose.Enabled = false;
                //textbox
                //tbxProductID.ReadOnly = false;
                tbxCategoryID.ReadOnly = false;
                tbxProductName.ReadOnly = false;
                tbxQuantity.ReadOnly = false;
                tbxPrice.ReadOnly = false;
                    tbxInStock.ReadOnly = false;
                break;
            }
        }

        private void frmMerchandiseInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Check if the user is not in view mode
            if(myState != "View")
            {
                //Display notification and cancel close
                MessageBox.Show("You must finish the current edit before closing the application.", "Finish Edit",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {
                //Update database
                ProgOps.UpdateMerchOnClose();
                ProgOps.DisposeMerchObjects();
            }
        }
    }
    }  


    
