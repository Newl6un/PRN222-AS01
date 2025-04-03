using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Account;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeHoangNhatTanRazorPages.Pages.Staff.Profile
{
    public class IndexModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<IndexModel> _logger;

        public ProfileViewModel Account { get; set; } = new ProfileViewModel();

        [BindProperty]
        public ChangePasswordViewModel PasswordForm { get; set; } = new ChangePasswordViewModel();

        public IndexModel(IServiceManager serviceManager, ILogger<IndexModel> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Auth/Login");
            }
            try
            {
                // Lấy thông tin tài khoản
                Account = await _serviceManager.SystemAccount.GetProfileAsync((short)userId, false);

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting profile for user {UserId}", userId);
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi tải thông tin cá nhân";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostChangePasswordAsync()
        {

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Auth/Login");
            }
            Account = await _serviceManager.SystemAccount.GetProfileAsync((short)userId, false);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var result = await _serviceManager.SystemAccount.ChangePasswordAsync(
                    (short)userId,
                    PasswordForm.CurrentPassword ?? "",
                    PasswordForm.NewPassword ?? "");

                if (result)
                {
                    TempData["SuccessMessage"] = "Đổi mật khẩu thành công";
                }
                else
                {
                    TempData["ErrorMessage"] = "Mật khẩu hiện tại không đúng";
                }
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi đổi mật khẩu";
                return Page();
            }
        }

        public async Task<IActionResult> OnGetEditAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Auth/Login");
            }
            try
            {
                // Lấy thông tin tài khoản
                var profile = await _serviceManager.SystemAccount.GetProfileAsync((short)userId, false);

                return Partial("Partials/_EditProfile", profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting profile for user {UserId}", userId);
                return StatusCode(500, "Đã xảy ra lỗi khi tải thông tin cá nhân");
            }
        }

        public async Task<IActionResult> OnPostEditAsync([FromForm] ProfileViewModel profile)
        {
            ModelState.Remove("CurrentPassword");
            ModelState.Remove("NewPassword");
            ModelState.Remove("ConfirmPassword");
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Auth/Login");
            }


            if (!ModelState.IsValid)
            {
                return Partial("Partials/_EditProfile", profile);
            }

            try
            {
                await _serviceManager.SystemAccount.UpdateProfileAsync(profile);
                HttpContext.Session.SetString("UserName", profile.AccountName ?? "");

                TempData["SuccessMessage"] = "Cập nhật thông tin cá nhân thành công";
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating profile for user {UserId}", profile.AccountId);
                return StatusCode(500, "Đã xảy ra lỗi khi cập nhật thông tin cá nhân");
            }
        }
    }
}
