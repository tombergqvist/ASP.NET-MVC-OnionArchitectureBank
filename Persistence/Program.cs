using System;
using System.Linq;

namespace Persistence
{
    class Program
    {
        static void Main(string[] args)
        {
            BankDbContext context = new BankDbContext();
            Console.WriteLine($"Hello {context.Customers.First().Givenname}!");
        }
    }
}
