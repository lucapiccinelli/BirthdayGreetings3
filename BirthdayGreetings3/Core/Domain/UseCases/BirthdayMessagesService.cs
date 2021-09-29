using System;
using System.Collections.Generic;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core.Domain.UseCases
{
    public class BirthdayMessagesService<T>
    {
        private readonly IEmployeesRepository _repository;
        private readonly Func<DateTime, Employee, T> _employTranform;
        
        public BirthdayMessagesService(IEmployeesRepository repository, Func<DateTime, Employee, T> employTranform)
        {
            _repository = repository;
            _employTranform = employTranform;
        }

        public List<T> CreateMessages(in DateTime today)
        {
            List<Employee> employees = _repository.GetAll();
            return EmployeeTransform<T>.Of(employees, today, _employTranform);
        }
    }
}