using System;
using BirthdayGreetings3.Core;
using BirthdayGreetings3.Core.Domain.UseCases;
using BirthdayGreetings3.Core.Doors.Repositories.Csv;
using BirthdayGreetings3.Core.Doors.Repositories.EfCore;

namespace BirthdayGreetings3
{
    class Program
    {
        static void Main(string[] args)
        {
            StoreBirthdayMessagesService service = new StoreBirthdayMessagesService(
                new EmployeesCsvReadSource(@"Resources\employees.txt"), 
                new EfBirthdayMessagesRepository(
                    new MySqlBirthdayDbContext(
                        new ConnectionOptions("localhost", 3306, "Test", "root", "sa"))));

            service.SaveBirthDaysOf(DateTime.Now);
        }
    }
}
