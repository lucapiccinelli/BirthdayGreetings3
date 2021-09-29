using System;
using System.Collections.Generic;
using System.Linq;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core.Domain
{
    public static class BirthdayMessages
    {
        public static List<BirthdayMessage> Of(List<Employee> employees, DateTime today) => 
            EmployeeTransform<BirthdayMessage>
                .Of(employees, today, (time, employee) => 
                    new BirthdayMessage(employee.Name, today));
    }

    public static class EmployeeTransform<T>
    {
        public static List<T> Of(List<Employee> employees, DateTime today, Func<DateTime, Employee, T> adaptFn) =>
            employees
                .Where(employee => employee.IsBirthday(today))
                .Select(employee => adaptFn(today, employee))
                .ToList();
    }
}