using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

namespace LeHoangNhatTanRazorPages.Repository.Interfaces
{
    public interface INewsArticleRepository : IRepositoryBase<NewsArticle>
    {
        Task<PagedList<NewsArticle>> GetNewsArticles(NewsArticleParameters newsArticleParameters, bool trackChanges);
    }
}
