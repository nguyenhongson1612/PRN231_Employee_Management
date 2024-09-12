using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Employee_Management.Models;

namespace Employee_Management.Pages.EmployeePositionView
{
    public class CreateModel : PageModel
    {
        private readonly Employee_Management.Models.EmployeeManagementSystemContext _context;

        public CreateModel(Employee_Management.Models.EmployeeManagementSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
        ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionId");
            return Page();
        }

        [BindProperty]
        public EmployeePosition EmployeePosition { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.EmployeePositions == null || EmployeePosition == null)
            {
                return Page();
            }

            _context.EmployeePositions.Add(EmployeePosition);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
