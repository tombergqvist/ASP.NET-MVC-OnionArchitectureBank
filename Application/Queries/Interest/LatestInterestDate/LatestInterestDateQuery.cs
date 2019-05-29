using Application.Interfaces;
using System;
using System.Linq;

namespace Application.Queries.Interest.LatestInterestDate
{
    public class LatestInterestDateQuery
    {
        public DateTime Get(IBankDbContext context, int id)
        {
            var date = context.Transactions
                   .Where(t => t.AccountId == id && t.Symbol == "interest credited");

            if (date.Count() == 0)
            {
                return context.Accounts.Single(a => a.AccountId == id).Created;
            }
            return date.Max(t => t.Date);
        }
    }
}
