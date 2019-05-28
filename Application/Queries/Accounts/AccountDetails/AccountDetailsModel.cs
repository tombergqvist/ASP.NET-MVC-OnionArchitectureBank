using System;
using System.Collections.Generic;

namespace Application.Queries.Accounts.AccountDetails
{
    public class AccountDetailsModel
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
    }
}
