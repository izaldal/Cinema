//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cinema.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class admin
    {
        [Key]
        [Required]
        [StringLength(50,MinimumLength =4,ErrorMessage ="user name must be between 4-50 letters")]
        public string userName { get; set; }
        public string email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "firts name must be between 4-50 letters")]
        public string firstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "last name must be between 4-50 letters")]
        public string lastName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "password must be between 6-50 letters")]
        public string password { get; set; }
        public System.DateTime yob { get; set; }
    }
}
