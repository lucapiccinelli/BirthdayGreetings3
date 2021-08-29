using System.Collections.Generic;
using System.Linq;
using BirthdayGreetings3.Core;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Doors.Repositories.SqlLite;
using BirthdayGreetings3.Core.Exceptions;
using Xunit;

namespace BirthdayGreetings.Tests
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
