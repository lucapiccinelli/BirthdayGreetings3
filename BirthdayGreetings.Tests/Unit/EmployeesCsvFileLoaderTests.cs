using System.Collections.Generic;
using System.Linq;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Doors.Repositories.Csv;
using BirthdayGreetings3.Core.Exceptions;
using Xunit;

namespace BirthdayGreetings.Tests.Unit
{
    public class EmployeesCsvFileLoaderTests
    {
        [Fact]
        public void CanLoadEmployees_FromACsvFile()
        {
            List<Employee> expectedEmployees = EmployeesTestsHelper.TestEmployees;

            List<Employee> employees = EmployeesCsvFileLoader.Load(@"Resources\employees.txt");
            
            Assert.Equal(expectedEmployees, employees);
        }

        [Fact]
        public void LoadingBadFile_ShouldThrow_EmployeeLoadingException()
        {
            var ex = Assert.Throws<EmployeesLoadingException>(() => EmployeesCsvFileLoader.Load(@"Resources\bad_employees.txt"));
            Assert.Equal(2, ex.ExceptionsNumber);
            Assert.Equal(1, ex.Errors.First().LineNumber);
            Assert.Equal(2, ex.Errors.Last().LineNumber);
        }
    }
}
