using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Accounts.CustomerAccounts
{
    public class CustomerAccountsModel
    {
        public decimal TotalBalance { get; set; }
        public List<AccountDetailsModel> Accounts { get; set; }
    }
}
