using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Category;
using LeHoangNhatTanRazorPages.Shared.ViewModels.News;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Shared;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Tag;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeHoangNhatTanRazorPages.Pages.News
{
    public class DetailsModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(
            IServiceManager serviceManager,
            ILogger<DetailsModel> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }
        public NewsArticleViewModel NewsArticle { get; set; } = new NewsArticleViewModel();

        // Danh sách tin tức liên quan
        public List<NewsArticleViewModel> RelatedNews { get; set; } = new List<NewsArticleViewModel>();

        // Danh sách tin tức mới nhất
        public List<NewsArticleViewModel> LatestNews { get; set; } = new List<NewsArticleViewModel>();

        // Danh sách danh mục cho sidebar
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
        public PaginationViewModel Pagination { get; set; } = new PaginationViewModel();

        [BindProperty(SupportsGet = true)]
        public NewsArticleParameters Parameters { get; set; } = new NewsArticleParameters();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {
                // Lấy chi tiết tin tức
                var newsArticle = await _serviceManager.NewsArticle.GetNewsArticleAsync(id, false);
                if (newsArticle == null)
                {
                    return NotFound();
                }

                // Kiểm tra trạng thái tin tức
                // Nếu không hoạt động, chỉ Lecturer, Staff và Admin mới xem được
                if (!newsArticle.NewsStatus)
                {
                    var userRole = HttpContext.Session.GetInt32("UserRole");
                    if (userRole != 0 && userRole != 1 && userRole != 2)
                    {
                        return RedirectToPage("/Auth/AccessDenied");
                    }
                }
                NewsArticle = newsArticle;
                // Lấy tin tức liên quan (cùng danh mục)
                var relatedNewsParameter = new NewsArticleParameters
                {
                    PageSize = 4,
                    CategoryId = newsArticle.CategoryId,
                    ExcludeNewsArticleId = newsArticle.NewsArticleId,
                    NewsStatus = true,
                    IncludeCategory = true,
                    IncludeCreatedBy = true,
                    IncludeTags = true,
                    OrderBy = "CreatedDate desc"
                };
                var relatedNews = await _serviceManager.NewsArticle.GetNewsArticlesAsync(relatedNewsParameter, false);
                RelatedNews = relatedNews;


                Parameters.OrderBy = "CreatedDate desc";
                Parameters.PageSize = 6;
                Parameters.NewsStatus = true;
                Parameters.IncludeTags = true;
                Parameters.IncludeCategory = true;
                Parameters.IncludeCreatedBy = true;
                Parameters.ExcludeNewsArticleId = newsArticle.NewsArticleId;

                var latestNews = await _serviceManager.NewsArticle.GetNewsArticlesAsync(relatedNewsParameter, false);
                LatestNews = latestNews;

                // Lấy danh sách danh mục
                Pagination = new PaginationViewModel(
                    latestNews.MetaData,
                    Request
                );

                // Lấy danh sách danh mục cho sidebar
                var categoryParameters = new CategoryParameters
                {
                    PageSize = 6,
                    OrderBy = "CategoryName",
                    HasMostPost = true,
                    IsActive = true,
                    IncludeSubCategories = true,
                    IncludeNewsArticles = true,
                    IncludeParentCategory = true
                };

                var categories = await _serviceManager.Category.GetCategoriesAsync(categoryParameters, false);
                Categories = categories;

                // Lấy danh sách tag cho sidebar
                var tagParameters = new TagParameters
                {
                    PageSize = 10,
                    OrderBy = "TagName",
                    HasMostNews = true
                };

                var tags = await _serviceManager.Tag.GetTagsAsync(tagParameters, false);
                Tags = tags;



                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting news details {Id}", id);
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi tải chi tiết tin tức";
                return RedirectToPage("./Index");
            }
        }
    }
}
