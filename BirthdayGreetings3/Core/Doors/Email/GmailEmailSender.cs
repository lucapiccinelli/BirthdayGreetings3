using BirthdayGreetings3.Core.Domain;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core.Doors.Email
{
    public class GmailEmailSender : IBirthdayEmailSender
    {
        private readonly IEmailSender _sender;
        public EMailConfiguration GmailConf { get; }

        public GmailEmailSender(MailCredentials credentials, IEmailSender sender)
        {
            _sender = sender;
            GmailConf = new EMailConfiguration(
                "smtp.gmail.com", 
                587, 
                secured: true,
                credentials);
        }

        public void Send(BirthdayEmail email)
        {
            _sender.Send(GmailConf, "luca.picci@gmail.com", email.Recipient.Value, email.ToMessage());
        }
    }
}