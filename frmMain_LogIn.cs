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
    public partial class frmMain_LogIn : Form
    {
        public frmMain_LogIn()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Check if the connection has not been established yet
            if (!ProgOps.isConnected)
            {
                //Connect to the database
                ProgOps.OpenDatabase();

                //Tell ProgOps that the app is connected
                ProgOps.isConnected = true;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Disconnect to the database
            ProgOps.CloseDatabase();
        }
        private void lblRecovery_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Open the Login Recovery form
            frmPasswordRecovery recovery = new frmPasswordRecovery();
            recovery.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Check the database to see if the entered username and password match a customer or employee
            switch (ProgOps.LoginCommand(tbxUsername, tbxPassword))
            {
                case 'A':
                    //Update the user info
                    ProgOps.SetUserInfo(tbxUsername, tbxPassword, false);

                    Console.WriteLine(ProgOps.userID);
                    Console.WriteLine(ProgOps.userFirstName);

                    //Open the admin menu
                    frmAdmin_MainMenu adminMenu = new frmAdmin_MainMenu();
                    adminMenu.ShowDialog();
                    break;
                case 'E':
                    //Update the user info
                    ProgOps.SetUserInfo(tbxUsername, tbxPassword, false);

                    Console.WriteLine(ProgOps.userID);
                    Console.WriteLine(ProgOps.userFirstName);

                    //Open the employee menu
                    frmEmployee_MainMenu employeeMain = new frmEmployee_MainMenu();
                    employeeMain.ShowDialog();
                    break;
                case 'C':
                    //Update the user info
                    ProgOps.SetUserInfo(tbxUsername, tbxPassword, true);

                    Console.WriteLine(ProgOps.userID);
                    Console.WriteLine(ProgOps.userFirstName);

                    //Open the customer menu
                    frmCustomer_MainMenu frmCustomerMain = new frmCustomer_MainMenu();
                    frmCustomerMain.ShowDialog();
                    break;
                case 'F':
                    MessageBox.Show("Incorrect Username and/or Password.", "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show("A database error has occured.", "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            //ProgOps.CloseDatabase();

            //Open the Sign Up form
            frmSignUp s = new frmSignUp();
            s.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbxUsername.Text = "GreenStach";
            tbxPassword.Text = "Yahooo2@";
        }

        private void frmCustomer_Click(object sender, EventArgs e)
        {
            tbxUsername.Text = "Noax42";
            tbxPassword.Text = "Wing90%!";
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            tbxUsername.Text = "JumpMan01";
            tbxPassword.Text = "Wahooo1!";
        }
    }
}