using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Queries.Accounts.AccountDetails
{
    public class AccountDetailsQuery
    {
        public AccountDetailsModel Get(IBankDbContext context, int id)
        {
            var account = context.Accounts.SingleOrDefault(a => a.AccountId == id);
            if(account == null) return null;

            return new AccountDetailsModel()
            {
                AccountId = account.AccountId,
                Balance = account.Balance,
                Created = account.Created,
                Frequency = account.Frequency
            };
        }
    }
}