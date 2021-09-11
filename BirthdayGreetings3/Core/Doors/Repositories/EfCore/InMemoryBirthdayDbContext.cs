using BirthdayGreetings3.Core.Doors.Repositories.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BirthdayGreetings3.Core.Doors.Repositories.EfCore
{
    public class InMemoryBirthdayDbContext: DbContext, IBirthdayDbContext
    {
        public DbSet<BirthdayMessageEntity> BithdayMessages { get; set; }

        public void Migrate(){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Test");
        }
    }
}