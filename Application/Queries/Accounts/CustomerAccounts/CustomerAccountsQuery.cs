using Application.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Application.Queries.Accounts.CustomerAccounts
{
    public class CustomerAccountsQuery
    {
        public CustomerAccountsModel Get(IBankDbContext context, int customerId)
        {
            var customer = context.Customers.SingleOrDefault(c => c.CustomerId == customerId);
            if(customer != null)
            {
                var dispositions = context.Dispositions.Where(d => d.Customer == customer).ToList();
                var listOfAccounts = new List<CustomerAccountDetailsModel>();
                foreach (var disposition in dispositions)
                {
                    var account = context.Accounts.SingleOrDefault(a => a.AccountId == disposition.AccountId);

                    if (account != null)
                    {
                        listOfAccounts.Add(new CustomerAccountDetailsModel()
                        {
                            AccountId = account.AccountId,
                            Balance = account.Balance,
                            Created = account.Created,
                            Frequency = account.Frequency,
                            Type = disposition.Type
                        });
                    }
                }
                return new CustomerAccountsModel()
                {
                    TotalBalance = listOfAccounts.Sum(a => a.Balance),
                    Accounts = listOfAccounts
                };
            }
            else
            {
                return null;
            }
            
        }
    }
}