using BirthdayGreetings3.Core.Doors.EfCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BirthdayGreetings3.Core.Doors.EfCore
{
    public class BirthdayMessagesContext : DbContext
    {
        private readonly string _connectionString;

        public BirthdayMessagesContext(MySqlConnectionOptions connectionOptions)
        {
            _connectionString = connectionOptions.ConnectionString();
        }

        private BirthdayMessagesContext(DbContextOptions optionsBuilderOptions, string connectionString)
            : base(optionsBuilderOptions)
        {
            _connectionString = connectionString;
        }

        public DbSet<BirthdayMessageEntity> BirthdayMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }

        public void Migrate()
        {
            Database.Migrate();
        }

        public class MySqlBirthdayDbContextDesign : IDesignTimeDbContextFactory<BirthdayMessagesContext>
        {
            public BirthdayMessagesContext CreateDbContext(string[] args)
            {
                string connectionString = args[0];

                DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<BirthdayMessagesContext>();
                optionsBuilder.UseMySQL(connectionString);
                return new BirthdayMessagesContext(optionsBuilder.Options, connectionString);
            }
        }
    }
}