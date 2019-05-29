using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Commands.Transactions.Interest
{
    public class InterestModel
    {
        [Required]
        [Display(Name = "Account")]
        public int AccountId { get; set; }
    }
}