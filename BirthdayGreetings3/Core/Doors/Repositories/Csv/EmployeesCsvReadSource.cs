using System.Collections.Generic;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core.Doors.Repositories.Csv
{
    public class EmployeesCsvReadSource : IEmployeesReadSource
    {
        private readonly string _filename;

        public EmployeesCsvReadSource(string filename)
        {
            _filename = filename;
        }

        public List<Employee> GetAll()
        {
            return EmployeesCsvFileLoader.Load(_filename);
        }
    }
}