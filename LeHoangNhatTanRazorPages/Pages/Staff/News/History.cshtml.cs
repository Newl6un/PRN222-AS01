using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.News;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Shared;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeHoangNhatTanRazorPages.Pages.Staff.News
{
    public class HistoryModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<HistoryModel> _logger;

        public List<NewsArticleViewModel> NewsArticles { get; set; } = new List<NewsArticleViewModel>();

        [BindProperty(SupportsGet = true)]
        public NewsArticleParameters Parameters { get; set; } = new NewsArticleParameters();
        public PaginationViewModel Pagination { get; set; } = new PaginationViewModel();

        public HistoryModel(IServiceManager serviceManager, ILogger<HistoryModel> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Kiểm tra quyền truy cập (Staff hoặc Admin)
            var userRole = HttpContext.Session.GetInt32("UserRole");
            if (userRole != 0 && userRole != 1)
            {
                return RedirectToPage("/Auth/AccessDenied");
            }

            try
            {
                var userId = (short?)HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    return RedirectToPage("/Auth/Login");
                }

                // Thiết lập tham số để lấy tags cùng với tin tức
                Parameters.CreatedById = userId.Value;
                Parameters.IncludeTags = true;
                Parameters.IncludeCategory = true;

                var result = await _serviceManager.NewsArticle.GetNewsArticlesAsync(Parameters, false);
                NewsArticles = result;

                // Khởi tạo thông tin phân trang
                Pagination = new PaginationViewModel(
                    result.MetaData,
                    Request
                );

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting news history");
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi tải lịch sử tin tức";
                return Page();
            }
        }
    }
}
