using System;
using System.Collections.Generic;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core.Domain.UseCases
{
    public class BirthdayMessagesService
    {
        private readonly IEmployeesRepository _repository;

        public BirthdayMessagesService(IEmployeesRepository repository)
        {
            _repository = repository;
        }

        public List<BirthdayMessage> CreateMessages(in DateTime today)
        {
            List<Employee> employees = _repository.GetAll();
            return BirthdayMessages.Of(employees, today);
        }
    }
}