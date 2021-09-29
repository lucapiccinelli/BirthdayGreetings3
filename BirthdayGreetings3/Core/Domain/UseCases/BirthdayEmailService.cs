using System;
using System.Collections.Generic;
using System.Linq;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;
using BirthdayGreetings3.Core.Doors.Email;

namespace BirthdayGreetings3.Core.Domain.UseCases
{
    public class BirthdayEmailService
    {
        private readonly BirthdayMessagesService<BirthdayEmail> _messagesService;
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IBirthdayEmailSender _birthdayMessagesSender;

        public BirthdayEmailService(
            IEmployeesRepository employeesRepository, 
            IBirthdayEmailSender birthdayMessagesSender)
        {
            _messagesService = new BirthdayMessagesService<BirthdayEmail>(employeesRepository, ToBirthdayEmail);
            _employeesRepository = employeesRepository;
            _birthdayMessagesSender = birthdayMessagesSender;
        }

        public void Send(DateTime today)
        {
            var birthdayEmails = _employeesRepository.GetAll()
                .SelectMany(employee => 
                    employee.IsBirthday(today) 
                        ? new List<BirthdayEmail> { ToBirthdayEmail(today, employee) } 
                        : new List<BirthdayEmail>())
                .ToList();

            birthdayEmails.ForEach(email => _birthdayMessagesSender.Send(email));
        }

        private static BirthdayEmail ToBirthdayEmail(DateTime today, Employee employee) => 
            new BirthdayEmail(employee.EmailAddress, new BirthdayMessage(employee.Name, today));
    }
}