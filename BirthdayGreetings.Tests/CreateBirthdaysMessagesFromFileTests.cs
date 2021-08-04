using System;
using System.Collections.Generic;
using System.Text;
using BirthdayGreetings3.Core;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public class CreateBirthdaysMessagesFromFileTests
    {

        [Fact]
        public void Can_CreateBirtdhdaysMessages_FromACsv()
        {
            DateTime today = EmployeesTestsHelper.John.BirthDate.AddYears(30);
            List<BirthdayMessage> birthdayMessages = BirthdayMessages.FromCsv(@"Resources\employees.txt", today);

            List<BirthdayMessage> expectedBirthdayMessages = new List<BirthdayMessage>
            {
                new BirthdayMessage(EmployeesTestsHelper.John)
            };

            Assert.Equal(expectedBirthdayMessages, birthdayMessages);
        }

    }
}
