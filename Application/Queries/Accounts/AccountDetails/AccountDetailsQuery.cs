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

            var transactions = new List<TransactionsModel>();
            foreach(var transaction in context.Transactions.Where(t => t.AccountId == account.AccountId).ToList())
            {
                transactions.Add(new TransactionsModel()
                {
                    AccountId = transaction.AccountId,
                    Amount = transaction.Amount,
                    Balance = transaction.Balance,
                    Bank = transaction.Bank,
                    Date = transaction.Date,
                    Operation = transaction.Operation,
                    Symbol = transaction.Symbol,
                    TransactionId = transaction.TransactionId,
                    Type = transaction.Type,
                    Account = transaction.Account
                });
            }
            return new AccountDetailsModel()
            {
                AccountId = account.AccountId,
                Balance = account.Balance,
                Created = account.Created,
                Frequency = account.Frequency,
                Transactions = transactions.OrderByDescending(t => t.Date).ToList()
            };
        }
    }
}
