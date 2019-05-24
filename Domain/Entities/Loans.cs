﻿using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Loans
    {
        public int LoanId { get; set; }
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public decimal Payments { get; set; }
        public string Status { get; set; }

        public virtual Accounts Account { get; set; }
    }
}
