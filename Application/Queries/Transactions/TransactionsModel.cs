using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Transactions
{
    public class TransactionsModel
    {
        public bool HasMore { get; set; }
        public List<TransactionDetailsModel> Transactions { get; set; }
    }
}
