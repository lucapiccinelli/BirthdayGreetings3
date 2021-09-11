using BirthdayGreetings3.Core.Doors.Repositories.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BirthdayGreetings3.Core.Doors.Repositories.EfCore
{
    public class InMemoryBirthdayDbContext: BirthdayDbContext
    {
        public override void Migrate(){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Test");
        }
    }
}