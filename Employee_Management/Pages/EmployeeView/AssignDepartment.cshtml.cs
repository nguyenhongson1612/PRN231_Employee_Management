using Employee_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management.Pages.EmployeeView
{
    public class AssignDepartmentModel : PageModel
    {
        private readonly Employee_Management.Models.EmployeeManagementSystemContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AssignDepartmentModel(Employee_Management.Models.EmployeeManagementSystemContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            Departments = new List<Department>();
        }
        [BindProperty]
        public Employee Employee { get; set; } = default!;
        [BindProperty]
        public int SelectedDepartmentId { get; set; }
        public List<Department> Departments { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["IsManager"] = -1;
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }
            if (_httpContextAccessor.HttpContext.Session.TryGetValue("AccountPosition", out var AccountData))
            {
                string AccountPosition = System.Text.Json.JsonSerializer.Deserialize<string>(AccountData);
                if (AccountPosition.Equals("Manager") || AccountPosition.Equals("HR Specialist"))
                {
                    if (AccountPosition.Equals("Manager")) { ViewData["IsManager"] = 1; }
                    else { ViewData["IsManager"] = 0; }
                    Employee = _context.Employees
                        .Include(e => e.Departments).SingleOrDefault(e => e.EmployeeId == id);
                    if (Employee == null)
                    {
                        return NotFound();
                    }
                    Departments = _context.Departments.ToList();
                    return Page();

                }
                else
                {
                    return RedirectToPage("/EmployeeView/");

                }
            }
            else
            {
                return RedirectToPage("/AccountView/Login");
            }
        }
        public async Task<IActionResult> OnPost()
        {
                // Update the employee's department associations in the EmployeeDepartments table.
            var updatedEmployee = _context.Employees.Include(e => e.Departments)
            .FirstOrDefault(e => e.EmployeeId == Employee.EmployeeId);
            if (updatedEmployee == null)
            {
                // Handle the case where the employee with the specified EmployeeId is not found.
                return NotFound(); // Or perform other error handling as needed.
            }
            updatedEmployee.Departments.Clear(); // Remove existing associations.
           
            foreach (var departmentId in Employee.Departments)
            {
                var department = _context.Departments.Find(SelectedDepartmentId);
                if (department != null)
                {
                    updatedEmployee.Departments.Add(department); // Associate the employee with selected departments.
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index"); // Redirect to a success page or index page.
            
            
        }
    }
}
