using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Customers.CustomerSearch
{
    public class CustomerSearchDetailsModel
    {
        public int CustomerId { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
    }
}
