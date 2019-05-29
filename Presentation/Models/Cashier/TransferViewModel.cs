using Application.Commands.Transactions.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models.Cashier
{
    public class TransferViewModel
    {
        public TransferModel Transfer { get; set; }
        public string Message { get; set; }
    }
}
