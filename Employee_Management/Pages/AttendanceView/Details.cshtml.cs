using Employee_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management.Pages.AttendanceView
{
    public class DetailsModel : PageModel
    {
        private readonly Employee_Management.Models.EmployeeManagementSystemContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DetailsModel(Employee_Management.Models.EmployeeManagementSystemContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public Attendance Attendance { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.Include(a => a.Employee).FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }
            else
            {
                Attendance = attendance;
            }
            ViewData["IsManager"] = -1;

            if (_httpContextAccessor.HttpContext.Session.TryGetValue("AccountPosition", out var AccountData))
            {
                string AccountPosition = System.Text.Json.JsonSerializer.Deserialize<string>(AccountData);

                if (AccountPosition.Equals("Manager") || AccountPosition.Equals("HR Specialist"))
                {
                    if (AccountPosition.Equals("Manager")) { ViewData["IsManager"] = 1; }
                    else { ViewData["IsManager"] = 0; }
                }
            }
            else
            {
                RedirectToPage("/AccountView/Login");
            }
            return Page();
        }
    }
}