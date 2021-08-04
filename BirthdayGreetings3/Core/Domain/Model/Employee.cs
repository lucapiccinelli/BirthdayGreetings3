using System;
//
namespace BirthdayGreetings3.Core.Domain.Model
{
    public class Employee
    {
        private readonly DateTime _leapYearBirthDate;
        public EmailAddress EmailAddress { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime BirthDate { get; }

        public Employee(string firstname, string lastname, DateTime dateOfBirth, EmailAddress email)
        {
            EmailAddress = email;
            FirstName = firstname;
            LastName = lastname;
            BirthDate = dateOfBirth;
            _leapYearBirthDate = dateOfBirth;


            if (BirthDate.Day == 29 && BirthDate.Month == 2)
            {
                _leapYearBirthDate = new DateTime(dateOfBirth.Year, dateOfBirth.Month, 28);
            }
        }

        protected bool Equals(Employee other)
        {
            return _leapYearBirthDate.Equals(other._leapYearBirthDate) && Equals(EmailAddress, other.EmailAddress) && FirstName == other.FirstName && LastName == other.LastName && BirthDate.Equals(other.BirthDate);
        }

        public override string ToString()
        {
            return $"{nameof(_leapYearBirthDate)}: {_leapYearBirthDate}, {nameof(EmailAddress)}: {EmailAddress}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(BirthDate)}: {BirthDate}";
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
            return HashCode.Combine(_leapYearBirthDate, EmailAddress, FirstName, LastName, BirthDate);
        }

        public bool IsBirthday(DateTime today)
        {
            if (today.Day != 28 || today.Month != 2 || DateTime.IsLeapYear(today.Year))
            {
                return MatchDate(today, BirthDate);
            }

            return MatchDate(today, _leapYearBirthDate);
        }

        private bool MatchDate(DateTime today, DateTime dateOfBirth) =>
            dateOfBirth.Date.Month == today.Date.Month &&
            dateOfBirth.Date.Day == today.Date.Day &&
            dateOfBirth.Date.Year <= today.Year;
    }
}