using System.Collections.Generic;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Doors.Repositories.SqlLite;
using Xunit;

namespace BirthdayGreetings.Tests.Unit
{
    public class EmployeesSqlLiteFileLoaderTests
    {
        [Fact]
        public void CanLoadEmployees_FromACsvFile()
        {
            List<Employee> expectedEmployees = EmployeesTestsHelper.TestEmployees;

            List<Employee> employees = EmployeesSqlLiteFileLoader.Load(@"Resources\employees.db");

            Assert.Equal(expectedEmployees, employees);
        }
    }
}
