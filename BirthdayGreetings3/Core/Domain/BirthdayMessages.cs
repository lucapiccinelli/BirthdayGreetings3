using System;
using System.Collections.Generic;
using System.Linq;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core.Domain
{
    public static class BirthdayMessages
    {
        public static List<BirthdayMessage> Of(List<Employee> employees, DateTime today)
        {
            return employees
                .Where((employee) => employee.IsBirthday(today))
                .Select(employee => new BirthdayMessage(employee, today))
                .ToList();
        }
    }
}