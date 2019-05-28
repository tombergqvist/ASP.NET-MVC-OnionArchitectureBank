using Application.Queries.Customers.CustomerSearch;
using System.Collections.Generic;

namespace Presentation.Models.Cashier
{
    public class CustomerSearchViewModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public int Page { get; set; }
        public List<CustomerSearchDetailsModel> Customers { get; set; }
        public bool HasMore { get; set; }
    }
}