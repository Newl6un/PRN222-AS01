using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.News;

namespace LeHoangNhatTanRazorPages.Services.Interfaces
{
    public interface INewsArticleService
    {
        Task<PagedList<NewsArticleViewModel>> GetNewsArticlesAsync(NewsArticleParameters parameters, bool trackChanges);
        Task<NewsArticleViewModel?> GetNewsArticleAsync(string newsArticleId, bool trackChanges);
        Task CreateNewsArticleAsync(NewsArticleViewModel newsArticleViewModel);
        Task UpdateNewsArticleAsync(NewsArticleViewModel newsArticleViewModel);
        Task DeleteNewsArticleAsyncAsync(string newsArticleId);

    }
}
