using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core.Doors.Email
{
    public interface IBirthdayEmailSender
    {
        void Send(BirthdayEmail email);
    }
}