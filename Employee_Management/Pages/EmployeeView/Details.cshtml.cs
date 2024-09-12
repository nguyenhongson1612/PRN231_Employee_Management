using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Employee_Management.Models;

namespace Employee_Management.Pages.EmployeeView
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

        public Employee Employee { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }
            ViewData["IsManager"] = -1;
            if (_context.Employees != null)
            {
                if (_httpContextAccessor.HttpContext.Session.TryGetValue("AccountPosition", out var AccountData))
                {
                    string AccountPosition = System.Text.Json.JsonSerializer.Deserialize<string>(AccountData);
                    if (AccountPosition.Equals("Manager") || AccountPosition.Equals("HR Specialist"))
                    {
                        if (AccountPosition.Equals("Manager")) { ViewData["IsManager"] = 1; }
                        else { ViewData["IsManager"] = 0; }
                    }
                    
                }
            }
            var employee = await _context.Employees.FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                Employee = employee;
            }
            return Page();
        }
    }
}
