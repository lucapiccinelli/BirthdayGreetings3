using System.Collections.Generic;
using System.Text;
using BirthdayGreetings3.Core.Domain;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Domain.UseCases;
using BirthdayGreetings3.Core.Doors.Email;
using Moq;
using Xunit;

namespace BirthdayGreetings.Tests.Unit
{
    public class SendBirthdayEmailsTests
    {

        [Fact]
        public void CanSend_BirthdayEmail_StartingFromAListOf_Employees()
        {
            // Arrange
            Mock<IEmployeesRepository> repoMock = new Mock<IEmployeesRepository>();
            repoMock
                .Setup(repository => repository.GetAll())
                .Returns(() => EmployeesTestsHelper.TestEmployees);

            var birthdayMessagesRepository = new InMemoryBirthdayMessagesSender();
            BirthdayEmailService service = new BirthdayEmailService(repoMock.Object, birthdayMessagesRepository);
            var today = EmployeesTestsHelper.John.BirthDate.AddYears(30);

            // Act
            service.Send(today);

            //Assert
            var expectedMessages = new List<BirthdayEmail>
            {
                new BirthdayEmail(
                    EmployeesTestsHelper.John.EmailAddress, 
                    new BirthdayMessage(EmployeesTestsHelper.John.Name, today))
            };
            Assert.Equal(expectedMessages, birthdayMessagesRepository.BirtdayMessages);
        }

        public class InMemoryBirthdayMessagesSender : IBirthdayEmailSender
        {
            public List<BirthdayEmail> BirtdayMessages { get; } = new List<BirthdayEmail>();

            public void Send(BirthdayEmail email)
            {
                BirtdayMessages.Add(email);
            }
        }
    }
}
