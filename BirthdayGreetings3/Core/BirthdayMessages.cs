using System;
using System.Collections.Generic;
using System.Linq;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core
{
    public static class BirthdayMessages
    {
        public static List<BirthdayMessage> Of(List<Employee> employees, DateTime today)
        {
            return employees
                .Where((employee) => employee.IsBirthday(today))
                .Select(employee => new BirthdayMessage(employee))
                .ToList();
        }

        public static List<BirthdayMessage> FromCsv(string filename, in DateTime today)
        {
            List<Employee> employees = EmployeesCsvFileLoader.Load(filename);
            return BirthdayMessages.Of(employees, today);
        }

        public static List<BirthdayMessage> FromSqlLite(string filename, in DateTime today)
        {
            List<Employee> employees = EmployeesSqlLiteFileLoader.Load(filename);
            return BirthdayMessages.Of(employees, today);
        }
    }
}