using System;
using System.Collections.Generic;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core.Doors.Repositories
{
    public interface IBirthdayMessageRepository : IRepository<BirthdayMessage>
    {
        List<BirthdayMessage> GetByDate(DateTime dateTime);
    }
}