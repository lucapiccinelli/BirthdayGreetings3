using System;

namespace BirthdayGreetings3.Core.Domain
{
    public class MailCredentials
    {
        public string Username { get; }
        public string Password { get; }

        public MailCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public override string ToString()
        {
            return $"{nameof(Username)}: {Username}, {nameof(Password)}: {Password}";
        }

        protected bool Equals(MailCredentials other)
        {
            return Username == other.Username && Password == other.Password;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MailCredentials) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Username, Password);
        }
    }
}