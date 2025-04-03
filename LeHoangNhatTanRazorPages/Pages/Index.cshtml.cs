using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Home;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeHoangNhatTanRazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IServiceManager _serviceManager;

        public HomeViewModel HomeViewModel { get; set; } = new HomeViewModel();

        public IndexModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task OnGetAsync()
        {
            // Lấy tin tức nổi bật (6 tin mới nhất, có trạng thái hoạt động)
            var newsParameters = new NewsArticleParameters
            {
                PageSize = 6,
                OrderBy = "CreatedDate desc",
                NewsStatus = true,
                IncludeTags = true,
                IncludeCategory = true,
                IncludeCreatedBy = true
            };
            var featuredNews = await _serviceManager.NewsArticle.GetNewsArticlesAsync(newsParameters, false);

            HomeViewModel.FeaturedNews = featuredNews;

            // Lấy danh mục tin tức (6 danh mục có trạng thái hoạt động và có nhiều bài viết)
            var categoryParameters = new CategoryParameters
            {
                PageSize = 6,
                OrderBy = "CategoryName",
                HasMostPost = true,
                IsActive = true
            };

            var categories = await _serviceManager.Category.GetCategoriesAsync(categoryParameters, false);

            HomeViewModel.Categories = categories;

        }
    }
}
