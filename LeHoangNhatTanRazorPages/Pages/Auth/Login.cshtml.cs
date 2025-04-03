using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.Configurations;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Account;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Auth;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeHoangNhatTanRazorPages.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        private readonly AppAccountConfiguration _adminAccount;

        [BindProperty]
        public LoginViewModel Input { get; set; } = new LoginViewModel();

        public LoginModel(IServiceManager serviceManager, AppAccountConfiguration adminAccount)
        {
            _serviceManager = serviceManager;
            _adminAccount = adminAccount;
        }

        public IActionResult OnGet()
        {
            // Nếu đã đăng nhập, chuyển hướng về trang chủ
            if (HttpContext.Session.GetInt32("UserRole").HasValue)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string adminEmail = _adminAccount.AdminEmail?.Trim() ?? "";
            string adminPassword = _adminAccount.AdminPassword?.Trim() ?? "";

            if (Input.Email!.Equals(adminEmail, StringComparison.OrdinalIgnoreCase) && Input.Password == adminPassword)
            {
                SetSessionForAdmin(adminEmail);
                return RedirectToPage("/Admin/Accounts/Index");
            }

            var result = await _serviceManager.SystemAccount.ValidateUserAsync(Input.Email, Input.Password!, false);

            if (result != null)
            {
                // Lưu thông tin người dùng vào Session
                SetSessionForUser(result);

                // Điều hướng dựa trên vai trò
                return RedirectBaseOnRole(result.AccountRole!.Value);
            }

            ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng");
            return Page();
        }

        private void SetSessionForAdmin(string adminEmail)
        {
            HttpContext.Session.SetInt32("UserRole", 0);
            HttpContext.Session.SetInt32("UserId", 0);
            HttpContext.Session.SetString("UserName", "Administrator");
            HttpContext.Session.SetString("UserEmail", adminEmail);
        }

        private void SetSessionForUser(AccountViewModel result)
        {
            HttpContext.Session.SetInt32("UserRole", result.AccountRole!.Value);
            HttpContext.Session.SetInt32("UserId", result.AccountId);
            HttpContext.Session.SetString("UserName", result.AccountName!);
            HttpContext.Session.SetString("UserEmail", result.AccountEmail!);
        }

        private IActionResult RedirectBaseOnRole(int role)
        {
            switch (role)
            {
                case 0: // Admin
                    return RedirectToPage("/Admin/Accounts/Index");
                case 1: // Staff
                    return RedirectToPage("/Staff/News/Index");
                case 2: // Lecturer
                    return RedirectToPage("/Public/News/Index");
                default:
                    return RedirectToPage("/Index");
            }
        }
    }
}
