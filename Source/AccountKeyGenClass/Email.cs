using System;
using System.Net.Mail;
using System.Net;

namespace AccountKeyGenClass
{
    public class Email
    {
        public static void SendActivationEmail(string email, string subject, string body, string ipAddress, int port, string username, string pass)
        {
            try
            {

                MailMessage mailMessage = new MailMessage("webmaster@fortune.naw", email);
                SmtpClient smtpClient = new SmtpClient
                {
                    Host = ipAddress,
                    Port = port,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(username, pass)
                };
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                smtpClient.Send(mailMessage);
            }
            catch
            {
                Console.WriteLine("Couldnt send activation email! Please check configuration...");
            }
        }

        public static bool ValidEmailAddress(string email)
        {
            try
            {
                var emailValidator = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
