using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Queries.Customers.CustomerSearchCountPages
{
    public class CustomerSearchCountPagesQuery
    {
        public int Get(IBankDbContext context, string name, string city)
        {
            int customersPerPage = 50;
            var customers = context.Customers
                .Where(c => ((c.Givenname + " " + c.Surname).Contains(name ?? "")) && c.City.Contains(city ?? ""))
                .Count();

            int pages = customers / customersPerPage;
            return customers % customersPerPage == 0? pages : pages+1;
        }
    }
}