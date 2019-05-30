using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Queries.Transactions
{
    public class TransactionsQuery
    {
        public TransactionsModel Get(IBankDbContext context, int id, int limit, int page)
        {
            int transactionsPerPage = limit;

            var transactions = new List<TransactionDetailsModel>();
            foreach (var transaction in context.Transactions
                .Where(t => t.AccountId == id)
                .OrderByDescending(t => t.Date)
                .Skip(transactionsPerPage * page)
                .Take(transactionsPerPage)
                .ToList())
            {
                transactions.Add(new TransactionDetailsModel()
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
            var resultsOnNextPage = context.Transactions
                .Where(t => t.AccountId == id)
                .Skip(transactionsPerPage * (page + 1))
                .Take(1).Count();

            return new TransactionsModel()
            {
                Transactions = transactions,
                HasMore = resultsOnNextPage == 0 ? false : true
            };
        }
    }
}
