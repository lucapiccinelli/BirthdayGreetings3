using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BirthdayGreetings.Tests.Unit;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Doors.EfCore;
using BirthdayGreetings3.Core.Doors.EfCore.Entities;
using DockerTest;
using Xunit;

namespace BirthdayGreetings.Tests.Integration
{
    public class EfMessagesRepositoryTestsFixture: IDisposable
    {
        public MySqlContainer Container { get; }

        public EfMessagesRepositoryTestsFixture()
        {
            int port = new Random().Next(10000, 10500);
            Container = new MySqlContainer(port: port);
            Container.Start();
        }

        public void Dispose()
        {
            Container.Stop();
        }
    }


    public class EfMessagesRepositoryTests: IDisposable, IClassFixture<EfMessagesRepositoryTestsFixture>
    {
        private readonly MySqlContainer _container;
        private readonly BirthdayMessagesContex _db;

        public EfMessagesRepositoryTests(EfMessagesRepositoryTestsFixture fixture)
        {
            _container = fixture.Container;
            _db = new BirthdayMessagesContex(new MySqlConnectionOptions()
            {
                Host = "localhost",
                Port = _container.ExternalPort,
                User = "root",
                Password = _container.Password,
                DbName = "Test"
            });
        }

        public void Dispose()
        {
            _db.BirthdayMessages.RemoveRange(_db.BirthdayMessages.ToList());
            _db.SaveChanges();
            _db.Dispose();
        }

        [Fact]
        public void CanReadMessages_FromTheRepo()
        {
            List<BirthdayMessage> expectedMessages = PrepareDb(_db);

            EfMessagesRepository repository = new EfMessagesRepository(_db);
            Assert.Equal(expectedMessages, repository.GetAll());
        }

        [Fact]
        public void CanSaveMessages_IntoTheRepo()
        {
            EfMessagesRepository repository = new EfMessagesRepository(_db);
            var expected = new BirthdayMessage(
                EmployeesTestsHelper.John.Name, 
                EmployeesTestsHelper.John.BirthDate.AddYears(5));

            repository.Save(expected);

            var actual = repository.GetAll().First();
            Assert.Equal(expected, actual);

        }

        private static List<BirthdayMessage> PrepareDb(BirthdayMessagesContex db)
        {
            var birthdayMessages = new List<BirthdayMessage>
            {
                new BirthdayMessage(EmployeesTestsHelper.John.Name, EmployeesTestsHelper.John.BirthDate.AddYears(30)),
                new BirthdayMessage(EmployeesTestsHelper.Mary.Name, EmployeesTestsHelper.Mary.BirthDate.AddYears(10))
            };
            birthdayMessages
                .Select(message => new BirthdayMessageEntity
                {
                    Date = message.Date,
                    Firstname = message.Name.Firstname,
                    Lastname = message.Name.Lastname
                })
                .ToList()
                .ForEach(message => db.BirthdayMessages.Add(message));

            db.SaveChanges();

            return birthdayMessages;
        }
    }
}
