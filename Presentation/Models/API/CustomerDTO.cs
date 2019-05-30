using Application.Queries.Accounts.CustomerAccounts;
using Application.Queries.Customers.CustomerDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models.API
{
    public class CustomerDTO
    {
        public CustomerDetailsModel Customer { get; set; }
        public CustomerAccountsModel Accounts { get; set; }
    }
}
