using Application.Queries.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models.Cashier
{
    public class TransactionsViewModel
    {
        public int Id { get; set; }
        public int Page { get; set; }
        public bool HasMore { get; set; }
        public List<TransactionDetailsModel> Transactions { get; set; }
    }
}
