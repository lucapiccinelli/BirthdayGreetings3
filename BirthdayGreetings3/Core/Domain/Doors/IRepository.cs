using System.Collections.Generic;

namespace BirthdayGreetings3.Core.Domain.Doors
{
    public interface IRepository<T>
    {
        void Save(T message);
        List<T> GetAll();
    }
}