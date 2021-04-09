using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MailKit;

namespace SMB3_Curbside_Manager
{
    public partial class frmPasswordRecovery : Form
    {
        public frmPasswordRecovery()
        {
            InitializeComponent();
        }

        private void btnRecovery_Click(object sender, EventArgs e)
        {
            //Check if the textboxes are not empty
            if(tbxFirstName.Text != "" && tbxEmail.Text != "")
            {
                //Create a string array to hold the recovered information
                string[] recoveredInfo = ProgOps.RecoverLoginQuery(tbxFirstName, tbxEmail);

                //Make sure there was no error
                if (recoveredInfo[0] != "error")
                {
                    //Create a new MimeMessage (MimeKit is a component of MailKit)
                    var mailMessage = new MimeKit.MimeMessage();

                    //Set the address the mail will come from
                    mailMessage.From.Add(new MimeKit.MailboxAddress("SMB3 Curbside Manager", "smb3curbsidemanager@gmail.com"));

                    //Set the address to send to, in this case my personal gmail.
                    mailMessage.To.Add(new MimeKit.MailboxAddress(tbxFirstName.Text, tbxEmail.Text));

                    //Set the message's Subject
                    mailMessage.Subject = "Login Recovery for SMB3 Curbside Manager";

                    //Create the body, or the actual message part, of the email
                    mailMessage.Body = new MimeKit.TextPart("plain")
                    {
                        Text = "Hello " + tbxFirstName.Text +
                               ",\n\nYou are recieving this email because you submitted a login recovery request." +
                               "\nYour login information is as follows:" +
                               "\nUsername: " + recoveredInfo[0] +
                               "\nPassword: " + recoveredInfo[1] +
                               "\n\nPlease do not share login information with anyone else." +
                               "\n\n- Team SMB3"
                    };

                    //Create a smtp client object
                    using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
                    {
                        //Tutorial said to use port 587, but got an error telling to use port 465 for plain body messages
                        //Connect the client to gmail using port 465 with SSL enabled
                        smtpClient.Connect("smtp.gmail.com", 465, true);

                        //Authenticate the client using our gmail's email and password
                        smtpClient.Authenticate("smb3curbsidemanager@gmail.com", "smb33815294");

                        //Send the message we created
                        smtpClient.Send(mailMessage);

                        //Disconnect from the client
                        smtpClient.Disconnect(true);
                    }

                    //Confirm the send
                    MessageBox.Show("Your information has been sent to " + tbxEmail.Text, "Recovery Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please make sure to fill out all fields", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmPasswordRecovery_Load(object sender, EventArgs e)
        {

        }
    }
}
