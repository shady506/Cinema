using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Cinema.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("shady.shda3333@gmail.com", "cpto gkby plps knee")
            };

            return client.SendMailAsync(
           new MailMessage(from: "your.email@live.com",
                           to: email,
                           subject,
                           htmlMessage
                           )
           {
               IsBodyHtml = true
           });
        }
    }
}
