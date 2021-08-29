using System;
using System.Collections.Generic;
using System.Linq;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Domain.UseCases;
using BirthdayGreetings3.Core.Doors.Repositories;
using Moq;
using Xunit;

namespace BirthdayGreetings.Tests.Unit
{
    public class SaveMessagesInDbTests
    {
        private readonly Mock<IEmployeesReadSource> _employeesRepo;

        public SaveMessagesInDbTests()
        {
            _employeesRepo = new Mock<IEmployeesReadSource>();
            _employeesRepo
                .Setup(repository => repository.GetAll())
                .Returns(() => EmployeesTestsHelper.TestEmployees);
        }

        [Fact]
        public void CanStoreBirthdayMessages()
        {
            var messagesRepository = new InMemoryMessagesRepository();
            StoreBirthdayMessagesService service = new StoreBirthdayMessagesService(
                _employeesRepo.Object, 
                messagesRepository);

            var today = EmployeesTestsHelper.John.BirthDate.AddYears(30);
            service.SaveBirthDaysOf(today);

            List<BirthdayMessage> messages = messagesRepository.GetAll();
            List<BirthdayMessage> expectedMessages = new List<BirthdayMessage>
            {
                new BirthdayMessage(EmployeesTestsHelper.John, today)
            };

            Assert.Equal(expectedMessages, messages);
        }

        [Fact]
        public void CanGetMessagesByDate()
        {
            var messagesRepository = new InMemoryMessagesRepository();
            StoreBirthdayMessagesService service = new StoreBirthdayMessagesService(
                _employeesRepo.Object, 
                messagesRepository);

            var today1 = EmployeesTestsHelper.Mary.BirthDate.AddYears(30);
            service.SaveBirthDaysOf(today1);

            var today2 = EmployeesTestsHelper.John.BirthDate.AddYears(30);
            service.SaveBirthDaysOf(today2);


            List<BirthdayMessage> messages = service.GetSavedMessages(today1);
            List<BirthdayMessage> expectedMessages = new List<BirthdayMessage>
            {
                new BirthdayMessage(EmployeesTestsHelper.Mary, today1)
            };

            Assert.Equal(expectedMessages, messages);
        }
    }

    public class InMemoryMessagesRepository : IBirthdayMessageRepository
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

        public List<BirthdayMessage> GetByDate(DateTime dateTime) => GetAll()
            .Where(message => message.Date == dateTime)
            .ToList();
    }
}
