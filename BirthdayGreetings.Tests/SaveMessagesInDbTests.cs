using System.Collections.Generic;
using System.Text;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Domain.UseCases;
using Moq;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public class SaveMessagesInDbTests
    {
        [Fact]
        public void CanStoreBirthdayMessages()
        {
            Mock<IEmployeesReadSource> employeesRepo = new Mock<IEmployeesReadSource>();
            employeesRepo
                .Setup(repository => repository.GetAll())
                .Returns(() => EmployeesTestsHelper.TestEmployees);

            StoreBirthdayMessagesService service = new StoreBirthdayMessagesService(
                employeesRepo.Object, 
                new InMemoryMessagesRepository());

            var today = EmployeesTestsHelper.John.BirthDate.AddYears(30);
            service.SaveBirthDaysOf(today);

            List<BirthdayMessage> messages = service.GetSavedMessages();
            List<BirthdayMessage> expectedMessages = new List<BirthdayMessage>
            {
                new BirthdayMessage(EmployeesTestsHelper.John, today)
            };

            Assert.Equal(expectedMessages, messages);
        }
    }

    public class InMemoryMessagesRepository : IRepository<BirthdayMessage>
    {
        private readonly List<BirthdayMessage> _birthdayMessages;

        public InMemoryMessagesRepository()
        {
            _birthdayMessages = new List<BirthdayMessage>();
        }

        public void Save(BirthdayMessage message)
        {
            _birthdayMessages.Add(message);
        }

        public List<BirthdayMessage> GetAll() => _birthdayMessages;
    }
}
