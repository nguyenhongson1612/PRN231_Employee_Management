using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Employee_Management.Models;

namespace Employee_Management.Pages.SalaryView
{
    public class IndexModel : PageModel
    {
        private readonly Employee_Management.Models.EmployeeManagementSystemContext _context;

        public IndexModel(Employee_Management.Models.EmployeeManagementSystemContext context)
        {
            _context = context;
        }

        public IList<Salary> Salary { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Salaries != null)
            {
                Salary = await _context.Salaries
                .Include(s => s.Employee).ToListAsync();
            }
        }
    }
}
