using System;
using System.Collections.Generic;
using System.Text;
using BirthdayGreetings3.Core;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings.Tests
{
    public static class EmployeesTestsHelper
    {
        public static readonly Employee John = new Employee("John", "Doe", new DateTime(1982, 10, 8), EmailAddress.Of("john.doe@foobar.com"));
        public static readonly Employee Mary = new Employee("Mary", "Ann", new DateTime(1975, 9, 11), EmailAddress.Of("mary.ann@foobar.com"));

        public static readonly List<Employee> TestEmployees = new List<Employee> {John, Mary};

        public static Employee WasBorn(this Employee employee, DateTime birthday)
        {
            return new Employee(employee.FirstName, employee.LastName, birthday, employee.EmailAddress);
        }
    }
}
