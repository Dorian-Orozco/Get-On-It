using System.Net.Mail;
using System.Net;

namespace GetOnIt.Services
{
    public class SendEmail
    {
        //Simple Task to Create and Pass back and email to send, public since it needs to be accessed in many places
        public async Task<bool> SendEmailAsync(string email, string subject, string confirmLink)
        {
            try //try to set up the email, if fail return false.
            {
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                message.From = new MailAddress("dorianCodeDemo@outlook.com");
                message.To.Add(email);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = confirmLink;

                // Set up port and host name 
                smtpClient.Port = 587;
                smtpClient.Host = "smtp-mail.outlook.com";

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("dorianCodeDemo@outlook.com", "nairoD4002!"); //please dont steal my account :(
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtpClient.Send(message);
            }

            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
