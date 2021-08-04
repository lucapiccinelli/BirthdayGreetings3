using BirthdayGreetings3.Core.Domain.Model;

namespace BirthdayGreetings3.Core
{
    public class BirthdayMessage
    {
        private readonly Employee _employee;

        public BirthdayMessage(in Employee employee)
        {
            _employee = employee;
        }

        protected bool Equals(BirthdayMessage other)
        {
            return Equals(_employee, other._employee);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BirthdayMessage) obj);
        }

        public override int GetHashCode()
        {
            return (_employee != null ? _employee.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return $"{nameof(_employee)}: {_employee}";
        }
    }
}