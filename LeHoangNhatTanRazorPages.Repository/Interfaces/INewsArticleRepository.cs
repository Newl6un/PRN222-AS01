using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

namespace LeHoangNhatTanRazorPages.Repository.Interfaces
{
    public interface INewsArticleRepository : IRepositoryBase<NewsArticle>
    {
        public Task<PagedList<NewsArticle>> GetNewsArticlesAsync(NewsArticleParameters parameters, bool trackChanges);

        public Task<NewsArticle?> GetNewsArticleAsync(string newsArticleId, bool trackChanges);

        public new Task Create(NewsArticle entity);
    }
}
