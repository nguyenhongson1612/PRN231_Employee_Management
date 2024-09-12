using Employee_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management.Pages.AttendanceView
{
    public class CreateModel : PageModel
    {
        private readonly Employee_Management.Models.EmployeeManagementSystemContext _context;

        public CreateModel(Employee_Management.Models.EmployeeManagementSystemContext context)
        {
            _context = context;
        }

        public List<Employee> Employees { get; set; }
        public IList<Department> Departments { get; set; } = default!;
        public IActionResult OnGet()
        {
            var today = DateTime.Today;

            //ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
                Employees = _context.Employees
                .Include(e => e.EmployeePositions)
                .Include(e => e.Attendances)
                .Where(e => e.EmployeePositions.Any(ep => ep.PositionId == 2) && !e.Attendances.Any(a => a.AttendanceDate == today))
                .ToList();
            Departments =  _context.Departments.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var today = DateTime.Today;
            Attendance attendance;
            Employees = _context.Employees
                .Include(e => e.EmployeePositions)
                .Include(e => e.Attendances)
                .Where(e => e.EmployeePositions.Any(ep => ep.PositionId == 2) && !e.Attendances.Any(a => a.AttendanceDate == today))
                .ToList();
            int t = Employees.Count;
            for (int i = 0; i < Employees.Count; i++)
            {
                var status = Request.Form["AttendanceList[" + i + "].Status"];
                var employeeId = Employees[i].EmployeeId;

                attendance = new Attendance() { 
                    EmployeeId = employeeId,
                    AttendanceDate = today,
                    Status = status,
                };
                _context.Attendances.Add(attendance);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToPage("./Index");
        }
    }
}