using BirthdayGreetings3.Core.Doors.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BirthdayGreetings3.Core.Doors.EfCore
{
    public class BirthdayMessagesContex : DbContext
    {
        private readonly MySqlConnectionOptions _connectionOptions;

        public BirthdayMessagesContex(MySqlConnectionOptions connectionOptions)
        {
            _connectionOptions = connectionOptions;
        }

        public DbSet<BirthdayMessageEntity> BirthdayMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionOptions.ConnectionString());
        }
    }
}