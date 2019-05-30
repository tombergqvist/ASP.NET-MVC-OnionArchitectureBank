using Application.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Application.Queries.Customers.CustomerSearch
{
    public class CustomerSearchQuery
    {
        public CustomerSearchModel Get(IBankDbContext context, string name, string city, int page)
        {
            int customersPerPage = 50;
            var customers = context.Customers
                .Where(c => ((c.Givenname + " " + c.Surname).Contains(name ?? "")) && c.City.Contains(city ?? ""))
                .Skip(customersPerPage * (page-1))
                .Take(customersPerPage)
                .ToList();

            var customerList = new List<CustomerSearchDetailsModel>();
            foreach (var customer in customers)
            {
                customerList.Add(new CustomerSearchDetailsModel()
                {
                    CustomerId = customer.CustomerId,
                    Givenname = customer.Givenname,
                    Surname = customer.Surname,
                    Streetaddress = customer.Streetaddress,
                    City = customer.City,
                    Zipcode = customer.Zipcode,
                    Country = customer.Country
                });
            }
            return new CustomerSearchModel() {
                Customers = customerList
            };
        }
    }
}
