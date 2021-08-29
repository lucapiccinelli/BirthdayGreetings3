using System;
using System.Collections.Generic;
using BirthdayGreetings3.Core.Domain.Model;
using Xunit;

namespace BirthdayGreetings.Tests.Unit
{
    public class EmployeeBirthdaysTests
    {
        public static List<object[]> GIVEN_aDate_TellIfItIs_ABirthday_Data()
        {
            return new List<object[]>
            {
                new object[]{ EmployeesTestsHelper.John.BirthDate, EmployeesTestsHelper.John.BirthDate.AddYears(30), true},
                new object[]{ EmployeesTestsHelper.John.BirthDate, EmployeesTestsHelper.John.BirthDate.AddMonths(10), false},
                new object[]{ new DateTime(2008, 02, 29), new DateTime(2012, 02, 29), true},
                new object[]{ new DateTime(2008, 02, 29), new DateTime(2013, 02, 28), true}
            };
        }


        [Theory]
        [MemberData(nameof(GIVEN_aDate_TellIfItIs_ABirthday_Data))]
        public void GIVEN_aDate_TellIfItIs_ABirthday(DateTime birthdate, DateTime today, bool expected)
        {
            Employee employee = EmployeesTestsHelper.John.WasBorn(birthdate);
            Assert.Equal(expected, employee.IsBirthday(today));
        }

    }
}
