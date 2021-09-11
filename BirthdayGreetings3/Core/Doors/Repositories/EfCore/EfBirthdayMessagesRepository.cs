using System;
using System.Collections.Generic;
using System.Linq;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Doors.Repositories.EfCore.Entities;

namespace BirthdayGreetings3.Core.Doors.Repositories.EfCore
{
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

        public List<BirthdayMessage> GetByDate(DateTime dateTime) => _db.BithdayMessages
            .Select(ToDomainModel)
            .Where(message => message.Date == dateTime)
            .ToList();
    }
}