using BirthdayGreetings3.Core.Doors.Repositories.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BirthdayGreetings3.Core.Doors.Repositories.EfCore
{
    public class BirthdayDbContext: DbContext
    {
        public DbSet<BirthdayMessageEntity> BithdayMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Test");
        }
    }
}