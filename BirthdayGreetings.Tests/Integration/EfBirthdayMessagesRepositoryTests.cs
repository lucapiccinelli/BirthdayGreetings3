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

        private void PrepareDb(Action<List<BirthdayMessage>> test)
        {
            var expectedMessages = EmployeesTestsHelper.TestEmployees
                .Select((employee, i) => new BirthdayMessage(employee.Name, DateTime.Now.AddYears(i)))
                .ToList();

            BirthdayDbContext db = new BirthdayDbContext();
            expectedMessages.ForEach(message => db.Add(new BithdayMessageEntity
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
        private BirthdayDbContext _db = new BirthdayDbContext();

        public void Save(BirthdayMessage message)
        {
            throw new NotImplementedException();
        }

        public List<BirthdayMessage> GetAll() =>
            _db.BithdayMessages
                .Select(ToDomainModel)
                .ToList();

        private static BirthdayMessage ToDomainModel(BithdayMessageEntity entity) => 
            new BirthdayMessage(
                new PersonName(entity.Firstname, entity.Lastname), 
                entity.Date);

        public List<BirthdayMessage> GetByDate(DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}
