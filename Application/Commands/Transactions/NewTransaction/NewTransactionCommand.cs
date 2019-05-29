using Application.Interfaces;
using Application.Queries.Transactions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Transactions.NewTransaction
{
    public class NewTransactionCommand
    {
        public async Task RunAsync(IBankDbContext context, NewTransactionModel model)
        {
            context.Transactions.Add(new Transaction()
            {
                AccountId = model.AccountId,
                Account = model.Account,
                Amount = model.Amount,
                Balance = model.Balance,
                Bank = model.Bank,
                Date = model.Date,
                Operation = model.Operation,
                Symbol = model.Symbol,
                Type = model.Type
            });
            await context.SaveChangesAsync(new CancellationToken());
        }
    }
}