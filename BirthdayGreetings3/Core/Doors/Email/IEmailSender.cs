using BirthdayGreetings3.Core.Domain;

namespace BirthdayGreetings3.Core.Doors.Email
{
    public interface IEmailSender
    {
        void Send(EMailConfiguration configuration, string @from, string to, string message);
    }
}