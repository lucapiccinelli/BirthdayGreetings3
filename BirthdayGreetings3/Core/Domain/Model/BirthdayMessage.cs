using System;

namespace BirthdayGreetings3.Core.Domain.Model
{
    public class BirthdayMessage
    {
        private readonly Employee _employee;
        private readonly DateTime _dateTime;

        public BirthdayMessage(in Employee employee, DateTime dateTime)
        {
            _employee = employee;
            _dateTime = dateTime;
        }

        protected bool Equals(BirthdayMessage other)
        {
            return Equals(_employee, other._employee);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BirthdayMessage) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_employee, _dateTime);
        }

        public override string ToString()
        {
            return $"{nameof(_employee)}: {_employee}, {nameof(_dateTime)}: {_dateTime}";
        }
    }
}