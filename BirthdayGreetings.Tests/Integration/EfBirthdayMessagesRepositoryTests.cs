using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BirthdayGreetings.Tests.Unit;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Doors.Repositories.EfCore;
using BirthdayGreetings3.Core.Doors.Repositories.EfCore.Entities;
using DockerTest;
using Xunit;

namespace BirthdayGreetings.Tests.Integration
{
    public class MySqlFixture: IDisposable
    {
        public MySqlContainer Container { get; }

        public MySqlFixture()
        {
            Container = new MySqlContainer(port: new Random().Next(10000, 10500));
            Container.Start();
        }

        public void Dispose()
        {
            Container.Stop();
        }
    }

    public class EfBirthdayMessagesRepositoryTests: IClassFixture<MySqlFixture>, IDisposable
    {
        private readonly BirthdayDbContext _db;

        public EfBirthdayMessagesRepositoryTests(MySqlFixture fixture)
        {
            MySqlContainer container = fixture.Container;
            _db = new MySqlBirthdayDbContext(
                new ConnectionOptions(
                    "localhost", 
                    container.ExternalPort, 
                    "Test", 
                    "root", 
                    container.Password));
        }

        [Fact]
        public void CanReadAll_FromTheRepository()
        {
            List<BirthdayMessage> expectedMessages = PrepareDb();
            EfBirthdayMessagesRepository repo = new EfBirthdayMessagesRepository(_db);
            Assert.Equal(expectedMessages, repo.GetAll());
        }

        [Fact]
        public void CanSave_IntoTheRepository()
        {
            EfBirthdayMessagesRepository repo = new EfBirthdayMessagesRepository(_db);
            var birthdayMessage = new BirthdayMessage(new PersonName("Luca", "Piccinelli"), DateTime.Now);
            repo.Save(birthdayMessage);
            
            var expectedEntity = new BirthdayMessageEntity
            {
                Firstname = birthdayMessage.Name.Firstname,
                Lastname = birthdayMessage.Name.Lastname,
                Date = birthdayMessage.Date,
            };

            BirthdayMessageEntity actualEntity = _db.BithdayMessages.ToList().First();
            Assert.Equal(expectedEntity.Firstname, actualEntity.Firstname);
            Assert.Equal(expectedEntity.Lastname, actualEntity.Lastname);
            Assert.Equal(expectedEntity.Date, actualEntity.Date);
        }

        [Fact]
        public void CanGetByDate()
        {
            List<BirthdayMessage> expectedMessages = PrepareDb().Take(1).ToList();
            EfBirthdayMessagesRepository repo = new EfBirthdayMessagesRepository(_db);
            Assert.Equal(expectedMessages, repo.GetByDate(expectedMessages.First().Date));
        }

        private List<BirthdayMessage> PrepareDb()
        {
            _db.Migrate();
            var expectedMessages = EmployeesTestsHelper.TestEmployees
                .Select((employee, i) => new BirthdayMessage(employee.Name, DateTime.Now.AddYears(i)))
                .ToList();

            expectedMessages.ForEach(message => _db.Add(new BirthdayMessageEntity
            {
                Firstname = message.Name.Firstname,
                Lastname = message.Name.Lastname,
                Date = message.Date
            }));
            _db.SaveChanges();

            return expectedMessages;
        }

        public void Dispose()
        {
            _db.RemoveRange(_db.BithdayMessages.ToList());
            _db.SaveChanges();
        }
    }
}
