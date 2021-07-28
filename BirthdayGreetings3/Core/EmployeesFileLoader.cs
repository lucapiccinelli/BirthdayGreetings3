using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core
{
    public static class EmployeesFileLoader
    {
        public static List<Employee> Load(string filename)
        {
            var fileLines = File.ReadAllLines(filename);
            IEnumerable<string> employeesLines = SkipHeader(fileLines);

            return employeesLines
                .Select(EmployeeParser.ToEmployee)
                .ToList();
        }

        private static IEnumerable<string> SkipHeader(string[] fileLines)
        {
            return fileLines.Skip(1);
        }
    }
}