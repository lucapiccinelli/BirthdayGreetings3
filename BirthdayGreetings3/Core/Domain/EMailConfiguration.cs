using System;

namespace BirthdayGreetings3.Core.Domain
{
    public class EMailConfiguration
    {
        public string Smtp { get; }
        public int Port { get; }
        public bool Secured { get; }
        public MailCredentials MailCredentials { get; }

        public EMailConfiguration(string smtp, int port, bool secured, MailCredentials mailCredentials)
        {
            Smtp = smtp;
            Port = port;
            Secured = secured;
            MailCredentials = mailCredentials;
        }

        public override string ToString()
        {
            return $"{nameof(Smtp)}: {Smtp}, {nameof(Port)}: {Port}, {nameof(Secured)}: {Secured}, {nameof(MailCredentials)}: {MailCredentials}";
        }

        protected bool Equals(EMailConfiguration other)
        {
            return Smtp == other.Smtp && Port == other.Port && Secured == other.Secured && Equals(MailCredentials, other.MailCredentials);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EMailConfiguration) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Smtp, Port, Secured, MailCredentials);
        }
    }
}