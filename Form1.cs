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
        //OAuth Project ID: mailkittest-306622
        //OAuth Client ID: 701434023862-rs304a9flq9chlll4uq1srjgrt2vno0d.apps.googleusercontent.com
        //OAuth Client Secret: rSY-E16PYL-8uYTfCe180hUE

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //Attempt from 3/4/2021 (ignore this unless you want your brain to hurt lol)
            /*
            //Code to get access to gmail api
            const string GMailAccount = "thetroggysiege@gmail.com";

            var clientSecrets = new ClientSecrets
            {
                CliendId = "701434023862-rs304a9flq9chlll4uq1srjgrt2vno0d.apps.googleusercontent.com",
                ClientSecret = "rSY-E16PYL-8uYTfCe180hUE"
            };

            var codeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                DataStore = new FileDataStore("CredentialCacheFolder", false),
                Scopes = new[] { "https://mail.google.com/" },
                ClientSecrets = clientSecrets
            });

            var codeReciever = new LocalServerCodeReceiver();
            var authCode = new AuthorizationCodeInstalledApp (codeFlow, codeReciever);

            var credential = await authCode.AuthorizeAsync(CancellationToken.None);

            var oauth2 = new SaslMechanismOAuth2(credential.UserId, credential.Token.AccessToken);

            using(var client = new ImapClient())
            {
                await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(oauth2);
                await client.DisconnectAsync(true);
            }

            //Code to create and send message
            var message = new MimeKit.MimeMessage();
            message.From.Add(new MimeKit.MailboxAddress("SMB3 Curbside Manager", "smb3curbsidemanager@gmail.com"));
            message.To.Add(new MimeKit.MailboxAddress("Scott", "thetroggysiege@gmail.com"));

            message.Subject = "MailKit Test";
            message.Body = new MimeKit.TextPart("plain")
            {
                Text =
                @"Hey Scott,

If you got this, then that means you've figured out how to use MailKit to send a message. Grats ^^!

- Yourself"
            };

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587);
            }
            */

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
