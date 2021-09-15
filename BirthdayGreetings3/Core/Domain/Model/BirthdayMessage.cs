using System;

namespace BirthdayGreetings3.Core.Domain.Model
{
    public class BirthdayMessage
    {
        public PersonName Name { get; }
        public DateTime Date { get; }

        public BirthdayMessage(PersonName name, in DateTime date)
        {
            Name = name;
            Date = date;
        }

        protected bool Equals(BirthdayMessage other)
        {
            return Equals(Name, other.Name) && Date.Equals(other.Date);
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
            return HashCode.Combine(Name, Date);
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Date)}: {Date}";
        }
    }
}