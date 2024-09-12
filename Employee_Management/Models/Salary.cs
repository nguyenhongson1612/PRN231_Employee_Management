using System;
using System.Collections.Generic;

namespace Employee_Management.Models
{
    public partial class Salary
    {
        public int SalaryId { get; set; }
        public int? EmployeeId { get; set; }
        public decimal? SalaryAmount { get; set; }
        public DateTime? SalaryMonth { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
