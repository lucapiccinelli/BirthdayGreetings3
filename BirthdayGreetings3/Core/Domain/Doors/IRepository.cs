using System.Collections.Generic;

namespace BirthdayGreetings3.Core.Domain.Doors
{
    public interface IRepository<T>
    {
        void Save(T birthdayMessages);
        IEnumerable<T> GetAll();
    }
}