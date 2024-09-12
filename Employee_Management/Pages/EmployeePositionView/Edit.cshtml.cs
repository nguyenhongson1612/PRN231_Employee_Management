using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Employee_Management.Models;

namespace Employee_Management.Pages.EmployeePositionView
{
    public class EditModel : PageModel
    {
        private readonly Employee_Management.Models.EmployeeManagementSystemContext _context;

        public EditModel(Employee_Management.Models.EmployeeManagementSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EmployeePosition EmployeePosition { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.EmployeePositions == null)
            {
                return NotFound();
            }

            var employeeposition =  await _context.EmployeePositions.FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employeeposition == null)
            {
                return NotFound();
            }
            EmployeePosition = employeeposition;
           ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
           ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(EmployeePosition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeePositionExists(EmployeePosition.EmployeeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EmployeePositionExists(int id)
        {
          return (_context.EmployeePositions?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
