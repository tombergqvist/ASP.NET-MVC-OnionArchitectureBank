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
                CustomerId = customer.CustomerId,
                Givenname = customer.Givenname,
                Surname = customer.Surname,
                Gender = customer.Gender,
                Streetaddress = customer.Streetaddress,
                City = customer.City,
                Zipcode = customer.Zipcode,
                Country = customer.Country,
                CountryCode = customer.CountryCode,
                Birthday = customer.Birthday,
                NationalId = customer.NationalId,
                Telephonecountrycode = customer.Telephonecountrycode,
                Telephonenumber = customer.Telephonenumber,
                Emailaddress = customer.Emailaddress
            };
        }
    }
}
