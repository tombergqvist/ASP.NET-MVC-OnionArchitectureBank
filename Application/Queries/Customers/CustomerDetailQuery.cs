using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Queries.Customers
{
    public class CustomerDetailQuery
    {
        public CustomerDetailModel GetCustomer(IBankDbContext context, int id)
        {
            var customer = context.Customers.SingleOrDefault(c => c.CustomerId == id);
            return new CustomerDetailModel()
            {
                Givenname = customer.Givenname,
                Surname = customer.Surname
            };
        }
    }
}
