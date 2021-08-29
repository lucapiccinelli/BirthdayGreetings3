using System;
using System.Collections.Generic;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core.Domain.UseCases
{
    public class StoreBirthdayMessagesService
    {
        private readonly BirthdayMessagesService _messagesService;
        private readonly IRepository<BirthdayMessage> _inMemoryMessagesRepository;

        public StoreBirthdayMessagesService(
            IEmployeesReadSource employeesReadSource, 
            IRepository<BirthdayMessage> messagesRepository)
        {
            _messagesService = new BirthdayMessagesService(employeesReadSource);
            _inMemoryMessagesRepository = messagesRepository;
        }

        public void SaveBirthDaysOf(in DateTime today)
        {
            _messagesService.CreateMessages(today)
                .ForEach(message => _inMemoryMessagesRepository.Save(message));
        }

        public List<BirthdayMessage> GetSavedMessages()
        {
            return _inMemoryMessagesRepository.GetAll();
        }
    }
}