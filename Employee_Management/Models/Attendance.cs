using System;
using System.Collections.Generic;

namespace Employee_Management.Models
{
    public partial class Attendance
    {
        public int AttendanceId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string? Status { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
