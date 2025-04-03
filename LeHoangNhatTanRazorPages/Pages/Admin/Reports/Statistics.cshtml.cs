using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Report;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeHoangNhatTanRazorPages.Pages.Admin.Reports
{
    public class StatisticsModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<StatisticsModel> _logger;

        [BindProperty]
        public DateRangeViewModel DateRange { get; set; } = new DateRangeViewModel();

        public StatisticsViewModel? Statistics { get; set; }

        public List<string> ChartLabels { get; set; } = new List<string>();
        public List<int> ChartData { get; set; } = new List<int>();

        public StatisticsModel(IServiceManager serviceManager, ILogger<StatisticsModel> logger)
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
                // Nếu chưa có giá trị, thiết lập giá trị mặc định
                if (DateRange.StartDate == default)
                {
                    DateRange.StartDate = DateTime.Now.AddDays(-30);
                }
                if (DateRange.EndDate == default)
                {
                    DateRange.EndDate = DateTime.Now;
                }

                var statistics = await _serviceManager.Report.GetStatisticsAsync(DateRange.StartDate, DateRange.EndDate);
                Statistics = statistics ?? new StatisticsViewModel();

                // Chuẩn bị dữ liệu cho biểu đồ
                PrepareChartData();

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tải dữ liệu thống kê");
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi tải báo cáo thống kê";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Kiểm tra quyền truy cập
            if (HttpContext.Session.GetInt32("UserRole") != 0)
            {
                return RedirectToPage("/Auth/AccessDenied");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var statistics = await _serviceManager.Report.GetStatisticsAsync(DateRange.StartDate, DateRange.EndDate);
                Statistics = statistics ?? new StatisticsViewModel();

                // Chuẩn bị dữ liệu cho biểu đồ
                PrepareChartData();

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo báo cáo thống kê: {Message}", ex.Message);
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi tạo báo cáo thống kê";
                return Page();
            }
        }

        private void PrepareChartData()
        {
            // Đảm bảo Statistics không null
            if (Statistics == null)
            {
                ChartLabels = new List<string>();
                ChartData = new List<int>();
                return;
            }

            // Dữ liệu cho biểu đồ - ưu tiên dùng CategoryStats
            if (Statistics.CategoryStats != null && Statistics.CategoryStats.Any())
            {
                ChartLabels = Statistics.CategoryStats.Select(c => c.CategoryName ?? "Unknown").ToList();
                ChartData = Statistics.CategoryStats.Select(c => c.Count).ToList();
            }
            // Fallback sử dụng DailyStats nếu cần
            else if (Statistics.DailyStats != null && Statistics.DailyStats.Any())
            {
                ChartLabels = Statistics.DailyStats.Select(d => d.Date.ToString("dd/MM/yyyy") ?? "Unknown").ToList();
                ChartData = Statistics.DailyStats.Select(d => d.Count).ToList();
            }
            else
            {
                ChartLabels = new List<string>();
                ChartData = new List<int>();
            }
        }
    }
}
