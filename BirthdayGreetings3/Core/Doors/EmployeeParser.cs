using System;
using System.Globalization;
using System.Linq;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Exceptions;

namespace BirthdayGreetings3.Core.Doors
{
    public class EmployeeParser
    {
        public static Employee ToEmployee(string employeeLine)
        {
            string[] employeeItems = employeeLine
                .Split(",")
                .Select(s => s.Trim())
                .ToArray();

            if(employeeItems.Length < 4) throw new EmployeeParsingException("Dovresti avere 4 elementi");

            var firstName = employeeItems[1];
            var lastName = employeeItems[0];
            var birthDate = ParseExact(employeeItems, employeeItems[2]);
            var emailAdress = ParseEmailAdress(employeeItems);

            return new Employee(
                firstName,
                lastName, birthDate,
                emailAdress);
        }

        private static EmailAddress ParseEmailAdress(string[] employeeItems)
        {
            try
            {
                return EmailAddress.Of(employeeItems[3]);
            }
            catch (ArgumentException e)
            {
                throw new EmployeeParsingException(e.Message, e);
            }
        }

        private static DateTime ParseExact(string[] employeeItems, string employeeItem)
        {
            try
            {
                return DateTime.ParseExact(employeeItem, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                throw new EmployeeParsingException(e.Message, e);
            }
        }
    }
}