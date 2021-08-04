using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Exceptions;

namespace BirthdayGreetings3.Core
{
    public static class EmployeesFileLoader
    {
        public static List<Employee> Load(string filename)
        {
            var fileLines = File.ReadAllLines(filename);
            IEnumerable<string> employeesLines = SkipHeader(fileLines);
            List<EmployeeParsingException> parsingErrors = new List<EmployeeParsingException>();


            List<Employee> list = new List<Employee>();
            foreach (var employeeLine in employeesLines)
            {
                try
                {
                    Employee employee = EmployeeParser.ToEmployee(employeeLine);
                    list.Add(employee);
                }
                catch (EmployeeParsingException e)
                {
                    parsingErrors.Add(e);
                }
            }
            return list;

        }

        private static IEnumerable<string> SkipHeader(string[] fileLines)
        {
            return fileLines.Skip(1);
        }
    }
}