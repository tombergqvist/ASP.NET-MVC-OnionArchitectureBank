using Application.Commands.Transactions.NewTransaction;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Transactions.Transfer
{
    public class TransferCommand
    {
        public async Task<string> RunAsync(IBankDbContext context, TransferModel model)
        {
            var from = context.Accounts.SingleOrDefault(a => a.AccountId == model.AccountId);
            var to = context.Accounts.SingleOrDefault(a => a.AccountId == model.ToAccount);

            decimal amount = Decimal.Parse(model.Amount, CultureInfo.InvariantCulture);

            if(from != null && to != null && amount > 0)
            {
                var balance = from.Balance - amount;
                if(balance < 0)
                {
                    return "The account does not have enough money. ";
                }
                from.Balance = balance;
                to.Balance = to.Balance + amount;
                await context.SaveChangesAsync(new CancellationToken());

                // From transaction
                await new NewTransactionCommand().RunAsync(context, new NewTransactionModel()
                {
                    AccountId = model.AccountId,
                    Account = model.ToAccount.ToString(),
                    Amount = -amount,
                    Balance = from.Balance,
                    Date = DateTime.Now,
                    Operation = "Transfer to another account",
                    Type = "Debit",
                    Symbol = model.Symbol,
                });

                // To transaction
                await new NewTransactionCommand().RunAsync(context, new NewTransactionModel()
                {
                    AccountId = model.ToAccount,
                    Account = model.AccountId.ToString(),
                    Amount = amount,
                    Balance = to.Balance,
                    Date = DateTime.Now,
                    Operation = "Transfer from another account",
                    Type = "Credit",
                    Symbol = model.Symbol,
                });
                return "The transfer was successful! ";
            }
            return "Something went wrong!";
        }
    }
}
