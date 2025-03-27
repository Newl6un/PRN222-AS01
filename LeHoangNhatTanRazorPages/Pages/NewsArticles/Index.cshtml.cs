using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.DataTransferObjects.NewsArticle;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace LeHoangNhatTanRazorPages.Pages.NewsArticles
{
    public class IndexModel : PageModel
    {
        private readonly IServiceManager _serviceManager;

        public IndexModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public PagedList<NewsArticleDto> NewsArticles { get; set; } = default!;

        public async Task OnGet()
        {
            var pagingParameters = new NewsArticleParameters
            {
                PageNumber = CurrentPage,
                PageSize = PageSize,

            };
            NewsArticles = await _serviceManager.NewsArticle.GetNewsArticles(pagingParameters, false);
        }
    }
}
