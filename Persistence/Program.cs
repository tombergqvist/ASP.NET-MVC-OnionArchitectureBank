using System;
using System.Linq;

namespace Persistence
{
    class Program
    {
        static void Main()
        {
            BankDbContext context = new BankDbContext();
            Console.WriteLine($"Hello {context.Customers.First().Givenname}!");
        }
    }
}
