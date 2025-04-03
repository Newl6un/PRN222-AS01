using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Account;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Shared;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeHoangNhatTanRazorPages.Pages.Admin.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<IndexModel> _logger;

        public List<AccountViewModel> Accounts { get; set; } = new List<AccountViewModel>();

        [BindProperty(SupportsGet = true)]
        public SystemAccountParameters Parameters { get; set; } = new SystemAccountParameters();
        public PaginationViewModel Pagination { get; set; } = new PaginationViewModel();

        public IndexModel(IServiceManager serviceManager, ILogger<IndexModel> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Kiểm tra quyền truy cập (chỉ Admin mới được phép)
            if (HttpContext.Session.GetInt32("UserRole") != 0)
            {
                return RedirectToPage("/Auth/AccessDenied");
            }


            try
            {
                var result = await _serviceManager.SystemAccount.GetAccountsAsync(Parameters, false);
                Accounts = result;

                Pagination = new PaginationViewModel(
                    result.MetaData,
                    Request
                );

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting accounts");
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi tải danh sách tài khoản";
                return Page();
            }
        }
        public IActionResult OnGetCreate()
        {

            return Partial("Partials/_CreateAccount", new AccountViewModel());
        }

        public async Task<IActionResult> OnPostCreateAsync([FromForm] AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Partial("Partials/_CreateAccount", model);
            }

            try
            {
                await _serviceManager.SystemAccount.CreateAccountAsync(model);
                TempData["SuccessMessage"] = "Tạo tài khoản thành công!";

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category");
                return StatusCode(500, "Đã xảy ra lỗi khi tạo tài khoản");
            }
        }

        public async Task<IActionResult> OnGetEditAsync(short id)
        {
            var account = await _serviceManager.SystemAccount.GetAccountAsync(id, false);
            if (account == null)
            {
                return NotFound();
            }

            return Partial("Partials/_EditAccount", account);
        }

        public async Task<IActionResult> OnPostEditAsync([FromForm] AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Partial("Partials/_EditAccount", model);
            }
            try
            {
                await _serviceManager.SystemAccount.UpdateAccountAsync(model);
                TempData["SuccessMessage"] = "Cập nhật tài khoản thành công!";
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating account");
                return StatusCode(500, "Đã xảy ra lỗi khi cập nhật tài khoản");
            }
        }

        public async Task<IActionResult> OnGetDeleteAsync(short id)
        {
            var account = await _serviceManager.SystemAccount.GetAccountAsync(id, false);
            if (account == null)
            {
                return NotFound();
            }
            return Partial("Partials/_DeleteAccount", account);
        }

        public async Task<IActionResult> OnPostDeleteAsync(short id)
        {
            try
            {
                await _serviceManager.SystemAccount.DeleteAccountAsync(id);
                TempData["SuccessMessage"] = "Xóa tài khoản thành công!";
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting account");
                return StatusCode(500, "Đã xảy ra lỗi khi xóa tài khoản");
            }
        }

    }
}
