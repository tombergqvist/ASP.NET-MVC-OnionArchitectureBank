using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Transactions.Withdraw
{
    public class WithdrawModel
    {
        [Required]
        [Display(Name = "From account")]
        public int AccountId { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        public string Amount { get; set; }

        public string Symbol { get; set; }
    }
}
