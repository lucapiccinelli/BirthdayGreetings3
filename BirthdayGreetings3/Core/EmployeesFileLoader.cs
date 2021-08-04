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
            var parsingErrors = new List<ParsingError>();
            List<Employee> list = new List<Employee>();

            int lineNumber = 1;
            foreach (var employeeLine in employeesLines)
            {
                try
                {
                    Employee employee = EmployeeParser.ToEmployee(employeeLine);
                    list.Add(employee);
                }
                catch (EmployeeParsingException e)
                {
	                var parsingError = new ParsingError(lineNumber,e);
                    parsingErrors.Add(parsingError);
                }

                lineNumber++;
            }

            if (parsingErrors.Count > 0)
            {
	            throw new EmployeesLoadingException(parsingErrors);
            }


            return list;

        }

        private static IEnumerable<string> SkipHeader(string[] fileLines)
        {
            return fileLines.Skip(1);
        }
    }
}