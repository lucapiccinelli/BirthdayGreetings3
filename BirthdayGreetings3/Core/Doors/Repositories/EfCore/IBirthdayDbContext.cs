using BirthdayGreetings3.Core.Doors.Repositories.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BirthdayGreetings3.Core.Doors.Repositories.EfCore
{
    public interface IBirthdayDbContext
    {
        DbSet<BirthdayMessageEntity> BithdayMessages { get; set; }
        void Migrate();
    }
}