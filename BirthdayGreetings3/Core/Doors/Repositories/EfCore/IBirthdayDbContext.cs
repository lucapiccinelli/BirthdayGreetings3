using System.Drawing;
using BirthdayGreetings3.Core.Doors.Repositories.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BirthdayGreetings3.Core.Doors.Repositories.EfCore
{
    public abstract class BirthdayDbContext: DbContext
    {
        protected BirthdayDbContext(): base()
        {
        }

        protected BirthdayDbContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<BirthdayMessageEntity> BithdayMessages { get; set; }

        public abstract void Migrate();
    }
}