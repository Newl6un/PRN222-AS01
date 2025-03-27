using LeHoangNhatTanRazorPages.Shared.DataTransferObjects.NewsArticle;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

namespace LeHoangNhatTanRazorPages.Services.Interfaces
{
    public interface INewsArticleService
    {
        Task<NewsArticleDto?> GetNewsArticle(short newsArticleId);
        Task<PagedList<NewsArticleDto>> GetNewsArticles(NewsArticleParameters newsArticleParameters, bool trackChanges);
    }
}
