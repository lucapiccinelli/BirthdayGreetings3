using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BirthdayGreetings3.Core;
using BirthdayGreetings3.Core.Domain.Model;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public class BirthdayMessagesTests
    {
        [Fact]
        public void CreatesBirthDayMessages_FromAList_OfEmployees_OnlyOneHasBirthday()
        {
            DateTime today = EmployeesTestsHelper.John.BirthDate.AddYears(30);
            List<BirthdayMessage> birthdayMessages = BirthdayMessages.Of(EmployeesTestsHelper.TestEmployees, today);

            List<BirthdayMessage> expectedBirthdayMessages = new List<BirthdayMessage>
            {
                new BirthdayMessage(EmployeesTestsHelper.John)
            };

            Assert.Equal(expectedBirthdayMessages, birthdayMessages);
        }
    }

    public static class BirthdayMessages
    {
        public static List<BirthdayMessage> Of(List<Employee> employees, DateTime today)
        {
            return employees
                .Where((employee) => employee.IsBirthday(today))
                .Select(employee => new BirthdayMessage(employee))
                .ToList();
        }
    }
}
