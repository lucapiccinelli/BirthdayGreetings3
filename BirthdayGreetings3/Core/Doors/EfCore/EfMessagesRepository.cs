using System.Collections.Generic;
using System.Linq;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Doors.EfCore.Entities;

namespace BirthdayGreetings3.Core.Doors.EfCore
{
    public class EfMessagesRepository: IRepository<BirthdayMessage>
    {
        private readonly BirthdayMessagesContex _db;

        public EfMessagesRepository(BirthdayMessagesContex db)
        {
            _db = db;
        }

        public void Save(BirthdayMessage birthdayMessages)
        {
            _db.BirthdayMessages.Add(new BirthdayMessageEntity
            {
                Date = birthdayMessages.Date,
                Firstname = birthdayMessages.Name.Firstname,
                Lastname = birthdayMessages.Name.Lastname,
            });
            _db.SaveChanges();
        }

        public IEnumerable<BirthdayMessage> GetAll() =>
            _db.BirthdayMessages
                .Select(entity => new BirthdayMessage(new PersonName(entity.Firstname, entity.Lastname), entity.Date))
                .ToList();
    }
}