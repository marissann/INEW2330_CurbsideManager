using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmailValidation;

namespace SMB3_Curbside_Manager
{
    public partial class frmSignUp : Form
    {
        public frmSignUp()
        {
            InitializeComponent();
        }

        private void frmSignUp_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            /*
            //bool to confirm if all fields are correctly input
            bool allValid = true;

            //Firstname = 20, Lastname = 20, Phone = 20, Email = 50, User = 20, Password = 20
            //Validation for First Name
            if (tbxFirstName.Text.Trim() == String.Empty)
            {
                MessageBox.Show("First Name is empty. Please fill out all fields before continuing.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                allValid = false;
            }

            if(tbxFirstName.Text.Trim().Length > 20)
            {
                MessageBox.Show("First Name exceeds character limit of 20.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                allValid = false;
            }

            //Validation for Last Name
            if (tbxLastName.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Last Name is empty. Please fill out all fields before continuing.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                allValid = false;
            }

            if(tbxLastName.Text.Trim().Length > 20)
            {
                MessageBox.Show("Last name exceeds character limit of 20.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                allValid = false;
            }

            //Validation for Phone Number
            if (tbxPhone.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Phone number is empty. Please fill out all fields before continuing.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                allValid = false;
            }

            //Validation for Email
            if (tbxEmail.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Email is empty. Please fill out all fields before continuing.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                allValid = false;
            }

            if(tbxEmail.Text.Trim().Length > 50)
            {
                MessageBox.Show("Email entered exceeds the character limit of 50.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                allValid = false;
            }

            if(!EmailValidator.Validate(tbxEmail.Text.Trim(), true, true))
            {
                MessageBox.Show("Email address could not be found. Please make sure you entered a correct email address.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                allValid = false;
            }

            //Validation for Username
            if (tbxUsername.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Username is empty. Please fill out all fields before continuing.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                allValid = false;
            }

            if(tbxUsername.Text.Trim().Length > 20)
            {
                MessageBox.Show("Username exceeds character limit of 20.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                allValid = false;
            }

            //Validation for Password
            if (tbxPassword.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Password is empty. Please fill out all fields before continuing.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                allValid = false;
            }

            if(tbxPassword.Text.Trim().Length > 20)
            {
                MessageBox.Show("Username exceeds character limit of 20.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                allValid = false;
            }

            if (tbxPassword.Text.Trim().Length < 8)
            {
                MessageBox.Show("Password is too short.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                allValid = false;
            }

            //bool to tell if password has a number
            bool hasNumber = false;

            for (int i = 0; i < tbxPassword.Text.Trim().Length; i++)
            {
                if (tbxPassword.Text.Trim()[i] >= 48 && tbxPassword.Text.Trim()[i] <= 57)
                {
                    hasNumber = true;
                    break;
                }
            }

            if (!hasNumber)
                allValid = false;

            //int to track the number of symbols found in password
            int symbolCtr = 0;

            for (int i = 0; i < tbxPassword.Text.Trim().Length; i++)
            {

            }

            //If the entered data is all valid, attempt account creation
            if (allValid)
                ProgOps.InsertToDatabase(tbxFirstName, tbxLastName, tbxPhone, tbxEmail, tbxUsername, tbxPassword);
            */

            ProgOps.InsertToDatabase(tbxFirstName, tbxLastName, tbxPhone, tbxEmail, tbxUsername, tbxPassword);
        }

        private void frmSignUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Close this form's connection to the database
            ProgOps.CloseDatabase();
        }
    }
}

//wrap all of this in a try/catch because we don't know what will work //logic marissa pasted in here
//try
//{
//    int upperCase = 0;
//    int lowerCase = 0;
//    int number = 0;
//    int symbol = 0;
//    int length = 0;

//    //ask for userInput
//    System.out.println("Create a password: ");

//    //store userInput
//    String password = input.next();

//    //test for the criteria it must meet (username is all letters and one space)
//    if (password.length() < 6)
//    {

//        throw new BadInputException(" Password must be at least six characters.");
//    }
//    for (int i = 0; i < password.length(); i++)
//    {
//        if (Character.isUpperCase(password.charAt(i)))
//        {
//            upperCase++;
//        }
//        if (upperCase == 0)
//        {
//            throw new BadInputException("Your password must contain one uppercase letter.");
//        }
//    }


//}
//catch (BadInputException msg)
//{
//    System.out.println(msg.toString());

//    //shows the error, and then restarts the loop
//    continue;
//}