using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Commands.Transactions.NewTransaction
{
    public class NewTransactionModel
    {
        [Display(Name = "Transaction id")]
        public int TransactionId { get; set; }

        [Required]
        [Display(Name = "Account id")]
        public int AccountId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Required]
        [StringLength(50)]
        public string Operation { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public string Symbol { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
    }
}
