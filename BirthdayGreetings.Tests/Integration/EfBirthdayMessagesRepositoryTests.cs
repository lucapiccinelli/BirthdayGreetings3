using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BirthdayGreetings.Tests.Unit;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Doors.Repositories;
using BirthdayGreetings3.Core.Doors.Repositories.EfCore;
using BirthdayGreetings3.Core.Doors.Repositories.EfCore.Entities;
using Xunit;

namespace BirthdayGreetings.Tests.Integration
{
    public class EfBirthdayMessagesRepositoryTests
    {

        [Fact]
        public void CanReadAll_FromTheRepository()
        {
            PrepareDb((expectedMessages) =>
            {
                EfBirthdayMessagesRepository repo = new EfBirthdayMessagesRepository();
                Assert.Equal(expectedMessages, repo.GetAll());
            });
        }

        [Fact]
        public void CanSave_IntoTheRepository()
        {
            EfBirthdayMessagesRepository repo = new EfBirthdayMessagesRepository();
            var now = DateTime.Now;
            var birthdayMessage = new BirthdayMessage(new PersonName("Luca", "Piccinelli"), now);
            repo.Save(birthdayMessage);

            var db = new BirthdayDbContext();

            var expectedEntity = new BirthdayMessageEntity
            {
                Firstname = birthdayMessage.Name.Firstname,
                Lastname = birthdayMessage.Name.Lastname,
                Date = birthdayMessage.Date,
            };

            BirthdayMessageEntity actualEntity = db.BithdayMessages.ToList().First();
            Assert.Equal(expectedEntity.Firstname, actualEntity.Firstname);
            Assert.Equal(expectedEntity.Lastname, actualEntity.Lastname);
            Assert.Equal(expectedEntity.Date, actualEntity.Date);
        }

        private void PrepareDb(Action<List<BirthdayMessage>> test)
        {
            var expectedMessages = EmployeesTestsHelper.TestEmployees
                .Select((employee, i) => new BirthdayMessage(employee.Name, DateTime.Now.AddYears(i)))
                .ToList();

            BirthdayDbContext db = new BirthdayDbContext();
            expectedMessages.ForEach(message => db.Add(new BirthdayMessageEntity
            {
                Firstname = message.Name.Firstname,
                Lastname = message.Name.Lastname,
                Date = message.Date
            }));
            db.SaveChanges();

            test(expectedMessages);

            db.RemoveRange(db.BithdayMessages.ToList());
            db.SaveChanges();
        }
    }

    public class EfBirthdayMessagesRepository : IBirthdayMessageRepository
    {
        private readonly BirthdayDbContext _db = new BirthdayDbContext();

        public void Save(BirthdayMessage message)
        {
            _db.BithdayMessages.Add(new BirthdayMessageEntity
            {
                Date = message.Date,
                Firstname = message.Name.Firstname,
                Lastname = message.Name.Lastname,
            });
            _db.SaveChanges();
        }

        public List<BirthdayMessage> GetAll() =>
            _db.BithdayMessages
                .Select(ToDomainModel)
                .ToList();

        private static BirthdayMessage ToDomainModel(BirthdayMessageEntity entity) => 
            new BirthdayMessage(
                new PersonName(entity.Firstname, entity.Lastname), 
                entity.Date);

        public List<BirthdayMessage> GetByDate(DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}
