using System;
using System.Collections.Generic;
using BirthdayGreetings3.Core.Domain;
using BirthdayGreetings3.Core.Domain.Model;
using Xunit;

namespace BirthdayGreetings.Tests.Unit
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
                new BirthdayMessage(EmployeesTestsHelper.John, today)
            };

            Assert.Equal(expectedBirthdayMessages, birthdayMessages);
        }
    }
}
