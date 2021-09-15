using BirthdayGreetings3.Core.Doors.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BirthdayGreetings3.Core.Doors.EfCore
{
    public class BirthdayMessagesContex : DbContext
    {
        public DbSet<BirthdayMessageEntity> BirthdayMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Test");
        }
    }
}