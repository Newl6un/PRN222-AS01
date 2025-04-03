using LeHoangNhatTanRazorPages.Shared.ViewModels.Category;
using LeHoangNhatTanRazorPages.Shared.ViewModels.News;

namespace LeHoangNhatTanRazorPages.Shared.ViewModels.Home
{
    public class HomeViewModel
    {
        public List<NewsArticleViewModel> FeaturedNews { get; set; } = new List<NewsArticleViewModel>();
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
