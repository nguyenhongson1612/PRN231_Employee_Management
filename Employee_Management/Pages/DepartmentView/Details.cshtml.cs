﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Employee_Management.Models;

namespace Employee_Management.Pages.DepartmentView
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

        public Department Department { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }
            ViewData["IsManager"] = -1;
            if (_context.Departments != null)
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
            var department = await _context.Departments.FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }
            else 
            {
                Department = department;
            }
            return Page();
        }
    }
}
