using System;
using System.Collections.Generic;

namespace BirthdayGreetings3.Core.Exceptions
{
    public class EmployeesLoadingException : Exception
    {
        public int ExceptionsNumber { get; set; }
        public List<ParsingError> Errors { get; set; }
    }
}