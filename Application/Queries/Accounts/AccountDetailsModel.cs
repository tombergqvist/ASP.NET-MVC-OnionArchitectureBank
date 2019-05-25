﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Accounts
{
    public class AccountDetailsModel
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
    }
}
