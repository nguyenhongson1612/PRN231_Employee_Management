using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping_Website.Pages.AccountView
{
    public class LogoutModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult OnGet()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.Remove("Account");
            return RedirectToPage("./Login");
        }
    }
}
