using Application.Interfaces;
using System.Linq;

namespace Application.Queries.Customers
{
    public class CustomerDetailsQuery
    {
        public CustomerDetailsModel GetCustomer(IBankDbContext context, int id)
        {
            var customer = context.Customers.SingleOrDefault(c => c.CustomerId == id);
            return new CustomerDetailsModel()
            {
                Givenname = customer.Givenname,
                Surname = customer.Surname
            };
        }
    }
}
