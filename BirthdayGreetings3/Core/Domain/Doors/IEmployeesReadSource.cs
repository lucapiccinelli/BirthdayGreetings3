using System.Collections.Generic;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core.Domain.Doors
{
    public interface IEmployeesReadSource
    {
        List<Employee> GetAll();
    }
}