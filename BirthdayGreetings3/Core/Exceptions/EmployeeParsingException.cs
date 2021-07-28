using System;

namespace BirthdayGreetings3.Core.Exceptions
{
    public class EmployeeParsingException: Exception
    {
        public EmployeeParsingException(string message, Exception exception = null): base(message, exception)
        {
        }
    }
}