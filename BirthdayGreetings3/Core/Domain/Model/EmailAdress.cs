using System;
using System.Text.RegularExpressions;

namespace BirthdayGreetings3.Core.Domain.Model
{
    public class EmailAdress
    {
        private const string EmailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

        public string Value { get; }

        private EmailAdress(string value)
        {
            Value = value;
        }

        public static EmailAdress Of(string employeeItem)
        {
            if (!Regex.IsMatch(employeeItem, EmailRegex))
            {
                throw new ArgumentException($"{employeeItem} is not in email format");
            }
            return new EmailAdress(employeeItem);
        }

        protected bool Equals(EmailAdress other)
        {
            return Value == other.Value;
        }

        public override string ToString()
        {
            return $"{nameof(Value)}: {Value}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EmailAdress) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
    }
}