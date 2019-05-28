using Application.Queries.Accounts.AccountDetails;
using Application.Queries.Transactions;
using System.Collections.Generic;

namespace Presentation.Models.Cashier
{
    public class AccountViewModel
    {
        public AccountDetailsModel Account { get; set; }
        public TransactionsModel TransactionsList { get; set; }
    }
}
