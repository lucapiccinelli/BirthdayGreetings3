using System;
using System.Collections.Generic;
using BirthdayGreetings3.Core.Domain.Doors;
using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core.Domain.UseCases
{
    public class BirthdayMessagesService
    {
        private readonly IEmployeesReadSource _readSource;

        public BirthdayMessagesService(IEmployeesReadSource readSource)
        {
            _readSource = readSource;
        }

        public List<BirthdayMessage> CreateMessages(in DateTime today)
        {
            List<Employee> employees = _readSource.GetAll();
            return BirthdayMessages.Of(employees, today);
        }
    }
}