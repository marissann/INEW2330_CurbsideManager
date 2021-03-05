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

namespace MailKitTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //Attempt from 3/5/2021

            /* Notes:
             * I followed a tutorial from this site: https://blog.elmah.io/how-to-send-emails-from-csharp-net-the-definitive-tutorial/
             * This covered how to send an email on the old System.Net.Mail library,
             * and then rewrote that code to work for MailKit
             * 
             * I'm not 100% about the how-tos of all parts here, 
             * so these comments are essentially my educated guess on what's happening.
             */

            //Create a new MimeMessage (MimeKit is a component of MailKit)
            var mailMessage = new MimeKit.MimeMessage();

            //Set the address the mail will come from
            mailMessage.From.Add(new MimeKit.MailboxAddress("SMB3 Curbside Manger", "smb3curbsidemanager@gmail.com"));

            //Set the address to send to, in this case my personal gmail.
            //When testing, please replace the name and address with your own. Thank you ^.^
            mailMessage.To.Add(new MimeKit.MailboxAddress("Scott", "thetroggysiege@gmail.com"));

            //Set the message's Subject
            mailMessage.Subject = "You did it, you hot stud!";

            //Create the body, or the actual message part, of the email
            mailMessage.Body = new MimeKit.TextPart("plain")
            {
                Text = "I did it! I sent an email through Visual Studio! Let's gooooooooooooooooo!"
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
        }
    }
}
