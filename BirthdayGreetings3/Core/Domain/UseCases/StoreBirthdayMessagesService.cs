using System;
using System.Collections.Generic;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Doors.Repositories;

namespace BirthdayGreetings3.Core.Domain.UseCases
{
    public class StoreBirthdayMessagesService
    {
        private readonly BirthdayMessagesService _messagesService;
        private readonly IBirthdayMessageRepository _messagesRepository;

        public StoreBirthdayMessagesService(
            IEmployeesReadSource employeesReadSource,
            IBirthdayMessageRepository messagesRepository)
        {
            _messagesService = new BirthdayMessagesService(employeesReadSource);
            _messagesRepository = messagesRepository;
        }

        public void SaveBirthDaysOf(in DateTime today)
        {
            _messagesService.CreateMessages(today)
                .ForEach(message => _messagesRepository.Save(message));
        }

        public List<BirthdayMessage> GetSavedMessages(DateTime dateTime)
        {
            return _messagesRepository.GetByDate(dateTime);
        }
    }
}