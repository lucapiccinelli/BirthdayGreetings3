using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using BirthdayGreetings3.Core.Domain.Model;
using Moq;
using Xunit;

namespace BirthdayGreetings.Tests.Integration
{
    public class EmailServicetests
    {

        [Fact]
        public void CanSend_AnEmail()
        {

            // Arrange
            Mock<IEmailSender> mockSender = new Mock<IEmailSender>();
            var expectedCredentials = new MailCredentials("luca.picci@gmail.com", "bla");
            GmailEmailSender sender = new GmailEmailSender(expectedCredentials, mockSender.Object);
            EMailConfiguration expectedConf = sender.GmailConf;
            var email = new BirthdayEmail(
                EmailAddress.Of("luca.picci@gmail.com"),
                new BirthdayMessage(new PersonName("Luca", "Piccinelli"), DateTime.Now));

            // Act
            sender.Send(email);

            // Assert
            mockSender.Verify(emailSender => 
                emailSender.Send(
                    expectedConf, 
                    email.Recipient.Value, 
                    email.Recipient.Value, 
                    $"Happy Birthday dear Luca Piccinelli"));
        }

    }

    public class GmailEmailSender
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

    public interface IEmailSender
    {
        void Send(EMailConfiguration configuration, string @from, string to, string message);
    }

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

    public class EMailConfiguration
    {
        public string Smtp { get; }
        public int Port { get; }
        public bool Secured { get; }
        public MailCredentials MailCredentials { get; }

        public EMailConfiguration(string smtp, int port, bool secured, MailCredentials mailCredentials)
        {
            Smtp = smtp;
            Port = port;
            Secured = secured;
            MailCredentials = mailCredentials;
        }

        public override string ToString()
        {
            return $"{nameof(Smtp)}: {Smtp}, {nameof(Port)}: {Port}, {nameof(Secured)}: {Secured}, {nameof(MailCredentials)}: {MailCredentials}";
        }

        protected bool Equals(EMailConfiguration other)
        {
            return Smtp == other.Smtp && Port == other.Port && Secured == other.Secured && Equals(MailCredentials, other.MailCredentials);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EMailConfiguration) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Smtp, Port, Secured, MailCredentials);
        }
    }

    public class MailCredentials
    {
        public string Username { get; }
        public string Password { get; }

        public MailCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public override string ToString()
        {
            return $"{nameof(Username)}: {Username}, {nameof(Password)}: {Password}";
        }

        protected bool Equals(MailCredentials other)
        {
            return Username == other.Username && Password == other.Password;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MailCredentials) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Username, Password);
        }
    }
}
