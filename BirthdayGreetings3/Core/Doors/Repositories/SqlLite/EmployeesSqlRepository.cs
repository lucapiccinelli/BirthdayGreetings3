using System.Collections.Generic;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core.Doors.Repositories.SqlLite
{
    public class EmployeesSqlRepository : IEmployeesRepository
    {
        private readonly string _filename;

        public EmployeesSqlRepository(string filename)
        {
            _filename = filename;
        }

        public List<Employee> GetAll()
        {
            return EmployeesSqlLiteFileLoader.Load(_filename);
        }
    }
}