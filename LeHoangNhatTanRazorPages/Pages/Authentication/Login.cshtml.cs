using LeHoangNhatTanRazorPages.PageModels;
using LeHoangNhatTanRazorPages.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeHoangNhatTanRazorPages.Pages.Authentication
{
    public class LoginModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        private readonly IConfiguration _configuration;

        public LoginModel(IServiceManager serviceManager, IConfiguration configuration)
        {
            _serviceManager = serviceManager;
            _configuration = configuration;
        }

        [BindProperty(SupportsGet = true)]
        public string? Message { get; set; }

        [BindProperty]
        public LoginInputModel LoginInput { get; set; } = new LoginInputModel();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check for admin account from config
            var adminConfig = _configuration.GetSection("AdminConfig");
            string adminEmail = adminConfig["AdminEmail"]?.Trim() ?? "";
            string adminPassword = adminConfig["AdminPassword"]?.Trim() ?? "";

            if (LoginInput.Email == adminEmail && LoginInput.Password == adminPassword)
            {
                // Set session for admin
                HttpContext.Session.SetString("EmailAddress", adminEmail);
                HttpContext.Session.SetInt32("Role", 0); // Admin role = 0
                HttpContext.Session.SetString("AccountName", "Administrator");
                HttpContext.Session.SetInt32("UserId", 0);

                return Redirect("/");
            }

            // Check database accounts
            var account = await _serviceManager.SystemAccount.FindSystemAccount(
                LoginInput.Email, LoginInput.Password);

            if (account == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password");
                return Page();
            }

            // Check account role
            if (account.AccountRole == 1 || account.AccountRole == 2) // Staff = 1, Lecturer = 2
            {
                // Set session values
                HttpContext.Session.SetString("EmailAddress", account.AccountEmail!);
                HttpContext.Session.SetInt32("Role", account.AccountRole.Value);
                HttpContext.Session.SetString("AccountName", account.AccountName ?? "User");
                HttpContext.Session.SetInt32("UserId", account.AccountId);

                return Redirect("/");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "You don't have permission to access this system");
                return Page();
            }
        }
    }
}
