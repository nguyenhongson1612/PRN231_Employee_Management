using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Employee_Management.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "UserName")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\._\-]{0,23}[^.-]$",
            ErrorMessage = "UserName must be 1 to 24 characters in length, start with a letter, may contain letters, numbers or '.', '-', '_', and must not end in '.', '-', '._' or '-_'")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$",
            ErrorMessage = "Password must be between 8 and 15 characters and contain at least one uppercase letter, one lowercase letter, and one number.")]
        public string? Password { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
