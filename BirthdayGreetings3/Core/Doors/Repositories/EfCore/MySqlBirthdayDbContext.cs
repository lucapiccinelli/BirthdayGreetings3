using BirthdayGreetings3.Core.Doors.Repositories.EfCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BirthdayGreetings3.Core.Doors.Repositories.EfCore
{
    public class MySqlBirthdayDbContext: BirthdayDbContext
    {
        private readonly string _connectionString;

        public MySqlBirthdayDbContext(DbContextOptions options, string connectionString) : base(options)
        {
            _connectionString = connectionString;
        }
        public MySqlBirthdayDbContext(ConnectionOptions connectionOptions) : base()
        {
            _connectionString = connectionOptions.ConnectionString();
        }

        public override void Migrate() => Database.Migrate();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }
    }

    public class MySqlBirthdayDbContextDesign : IDesignTimeDbContextFactory<MySqlBirthdayDbContext>
    {
        public MySqlBirthdayDbContext CreateDbContext(string[] args)
        {
            string connectionString = args[0];

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<MySqlBirthdayDbContext>();
            optionsBuilder.UseMySQL(connectionString);
            return new MySqlBirthdayDbContext(optionsBuilder.Options, connectionString);
        }
    }
}