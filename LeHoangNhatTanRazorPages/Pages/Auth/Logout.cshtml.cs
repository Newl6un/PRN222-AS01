using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeHoangNhatTanRazorPages.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(ILogger<LogoutModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            // Kiểm tra nếu người dùng chưa đăng nhập
            if (!HttpContext.Session.Keys.Contains("UserId"))
            {
                return RedirectToPage("/Auth/Login");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                // Lấy thông tin người dùng để ghi log
                var userId = HttpContext.Session.GetInt32("UserId");
                var userName = HttpContext.Session.GetString("UserName");

                // Xóa tất cả các session
                HttpContext.Session.Clear();

                // Ghi log
                _logger.LogInformation($"Người dùng ID: {userId}, Tên: {userName} đã đăng xuất thành công vào lúc {DateTime.Now}");

                // Redirect về trang đăng nhập
                return RedirectToPage("/Auth/Login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đăng xuất");
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi đăng xuất. Vui lòng thử lại sau.";
                return Page();
            }
        }
    }
}
