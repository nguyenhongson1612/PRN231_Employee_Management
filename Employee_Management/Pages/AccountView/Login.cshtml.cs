using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Employee_Management.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Principal;

namespace Shopping_Website.Pages.AccountView
{
    public class LoginModel : PageModel
    {
        private readonly Employee_Management.Models.EmployeeManagementSystemContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(Employee_Management.Models.EmployeeManagementSystemContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            Account = _context.Accounts.Include(a => a.Employee)
                .Where(ac => ac.Username.Equals(Account.Username) && ac.Password.Equals(Account.Password))
                .Select(ac => new Account
                {
                    AccountId = ac.AccountId,
                    Username = ac.Username,
                    EmployeeId = ac.EmployeeId
                })
                .FirstOrDefault();
            if (Account == null)
            {
                ModelState.AddModelError("Account.Password", "Password Or UserName Invalid");
                return Page();
            }
            var currentPosition = _context.EmployeePositions
                .Where(ep => ep.EmployeeId == Account.EmployeeId && ep.EndDate == null)
                .Include(ep => ep.Position)
                .Select(ep => ep.Position.PositionName)
                .FirstOrDefault();
            var serializedCart = System.Text.Json.JsonSerializer.Serialize(Account);
           /* var serializedCart = JsonConvert.SerializeObject(Account);*/
            var serializedCart2 = System.Text.Json.JsonSerializer.Serialize(currentPosition);
            _httpContextAccessor.HttpContext.Session.Set("Account", System.Text.Encoding.UTF8.GetBytes(serializedCart));
            _httpContextAccessor.HttpContext.Session.Set("AccountPosition", System.Text.Encoding.UTF8.GetBytes(serializedCart2));
            return RedirectToPage("/EmployeeView/Index");
        }
        public JsonResult OnGetGetSessionValue()
        {
            _httpContextAccessor.HttpContext.Session.TryGetValue("Account", out var AccountData);
            return new JsonResult(System.Text.Json.JsonSerializer.Deserialize<Account>(AccountData));
        }
    }
}
