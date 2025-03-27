using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.DataAccess.Interfaces;
using LeHoangNhatTanRazorPages.Repository.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

namespace LeHoangNhatTanRazorPages.Repository.Implementation
{
    public class NewsArticleRepository : RepositoryBase<NewsArticle>, INewsArticleRepository
    {
        public NewsArticleRepository(IBaseDAO<NewsArticle> dao) : base(dao)
        {
        }

        public async Task<PagedList<NewsArticle>> GetNewsArticles(NewsArticleParameters newsArticleParameters, bool trackChanges)
        {
            return await FindAll(newsArticleParameters, trackChanges);
        }
    }
}
