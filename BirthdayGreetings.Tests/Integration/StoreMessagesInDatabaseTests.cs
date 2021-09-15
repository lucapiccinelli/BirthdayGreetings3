using System;
using System.Collections.Generic;
using System.Text;
using BirthdayGreetings.Tests.Unit;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Doors;
using Xunit;

namespace BirthdayGreetings.Tests.Integration
{
    class StoreMessagesInDatabaseTests
    {

        public void test()
        {
            BirthdayStoreService service = new BirthdayStoreService(EmployeesTestsHelper.TestEmployees);

            var today = EmployeesTestsHelper.John.BirthDate.AddYears(30);
            service.StoreBirthdayMessages(today);

            var expectedMessages = new List<BirthdayMessage>
            {
                new BirthdayMessage(EmployeesTestsHelper.John)
            };

            Assert.Equal(expectedMessages, service._birtdayMessages); 
        }

    }

    class BirthdayStoreService
    {
        public IEnumerable<BirthdayMessage> _birtdayMessages;

        public BirthdayStoreService(List<Employee> testEmployees)
        {
            
        }

        public void StoreBirthdayMessages(in DateTime today)
        {
            
        }
    }
}
