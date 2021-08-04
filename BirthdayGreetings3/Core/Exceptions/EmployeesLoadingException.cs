using System;
using System.Collections.Generic;

namespace BirthdayGreetings3.Core.Exceptions
{
    public class EmployeesLoadingException : Exception
    {

	    public EmployeesLoadingException(List<ParsingError> parsingErrors)
	    {
		    Errors = parsingErrors;
	    }

	    public int ExceptionsNumber => Errors.Count;
        public List<ParsingError> Errors { get;  }


    }
}