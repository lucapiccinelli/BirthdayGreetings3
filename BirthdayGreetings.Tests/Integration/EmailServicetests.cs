using System;
using System.Collections.Generic;
using System.Text;
using BirthdayGreetings3.Core.Domain;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Doors.Email;
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
}
