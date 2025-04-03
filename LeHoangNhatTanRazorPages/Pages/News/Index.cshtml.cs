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
    public class IndexModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<IndexModel> _logger;

        public List<NewsArticleViewModel> NewsArticles { get; set; } = new List<NewsArticleViewModel>();
        public NewsArticleViewModel? FeaturedNews { get; set; }
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
        public PaginationViewModel Pagination { get; set; } = new PaginationViewModel();

        [BindProperty(SupportsGet = true)]
        public NewsArticleParameters Parameters { get; set; } = new NewsArticleParameters();

        public IndexModel(
            IServiceManager serviceManager,
            ILogger<IndexModel> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {

                Parameters.PageSize = 1;
                Parameters.OrderBy = "CreatedDate desc";
                Parameters.NewsStatus = true;
                Parameters.IncludeTags = true;
                Parameters.IncludeCategory = true;
                Parameters.IncludeCreatedBy = true;

                // Lấy tin tức nổi bật (1 tin mới nhất, có trạng thái hoạt động)
                var result = await _serviceManager.NewsArticle.GetNewsArticlesAsync(Parameters, false);
                FeaturedNews = result.FirstOrDefault();

                //lấy 6 tin tức còn lại
                Parameters.PageSize = 6;
                Parameters.ExcludeNewsArticleId = FeaturedNews?.NewsArticleId;

                result = await _serviceManager.NewsArticle.GetNewsArticlesAsync(Parameters, false);
                NewsArticles = result;

                // Khởi tạo thông tin phân trang
                Pagination = new PaginationViewModel(
                    result.MetaData,
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
                _logger.LogError(ex, "Error getting public news");
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi tải tin tức";
                return Page();
            }
        }
    }
}
