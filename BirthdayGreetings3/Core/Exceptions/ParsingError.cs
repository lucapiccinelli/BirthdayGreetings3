namespace BirthdayGreetings3.Core.Exceptions
{
    public class ParsingError
    {
	    private readonly EmployeeParsingException _employeeParsingException;

	    public ParsingError(int lineNumber, EmployeeParsingException employeeParsingException)
	    {
		    _employeeParsingException = employeeParsingException;
		    LineNumber = lineNumber;
	    }

	    public int LineNumber { get; }
    }
}