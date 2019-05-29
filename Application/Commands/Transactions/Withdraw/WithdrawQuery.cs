using Application.Commands.Transactions.NewTransaction;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Transactions.Withdraw
{
    public class WithdrawQuery
    {
        public async Task<string> RunAsync(IBankDbContext context, WithdrawModel model)
        {
            var from = context.Accounts.SingleOrDefault(a => a.AccountId == model.AccountId);

            decimal amount = Decimal.Parse(model.Amount, CultureInfo.InvariantCulture);

            if (from != null && amount > 0)
            {
                var balance = from.Balance - amount;
                if (balance < 0)
                {
                    return "The account does not have enough money. ";
                }
                from.Balance = balance;
                await context.SaveChangesAsync(new CancellationToken());

                // Withdrawal transaction
                await new NewTransactionCommand().RunAsync(context, new NewTransactionModel()
                {
                    AccountId = model.AccountId,
                    Amount = -amount,
                    Balance = from.Balance,
                    Date = DateTime.Now,
                    Operation = "Withdrawal in Cash",
                    Type = "Debit",
                    Symbol = model.Symbol,
                });

                return "The withdrawal was successful! ";
            }
            return "Something went wrong!";
        }
    }
}