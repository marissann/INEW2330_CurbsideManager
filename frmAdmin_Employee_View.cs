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
    public partial class frmAdmin_Employee_View : Form
    {
        public frmAdmin_Employee_View()
        {
            InitializeComponent();
        }

        //Set up employee CurrencyManager
        CurrencyManager employeeManager;

        //Future variables for state and bookmark
        string myState = "";
        int myBookmark = 0;

        private void frmAdmin_Employee_View_Load(object sender, EventArgs e)
        {
            //Load in the Employees table from the database
            ProgOps.FetchEmployees(tbxEmployeeID, tbxUserName, tbxPassword, tbxFirstName, tbxLastName, tbxHireDate, tbxIsAdmin);

            //Fill the currency manager
            employeeManager = (CurrencyManager)this.BindingContext[ProgOps.GetEmployeeTable];

            SetState("View");
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //Move forward one entry
            if (employeeManager.Position != employeeManager.Count - 1)
                employeeManager.Position++;
        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            //Move back one entry
            if  (employeeManager.Position != 0)
                employeeManager.Position--;
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            //Move to the first entry
            employeeManager.Position = 0;
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            //Move to the last entry
            employeeManager.Position = employeeManager.Count - 1;
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                myBookmark = employeeManager.Position;
                employeeManager.AddNew();
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
            string savedName = tbxFirstName.Text;
            int savedRow;

            //Have the currency manager end the edit and re-sort the table
            employeeManager.EndCurrentEdit();
            ProgOps.GetEmployeeTable.DefaultView.Sort = "FirstName";

            savedRow = ProgOps.GetEmployeeTable.DefaultView.Find(savedName);

            employeeManager.Position = savedRow;

            //Return to view state
            SetState("View");

            //Display confirming message box
            MessageBox.Show("Record saved successfully.", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Cancel the edit
            employeeManager.CancelCurrentEdit();

            //If cancelling from adding an entry, return to saved position
            if (myState == "Add")
                employeeManager.Position = myBookmark;

            //Return to view mode
            SetState("View");
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Set the form into "Delete" mode
            //SetState("Delete");
            //Set up a dialog response to confirm deleting a record
            DialogResult confirmDelete = MessageBox.Show("Are you sure you want to delete this record?", "Delete Record",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //Delete the entry if the user clicked yes
            if (confirmDelete == DialogResult.Yes)
            {
                employeeManager.RemoveAt(employeeManager.Position);
            }
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
                    tbxEmployeeID.ReadOnly = true;
                    tbxUserName.ReadOnly = true;
                    tbxPassword.ReadOnly = true;
                    tbxFirstName.ReadOnly = true;
                    tbxLastName.ReadOnly = true;
                    tbxHireDate.ReadOnly = true;
                    tbxIsAdmin.ReadOnly = true;
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
                    tbxEmployeeID.ReadOnly = false;
                    tbxUserName.ReadOnly = false;
                    tbxPassword.ReadOnly = false;
                    tbxFirstName.ReadOnly = false;
                    tbxLastName.ReadOnly = false;
                    tbxHireDate.ReadOnly = false;
                    tbxIsAdmin.ReadOnly = false;
                    break;
            }

        }
        private void frmAdmin_Employee_View_FormClosing(object sender, FormClosingEventArgs e)
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
                ProgOps.UpdateEmployeeOnClose();
                ProgOps.DisposeEmployeeObjects();
            }
        }
    }
}
