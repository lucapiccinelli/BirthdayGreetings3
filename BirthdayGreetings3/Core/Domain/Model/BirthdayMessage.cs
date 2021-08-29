using System;

namespace BirthdayGreetings3.Core.Domain.Model
{
    public class BirthdayMessage
    {
        public Employee Employee { get; }
        public DateTime Date { get; }

        public BirthdayMessage(in Employee employee, DateTime dateTime)
        {
            Employee = employee;
            Date = dateTime;
        }

        protected bool Equals(BirthdayMessage other)
        {
            return Equals(Employee, other.Employee);
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
            return HashCode.Combine(Employee, Date);
        }

        public override string ToString()
        {
            return $"{nameof(Employee)}: {Employee}, {nameof(Date)}: {Date}";
        }
    }
}