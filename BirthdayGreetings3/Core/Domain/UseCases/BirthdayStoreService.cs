using System;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core.Domain.UseCases
{
    public class BirthdayStoreService
    {
        private readonly BirthdayMessagesService _service;
        private readonly IRepository<BirthdayMessage> _birthdayMessagesRepository;

        public BirthdayStoreService(IEmployeesRepository repository, IRepository<BirthdayMessage> birthdayMessagesRepository)
        {
            _birthdayMessagesRepository = birthdayMessagesRepository;
            _service = new BirthdayMessagesService(repository);
        }

        public void StoreBirthdayMessages(in DateTime today)
        {
            _service.CreateMessages(today)
                .ForEach(message => _birthdayMessagesRepository.Save(message));
        }
    }
}