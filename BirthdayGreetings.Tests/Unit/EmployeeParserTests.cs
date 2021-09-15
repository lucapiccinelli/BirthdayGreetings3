using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Doors;
using BirthdayGreetings3.Core.Exceptions;
using Xunit;

namespace BirthdayGreetings.Tests.Unit
{
    public class EmployeeParserTests
    {
        [Fact]
        public void CanParseAnEmployee()
        {
            string employeeLine = "Doe, John, 1982/10/08, john.doe@foobar.com";
            Employee actualEmployee = EmployeeParser.ToEmployee(employeeLine);

            Assert.Equal(EmployeesTestsHelper.John, actualEmployee);
        }

        [Theory]
        [InlineData("Doe, John, bla, john.doe@foobar.com")]
        [InlineData("Doe, John, 1982/10/08, john.doefoobar.com")]
        [InlineData("Doe, John, 1982/10/08")]
        public void IfTheLine_IsNotWellFormed_ShouldThrow_EmployeeParsingException(string employeeLine)
        {
            Assert.Throws<EmployeeParsingException>(() => EmployeeParser.ToEmployee(employeeLine));
        }
    }
}
