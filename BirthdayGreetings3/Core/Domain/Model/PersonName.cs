using System;

namespace BirthdayGreetings3.Core.Domain.Model
{
    public class PersonName
    {
        public string Firstname { get; }
        public string Lastname { get; }

        public PersonName(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        protected bool Equals(PersonName other)
        {
            return Firstname == other.Firstname && Lastname == other.Lastname;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PersonName) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Firstname, Lastname);
        }

        public override string ToString()
        {
            return $"{nameof(Firstname)}: {Firstname}, {nameof(Lastname)}: {Lastname}";
        }
    }
}