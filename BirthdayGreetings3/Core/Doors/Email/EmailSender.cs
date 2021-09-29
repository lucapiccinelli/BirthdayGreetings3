using System.Net;
using System.Net.Mail;
using BirthdayGreetings3.Core.Domain;

namespace BirthdayGreetings3.Core.Doors.Email
{
    public class EmailSender : IEmailSender
    {
        public void Send(EMailConfiguration configuration, string @from, string to, string message)
        {
            var client = new SmtpClient(configuration.Smtp, configuration.Port)
            {
                EnableSsl = configuration.Secured,
                Credentials = new NetworkCredential(configuration.MailCredentials.Username, configuration.MailCredentials.Password)
            };

            client.Send(new MailMessage(@from, to)
            {
                Body = message
            });
        }
    }
}