using System;
//
namespace BirthdayGreetings3.Core.Domain.Model
{
    public class Employee
    {
        public Employee(string firstName, string lastName, DateTime birthDate, EmailAdress emailAdress)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            EmailAdress = emailAdress;
        }

        private string FirstName { get; }
        private string LastName { get; }
        private DateTime BirthDate { get; }
        private EmailAdress EmailAdress { get; }

        public override string ToString()
        {
            return $"{nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(BirthDate)}: {BirthDate}, {nameof(EmailAdress)}: {EmailAdress}";
        }

        protected bool Equals(Employee other)
        {
            return FirstName == other.FirstName && LastName == other.LastName && BirthDate.Equals(other.BirthDate) && Equals(EmailAdress, other.EmailAdress);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Employee) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, BirthDate, EmailAdress);
        }
    }
}