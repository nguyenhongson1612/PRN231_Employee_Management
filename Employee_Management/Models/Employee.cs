using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Accounts = new HashSet<Account>();
            Attendances = new HashSet<Attendance>();
            EmployeePositions = new HashSet<EmployeePosition>();
            Salaries = new HashSet<Salary>();
            Departments = new HashSet<Department>();
        }

        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "FirstName")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters")]
        public string? FirstName { get; set; }


        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "LastName")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name can only contain letters")]
        public string? LastName { get; set; }


        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Format")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [Display(Name = "PhoneNumber")]
        [RegularExpression(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", ErrorMessage = "Invalid Phone Number Format")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string? PhoneNumber { get; set; }


        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address can't be longer than 100 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s,]*$", ErrorMessage = "Invalid Address Format")]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        [NotMapped]
        public string Fullname
        {
            get { return this.FirstName + " " + this.LastName; }
        }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<EmployeePosition> EmployeePositions { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
