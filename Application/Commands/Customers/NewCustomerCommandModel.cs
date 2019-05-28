using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Customers
{
    public class NewCustomerCommandModel
    {
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "Gender")]
        [StringLength(6)]
        public string Gender { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Given name")]
        public string Givenname { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Street")]
        [StringLength(100)]
        public string Streetaddress { get; set; }

        [Required]
        [Display(Name = "City")]
        [StringLength(100)]
        public string City { get; set; }

        [Required]
        [Display(Name = "Zip")]
        [StringLength(15)]
        public string Zipcode { get; set; }

        [Required]
        [Display(Name = "Country")]
        [StringLength(100)]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Country code")]
        [StringLength(2)]
        public string CountryCode { get; set; }

        [Display(Name = "Birthday")]
        public DateTime? Birthday { get; set; }

        [Display(Name = "National id")]
        [StringLength(20)]
        public string NationalId { get; set; }

        [Display(Name = "Telephone country code")]
        [StringLength(10)]
        public string Telephonecountrycode { get; set; }

        [Display(Name = "Telephone")]
        [StringLength(25)]
        public string Telephonenumber { get; set; }

        [Display(Name = "Email")]
        [StringLength(100)]
        [EmailAddress]
        public string Emailaddress { get; set; }
    }
}