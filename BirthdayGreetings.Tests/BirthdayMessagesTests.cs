using System;
using System.Collections.Generic;
using System.Text;
using BirthdayGreetings3.Core;
using BirthdayGreetings3.Core.Domain;
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
}
