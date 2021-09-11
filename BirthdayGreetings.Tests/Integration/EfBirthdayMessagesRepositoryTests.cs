using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BirthdayGreetings.Tests.Unit;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Doors.Repositories.EfCore;
using BirthdayGreetings3.Core.Doors.Repositories.EfCore.Entities;
using Xunit;

namespace BirthdayGreetings.Tests.Integration
{
    public class EfBirthdayMessagesRepositoryTests: IDisposable
    {



        [Fact]
        public void CanReadAll_FromTheRepository()
        {
            List<BirthdayMessage> expectedMessages = PrepareDb();
            EfBirthdayMessagesRepository repo = new EfBirthdayMessagesRepository();
            Assert.Equal(expectedMessages, repo.GetAll());
        }

        [Fact]
        public void CanSave_IntoTheRepository()
        {
            EfBirthdayMessagesRepository repo = new EfBirthdayMessagesRepository();
            var birthdayMessage = new BirthdayMessage(new PersonName("Luca", "Piccinelli"), DateTime.Now);
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

        [Fact]
        public void CanGetByDate()
        {
            List<BirthdayMessage> expectedMessages = PrepareDb().Take(1).ToList();
            EfBirthdayMessagesRepository repo = new EfBirthdayMessagesRepository();
            Assert.Equal(expectedMessages, repo.GetByDate(expectedMessages.First().Date));
        }

        private List<BirthdayMessage> PrepareDb()
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

            return expectedMessages;
        }

        public void Dispose()
        {
            BirthdayDbContext db = new BirthdayDbContext();
            db.RemoveRange(db.BithdayMessages.ToList());
            db.SaveChanges();
        }
    }
}
