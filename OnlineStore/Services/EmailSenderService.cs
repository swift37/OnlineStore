using System.Net.Mail;
using System.Net;

namespace OnlineStore.Services
{
    public class EmailSenderService
    {
        //public EmailSenderService(BaseMail baseMail)
        //{
        //    
        //}

        public void SendMail(string to, string subject, string message)
        {
            MailMessage mail = new MailMessage("ivantestivan41@gmail.com", "to");
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("ivantestivan41@gmail.com", "Ivan123!@#");
            client.Host = "smtp.gmail.com";
            mail.Subject = subject;
            mail.Body = message;
            client.Send(mail);
        }
    }
}
