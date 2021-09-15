using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BirthdayGreetings.Tests.Unit;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Doors.EfCore;
using BirthdayGreetings3.Core.Doors.EfCore.Entities;
using Xunit;

namespace BirthdayGreetings.Tests.Integration
{
    public class EfMessagesRepositoryTests: IDisposable
    {
        private readonly BirthdayMessagesContex _db;

        public EfMessagesRepositoryTests()
        {
            _db = new BirthdayMessagesContex();
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
