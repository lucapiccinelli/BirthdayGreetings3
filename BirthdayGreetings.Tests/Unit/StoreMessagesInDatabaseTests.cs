using System.Collections.Generic;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Domain.UseCases;
using Moq;
using Xunit;

namespace BirthdayGreetings.Tests.Unit
{
    public class StoreMessagesInDatabaseTests
    {

        [Fact]
        public void CanStoreMessagesInDb()
        {
            // Arrange
            Mock<IEmployeesRepository> repoMock = new Mock<IEmployeesRepository>();
            repoMock
                .Setup(repository => repository.GetAll())
                .Returns(() => EmployeesTestsHelper.TestEmployees);

            var birthdayMessagesRepository = new InMemoryBirthdayMessagesRepository();
            BirthdayStoreService service = new BirthdayStoreService(repoMock.Object, birthdayMessagesRepository);
            var today = EmployeesTestsHelper.John.BirthDate.AddYears(30);

            // Act
            service.StoreBirthdayMessages(today);

            //Assert
            var expectedMessages = new List<BirthdayMessage>
            {
                new BirthdayMessage(EmployeesTestsHelper.John.Name, today)
            };
            Assert.Equal(expectedMessages, birthdayMessagesRepository.GetAll()); 
        }

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
