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
    public partial class frmMain : Form
    {
        public frmMain()
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
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Check the database to see if the entered username and password match a customer or employee
            ProgOps.LoginCommand(tbxUsername, tbxPassword);
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            //Open the Sign Up form
        }
    }
}
