using Application.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Application.Queries.Accounts
{
    public class CustomerAccountsQuery
    {
        public List<AccountDetailsModel> GetCustomerAccounts(IBankDbContext context, int customerId)
        {
            var customer = context.Customers.Single(c => c.CustomerId == customerId);
            var accounts = context.Accounts.Where(a => a.Dispositions == customer.Dispositions);

            List<AccountDetailsModel> listOfAccounts = new List<AccountDetailsModel>();

            foreach(var account in accounts)
            {
                listOfAccounts.Add(new AccountDetailsModel()
                {
                    AccountId = account.AccountId,
                    Balance = account.Balance,
                    Created = account.Created,
                    Frequency = account.Frequency
                });
            }
            return listOfAccounts;
        }
    }
}