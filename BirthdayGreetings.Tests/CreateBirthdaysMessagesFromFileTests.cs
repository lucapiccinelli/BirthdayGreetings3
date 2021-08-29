using System;
using System.Collections.Generic;
using System.Text;
using BirthdayGreetings3.Core;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Domain.UseCases;
using BirthdayGreetings3.Core.Doors.Repositories.Csv;
using BirthdayGreetings3.Core.Doors.Repositories.SqlLite;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public class CreateBirthdaysMessagesFromFileTests
    {
        public static List<object[]> Can_CreateBirtdhdaysMessages_FromARepo_Data()
        {
            return new List<object[]>
            {
                new object[]{ new EmployeesCsvRepository(@"Resources\employees.txt")},
                new object[]{ new EmployeesSqlRepository(@"Resources\employees.db")},
            };
        }

        [Theory]
        [MemberData(nameof(Can_CreateBirtdhdaysMessages_FromARepo_Data))]
        public void Can_CreateBirtdhdaysMessages_FromARepo(IEmployeesRepository repository)
        {
            DateTime today = EmployeesTestsHelper.John.BirthDate.AddYears(30);
            List<BirthdayMessage> birthdayMessages = 
                new BirthdayMessagesService(repository).CreateMessages(today);

            List<BirthdayMessage> expectedBirthdayMessages = new List<BirthdayMessage>
            {
                new BirthdayMessage(EmployeesTestsHelper.John)
            };

            Assert.Equal(expectedBirthdayMessages, birthdayMessages);
        }

    }
}
