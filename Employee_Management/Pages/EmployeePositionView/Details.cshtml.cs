using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Employee_Management.Models;

namespace Employee_Management.Pages.EmployeePositionView
{
    public class DetailsModel : PageModel
    {
        private readonly Employee_Management.Models.EmployeeManagementSystemContext _context;

        public DetailsModel(Employee_Management.Models.EmployeeManagementSystemContext context)
        {
            _context = context;
        }

      public EmployeePosition EmployeePosition { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.EmployeePositions == null)
            {
                return NotFound();
            }

            var employeeposition = await _context.EmployeePositions.FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employeeposition == null)
            {
                return NotFound();
            }
            else 
            {
                EmployeePosition = employeeposition;
            }
            return Page();
        }
    }
}
