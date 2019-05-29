using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.Commands.Transactions.Transfer
{
    public class TransferModel
    {
        [Required]
        [Display(Name = "From account")]
        public int AccountId { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        public string Amount { get; set; }

        public string Symbol { get; set; }

        [Required]
        [Display(Name = "To account")]
        public int ToAccount { get; set; }
    }
}
