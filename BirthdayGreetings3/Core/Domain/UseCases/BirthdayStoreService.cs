using System;
using System.Collections.Generic;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core.Domain.UseCases
{
    public class BirthdayStoreService
    {
        private readonly IEmployeesRepository _repository;
        private readonly IRepository<BirthdayMessage> _birthdayMessagesRepository;

        public BirthdayStoreService(IEmployeesRepository repository, IRepository<BirthdayMessage> birthdayMessagesRepository)
        {
            _repository = repository;
            _birthdayMessagesRepository = birthdayMessagesRepository;
        }

        public void StoreBirthdayMessages(in DateTime today)
        {
            List<Employee> employees = _repository.GetAll();
            BirthdayMessages.Of(employees, today)
                .ForEach(message => _birthdayMessagesRepository.Save(message));
        }
    }
}