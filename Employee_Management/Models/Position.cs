using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management.Models
{
    public partial class Position
    {
        public Position()
        {
            EmployeePositions = new HashSet<EmployeePosition>();
        }

        public int PositionId { get; set; }

        [Required(ErrorMessage = "Position Name is required")]
        [StringLength(50, ErrorMessage = "Position Name can't be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Position Name can only contain letters and spaces")]
        public string? PositionName { get; set; }

        public virtual ICollection<EmployeePosition> EmployeePositions { get; set; }
    }
}
