using System;
using System.Collections.Generic;
using System.Text;
using BirthdayGreetings.Tests.Unit;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Domain.UseCases;
using BirthdayGreetings3.Core.Doors;
using Moq;
using Xunit;

namespace BirthdayGreetings.Tests.Integration
{
    public class StoreMessagesInDatabaseTests
    {

        [Fact]
        public void CanStoreMessagesInDb()
        {
            Mock<IEmployeesRepository> repoMock = new Mock<IEmployeesRepository>();
            repoMock
                .Setup(repository => repository.GetAll())
                .Returns(() => EmployeesTestsHelper.TestEmployees);

            var birthdayMessagesRepository = new InMemoryBirthdayMessagesRepository();
            BirthdayStoreService service = new BirthdayStoreService(repoMock.Object, birthdayMessagesRepository);

            var today = EmployeesTestsHelper.John.BirthDate.AddYears(30);
            service.StoreBirthdayMessages(today);

            var expectedMessages = new List<BirthdayMessage>
            {
                new BirthdayMessage(EmployeesTestsHelper.John.Name, today)
            };

            Assert.Equal(expectedMessages, birthdayMessagesRepository.GetAll()); 
        }

    }

    public class BirthdayStoreService
    {
        private readonly BirthdayMessagesService _service;
        private readonly IRepository<BirthdayMessage> _birthdayMessagesRepository;

        public BirthdayStoreService(IEmployeesRepository repository, IRepository<BirthdayMessage> birthdayMessagesRepository)
        {
            _birthdayMessagesRepository = birthdayMessagesRepository;
            _service = new BirthdayMessagesService(repository);
        }

        public void StoreBirthdayMessages(in DateTime today)
        {
            _service.CreateMessages(today)
                .ForEach(message => _birthdayMessagesRepository.Save(message));
        }
    }

    public interface IRepository<T>
    {
        void Save(T birthdayMessages);
        IEnumerable<T> GetAll();
    }

    public class InMemoryBirthdayMessagesRepository : IRepository<BirthdayMessage>
    {
        private readonly List<BirthdayMessage> _birtdayMessages = new List<BirthdayMessage>();

        public void Save(BirthdayMessage birthdayMessages)
        {
            _birtdayMessages.Add(birthdayMessages);;
        }

        public IEnumerable<BirthdayMessage> GetAll()
        {
            return _birtdayMessages;
        }
    }
}
