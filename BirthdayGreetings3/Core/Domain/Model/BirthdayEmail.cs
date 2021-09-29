using System;

namespace BirthdayGreetings3.Core.Domain.Model
{
    public class BirthdayEmail
    {
        public EmailAddress Recipient { get; }
        public BirthdayMessage Message { get; }

        public BirthdayEmail(EmailAddress recipient, BirthdayMessage message)
        {
            Recipient = recipient;
            Message = message;
        }

        protected bool Equals(BirthdayEmail other)
        {
            return Equals(Recipient, other.Recipient) && Equals(Message, other.Message);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BirthdayEmail) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Recipient, Message);
        }

        public override string ToString()
        {
            return $"{nameof(Recipient)}: {Recipient}, {nameof(Message)}: {Message}";
        }

        public string ToMessage()
        {
            return $"Happy Birthday dear {Message.Name.Firstname} {Message.Name.Lastname}";
        }
    }
}