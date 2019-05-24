using Application.Interfaces;
using System.Linq;

namespace Application.Queries.Statistics
{
    public class StatisticsQuery
    {
        public StatisticsModel GetStatistics(IBankDbContext context)
        {
            return new StatisticsModel()
            {
                AccountCount = context.Accounts.Count(),
                CustomersCount = context.Customers.Count(),
                TotalBalance = context.Accounts.Sum(a => a.Balance)
            };
        }
    }
}
