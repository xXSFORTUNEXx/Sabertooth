using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Net.Mail;
using System.Net;
using static SabertoothServer.Globals;

namespace AccountKeyGen
{
    public static class KeyGen
    {
        private static Random RND = new Random();
        static void Main(string[] args)
        {
            Title = "Account Key Generator";
            string key = Key(25);
            WriteLine("User Key: " + key);
            Write("Email address: ");
            string email = ReadLine();
            Write("Subject: ");
            string subject = ReadLine();
            Write("Body: ");
            string body = ReadLine() + " " + key;
            WriteLine("Sent to: " + email + "\nSubject: " + subject + "\nBody: " + body);
            SendActivationEmail(email, subject, body);
            ReadKey();
        }

        public static string Key(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[RND.Next(s.Length)]).ToArray());
        }

        public static void SendActivationEmail(string email, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage("webmaster@fortune.naw", email);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SMTP_IP_ADDRESS;
            smtpClient.Port = SMTP_SERVER_PORT;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential(SMTP_USER_CREDS, SMTP_PASS_CREDS);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            smtpClient.Send(mailMessage);            
        }
    }
}
