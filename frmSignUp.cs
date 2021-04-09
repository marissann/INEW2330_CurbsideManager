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
            //Trim each non-masked textbox
            tbxFirstName.Text = tbxFirstName.Text.Trim();
            tbxLastName.Text = tbxLastName.Text.Trim();
            tbxEmail.Text = tbxEmail.Text.Trim();
            tbxUsername.Text = tbxUsername.Text.Trim();
            tbxPassword.Text = tbxPassword.Text.Trim();

            //bool to confirm if all fields are correctly input
            bool allValid = true;

            //Firstname = 20, Lastname = 20, Phone = 20, Email = 50, User = 20, Password = 20
            //Validation for First Name

            if (tbxFirstName.Text == String.Empty || tbxFirstName.Text.Length > 20)
            {
                lblFirstNameError.Visible = true;
                allValid = false;
            }
            else
            {
                lblFirstNameError.Visible = false;
            }

            //Validation for Last Name
            if (tbxLastName.Text == String.Empty || tbxLastName.Text.Length > 20)
            {
                lblLastNameError.Visible = true;
                allValid = false;
            }
            else
            {
                lblLastNameError.Visible = false;
            }

            //Validation for Phone Number
            if (tbxPhone.Text == String.Empty)
            {
                //MessageBox.Show("Phone number is empty. Please fill out all fields before continuing.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblPhoneError.Visible = true;
                allValid = false;
            }
            else
            {
                lblPhoneError.Visible = false;
            }
     
            //Validation for Email
            if (tbxEmail.Text == String.Empty || tbxEmail.Text.Length > 50 || !EmailValidator.Validate(tbxEmail.Text))
            {
                lblEmailError.Visible = true;
                allValid = false;
            }
            else
            {
                lblEmailError.Visible = false;
            }   

            //Validation for Username
            if (tbxUsername.Text == String.Empty || (tbxUsername.Text.Length > 20))
            {
                lblUsernameError.Visible = true;

                allValid = false;
            }
            else
            {
                lblUsernameError.Visible = false;
            }
            
            //bool to tell if password has a number
            bool hasNumber = false;

            for (int i = 0; i < tbxPassword.Text.Length; i++)
            {
                if (Char.IsDigit(tbxPassword.Text[i]))
                {
                    hasNumber = true;
                    break;
                }
            }

            //int to track the number of symbols found in password
            int symbolCtr = 0;

            for (int i = 0; i < tbxPassword.Text.Length; i++)
            {
                //Check each character to see if it's a symbol
                //Note: Char.IsSymbol() did not actually work here
                if (!Char.IsLetter(tbxPassword.Text[i]) &&
                    !Char.IsDigit(tbxPassword.Text[i]))
                {
                    symbolCtr++;
                }
            }

            //Validation for Password
            if (tbxPassword.Text == String.Empty || tbxPassword.Text.Length > 20 || tbxPassword.Text.Length < 8 || !hasNumber || symbolCtr < 2)
            {
                lblPasswordError.Visible = true;
                allValid = false;
                Console.WriteLine(symbolCtr);
                Console.WriteLine(hasNumber);
            }
            else
            {
                lblPasswordError.Visible = false;
                
                //Console.WriteLine(symbolCtr);
                //Console.WriteLine(hasNumber);
            }
            

            //If the entered data is all valid, attempt account creation
            if (allValid == true)
            {
                ProgOps.InsertToDatabase(tbxFirstName, tbxLastName, tbxPhone, tbxEmail, tbxUsername, tbxPassword);
                MessageBox.Show("Account Successfully Created : Welcome " + tbxFirstName.Text + " " + tbxLastName.Text, "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                this.Close();

            }
            else
            {
                MessageBox.Show("Account Error. Scroll over the error labels to review your account credentials.","Account Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 32)
            {
                //Prevent spaces from being entered into the password textbox
                e.Handled = true;
            }
        }
    }
}
