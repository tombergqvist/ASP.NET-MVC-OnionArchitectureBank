using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Accounts
    {
        public Accounts()
        {
            Dispositions = new HashSet<Dispositions>();
            Loans = new HashSet<Loans>();
            PermanentOrder = new HashSet<PermanentOrder>();
            Transactions = new HashSet<Transactions>();
        }

        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }

        public virtual ICollection<Dispositions> Dispositions { get; set; }
        public virtual ICollection<Loans> Loans { get; set; }
        public virtual ICollection<PermanentOrder> PermanentOrder { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
