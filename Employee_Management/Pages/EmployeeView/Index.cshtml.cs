using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Employee_Management.Models;
using Microsoft.AspNetCore.SignalR;
using Shopping_Website;

namespace Employee_Management.Pages.EmployeeView
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

        public IList<Employee> Employee { get;set; } = default!;
        public Employee employee1 { get; set; } = default!;

        public async Task OnGetAsync()
        {
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
                        Employee = await _context.Employees.ToListAsync();

                    }
                    else
                    {
                        Employee = new List<Employee>();
                        if (_httpContextAccessor.HttpContext.Session.TryGetValue("Account", out var AccountData2))
                        {
                            Account AccountSession = System.Text.Json.JsonSerializer.Deserialize<Account>(AccountData2);
                            employee1 = _context.Employees.FirstOrDefault(ac => ac.EmployeeId == AccountSession.EmployeeId);
                            if (employee1 != null)
                            {
                                Employee.Add(employee1);
                            }
                        }

                    }
                }
                else { 
                    RedirectToPage("/AccountView/Login");
                }


            }
           

        }
    }
}
