using Application.Queries.Accounts.CustomerAccounts;
using Application.Queries.Customers.CustomerDetails;

namespace Presentation.Models.Cashier
{
    public class CustomerViewModel
    {
        public CustomerDetailsModel Details { get; set; }
        public CustomerAccountsModel AccountsModel { get; set; }
    }
}
