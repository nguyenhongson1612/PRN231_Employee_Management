using System;
using System.Collections.Generic;

namespace Employee_Management.Models
{
    public partial class EmployeePosition
    {
        public int EmployeeId { get; set; }
        public int PositionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual Position Position { get; set; } = null!;
    }
}
