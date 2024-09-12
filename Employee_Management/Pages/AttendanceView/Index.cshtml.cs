using Employee_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management.Pages.AttendanceView
{
    public class IndexModel : PageModel
    {
        private readonly Employee_Management.Models.EmployeeManagementSystemContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(Employee_Management.Models.EmployeeManagementSystemContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<Attendance> Attendance { get; set; } = default!;
        public IList<Department> Departments { get; set; } = default!;
        public Attendance attendance1 { get; set; } = default!;

        [BindProperty]
        public int SelectedDepartment { get; set; }

        public async Task OnGetAsync()
        {
            if (Request.Query.TryGetValue("SelectedDepartment", out var selectedDepartment))
            {
                SelectedDepartment = Convert.ToInt32(selectedDepartment);
            }
            ViewData["IsManager"] = -1;

            if (_context.Attendances != null)
            {
                if (_httpContextAccessor.HttpContext.Session.TryGetValue("AccountPosition", out var AccountData))
                {
                    string AccountPosition = System.Text.Json.JsonSerializer.Deserialize<string>(AccountData);

                    if (AccountPosition.Equals("Manager") || AccountPosition.Equals("HR Specialist"))
                    {
                        if (AccountPosition.Equals("Manager")) { ViewData["IsManager"] = 1; }
                        else { ViewData["IsManager"] = 0; }

                        if (SelectedDepartment != 0)
                        {
                            Attendance = await _context.Attendances
                                .Include(e => e.Employee.Departments)
                                .Where(a => a.Employee.Departments.Any(d => d.DepartmentId == SelectedDepartment))
                                .OrderByDescending( e => e.AttendanceDate)
                                .ToListAsync();
                        }
                        else
                        {
                            Attendance = await _context.Attendances
                                .Include(e => e.Employee)
                                .OrderByDescending(e => e.AttendanceDate)
                                .ToListAsync();
                        }

                        Departments = await _context.Departments.ToListAsync();
                    }
                    else
                    {
                        Attendance = new List<Attendance>();

                        if (_httpContextAccessor.HttpContext.Session.TryGetValue("Account", out var AccountData2))
                        {
                            Account AccountSession = System.Text.Json.JsonSerializer.Deserialize<Account>(AccountData2);
                            Attendance = _context.Attendances
                                .Include(e => e.Employee)
                                .OrderByDescending(e => e.AttendanceDate)
                                .Where(a => a.EmployeeId == AccountSession.EmployeeId).ToList();
                        }
                    }
                }
                else
                {
                    RedirectToPage("/AccountView/Login");
                }
            }
        }
    }
}