using Application.Commands.Transactions.NewTransaction;
using Application.Interfaces;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Transactions.Interest
{
    public class InterestQuery
    {
        public async Task<string> RunAsync(IBankDbContext context, InterestModel model, DateTime latestDate, decimal interest)
        {
            var account = context.Accounts.SingleOrDefault(a => a.AccountId == model.AccountId);

            if (account != null)
            {
                int days = (int)(DateTime.Now - latestDate).TotalDays;

                var amount = account.Balance * (interest / 365) * days;
                account.Balance = account.Balance + amount;

                await context.SaveChangesAsync(new CancellationToken());

                // Deposit transaction
                await new NewTransactionCommand().RunAsync(context, new NewTransactionModel()
                {
                    AccountId = model.AccountId,
                    Amount = amount,
                    Balance = account.Balance,
                    Date = DateTime.Now,
                    Type = "Credit",
                    Symbol = "interest credited",
                    Operation = "Interest"
                });

                return "The interest was successfully added! ";
            }
            return "Something went wrong!";
        }
    }
}
