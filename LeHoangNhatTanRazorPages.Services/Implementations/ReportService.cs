using AutoMapper;

using LeHoangNhatTanRazorPages.Repository.Interfaces;
using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Report;

namespace LeHoangNhatTanRazorPages.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ReportService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<StatisticsViewModel> GetStatisticsAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Đảm bảo endDate là cuối ngày
                endDate = endDate.Date.AddDays(1).AddMilliseconds(-1);

                var newsArticleParameter = new NewsArticleParameters
                {
                    FromDate = startDate,
                    ToDate = endDate,
                    TakeAll = true,
                    IncludeCategory = true,
                    IncludeTags = true,
                    IncludeCreatedBy = true,
                };
                var newsArticles = await _repositoryManager.NewsArticle.GetNewsArticlesAsync(newsArticleParameter, false);

                // Tạo đối tượng kết quả
                var result = new StatisticsViewModel
                {
                    TotalNews = newsArticles.Count(),
                    ActiveNews = newsArticles.Count(n => n.NewsStatus),
                    InactiveNews = newsArticles.Count(n => !n.NewsStatus)
                };

                // Lấy danh sách các danh mục được sử dụng
                var usedCategory = newsArticles
                    .Where(n => n.CategoryId.HasValue)
                    .Select(n => n.Category)
                    .Distinct()
                    .ToList();


                result.Categories = usedCategory.Count();

                // Thống kê theo danh mục
                var categoryStats = newsArticles
                    .Where(n => n.CategoryId.HasValue)
                    .GroupBy(n => n.CategoryId.Value)
                    .Select(g => new CategoryStatItem
                    {
                        CategoryId = g.Key,
                        Count = g.Count()
                    })
                    .ToList();

                // Lấy thông tin tên danh mục
                foreach (var stat in categoryStats)
                {
                    var category = usedCategory.FirstOrDefault(c => c.CategoryId == stat.CategoryId);
                    if (category != null)
                    {
                        stat.CategoryName = category.CategoryName;
                    }
                }

                // Sắp xếp theo số lượng giảm dần
                result.CategoryStats = categoryStats.OrderByDescending(c => c.Count).ToList();

                // Thống kê theo ngày
                result.DailyStats = newsArticles
                    .Where(n => n.CreatedDate.HasValue)
                    .GroupBy(n => n.CreatedDate.Value.Date)
                    .Select(g => new DailyStatItem
                    {
                        Date = g.Key,
                        Count = g.Count()
                    })
                    .OrderByDescending(d => d.Date)
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
