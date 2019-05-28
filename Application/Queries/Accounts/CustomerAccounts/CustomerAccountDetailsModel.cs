using System;

namespace Application.Queries.Accounts.CustomerAccounts
{
    public class CustomerAccountDetailsModel
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
    }
}
