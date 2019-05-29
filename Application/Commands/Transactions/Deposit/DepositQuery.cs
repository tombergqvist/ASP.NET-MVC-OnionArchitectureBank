using Application.Commands.Transactions.NewTransaction;
using Application.Interfaces;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Transactions.Deposit
{
    public class DepositQuery
    {
        public async Task<string> RunAsync(IBankDbContext context, DepositModel model)
        {
            var account = context.Accounts.SingleOrDefault(a => a.AccountId == model.AccountId);

            decimal amount = Decimal.Parse(model.Amount, CultureInfo.InvariantCulture);

            if (account != null && amount > 0)
            {
                account.Balance = account.Balance + amount;
                await context.SaveChangesAsync(new CancellationToken());

                // Deposit transaction
                await new NewTransactionCommand().RunAsync(context, new NewTransactionModel()
                {
                    AccountId = model.AccountId,
                    Amount = amount,
                    Balance = account.Balance,
                    Date = DateTime.Now,
                    Operation = "Credit in Cash",
                    Type = "Credit",
                    Symbol = model.Symbol,
                });

                return "The deposit was successful! ";
            }
            return "Something went wrong!";
        }
    }
}