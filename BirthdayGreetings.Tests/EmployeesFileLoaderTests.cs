using System;
using System.Collections.Generic;
using BirthdayGreetings3.Core;
using BirthdayGreetings3.Core.Domain.Model;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public class EmployeesFileLoaderTests
    {
        [Fact]
        public void CanLoadEmployees_FromACsvFile()
        {
            List<Employee> expectedEmployees = EmployeesTestsHelper.TestEmployees;

            List<Employee> employees = EmployeesFileLoader.Load(@"Resources\employees.txt");

            Assert.Equal(expectedEmployees, employees);
        }
    }
}
