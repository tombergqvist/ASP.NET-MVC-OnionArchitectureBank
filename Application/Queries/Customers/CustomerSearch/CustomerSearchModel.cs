using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Customers.CustomerSearch
{
    public class CustomerSearchModel
    {
        public bool HasMore { get; set; }
        public List<CustomerSearchDetailsModel> Customers { get; set; }
    }
}
