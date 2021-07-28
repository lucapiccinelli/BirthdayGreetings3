using System;
using System.Collections.Generic;
using System.Text;
using BirthdayGreetings3.Core;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings.Tests
{
    public static class EmployeesTestsHelper
    {
        public static Employee John = new Employee("John", "Doe", new DateTime(1982, 10, 8), EmailAdress.Of("john.doe@foobar.com"));
        public static Employee Mary = new Employee("Mary", "Ann", new DateTime(1975, 9, 11), EmailAdress.Of("mary.ann@foobar.com"));

        public static List<Employee> TestEmployees = new List<Employee> {John, Mary};
    }
}
