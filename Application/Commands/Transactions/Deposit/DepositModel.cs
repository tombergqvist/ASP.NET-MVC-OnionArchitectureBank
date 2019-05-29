using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Transactions.Deposit
{
    public class DepositModel
    {
        [Required]
        [Display(Name = "Account")]
        public int AccountId { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        public string Amount { get; set; }

        public string Symbol { get; set; }
    }
}
