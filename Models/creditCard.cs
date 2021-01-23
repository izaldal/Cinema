using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class creditCard
    {
        [Required]
        [RegularExpression("^[0-9]{16}$",ErrorMessage = "cardnumber must contain 16 digits")]

        public string cardnumber { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "card holder name must be between 1-50 letters")]

        public string cardholder { get; set; }

        [Required]
        public DateTime endDate { get; set; }

        [Required]
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "cardnumber must contain 16 digits")]

        public string cvv { get; set; }

    }
}