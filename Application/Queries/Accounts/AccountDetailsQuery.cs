using Application.Interfaces;

namespace Application.Queries.Accounts
{
    class AccountDetailsQuery
    {
        public AccountDetailsModel GetAccount(IBankDbContext context, int? id)
        {
            return new AccountDetailsModel()
            {

            };
        }
    }
}
