using System;
using BirthdayGreetings3.Core;

namespace BirthdayGreetings3
{
    class Program
    {
        static void Main(string[] args)
        {
            BirthdayMessages.FromCsv(args[1], DateTime.Now);
        }
    }
}
