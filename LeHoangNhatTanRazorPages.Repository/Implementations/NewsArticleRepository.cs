using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.DataAccess.Interfaces;
using LeHoangNhatTanRazorPages.Repository.Extensions;
using LeHoangNhatTanRazorPages.Repository.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

using Microsoft.EntityFrameworkCore;

namespace LeHoangNhatTanRazorPages.Repository.Implementation
{
    public class NewsArticleRepository : RepositoryBase<NewsArticle>, INewsArticleRepository
    {
        public NewsArticleRepository(IBaseDAO<NewsArticle> dao) : base(dao)
        {
        }

        public Task<NewsArticle?> GetNewsArticleAsync(string newsArticleId, bool trackChanges)
        {
            var newsArticle = _dao.GetByCondition(x => x.NewsArticleId == newsArticleId, trackChanges)
                .IncludeCategory(true)
                .IncludeCreatedBy(true)
                .IncludeTags(true)
                .FirstOrDefaultAsync();

            return newsArticle;
        }

        public async Task<PagedList<NewsArticle>> GetNewsArticlesAsync(NewsArticleParameters parameters, bool trackChanges)
        {
            return await _dao.GetAll(trackChanges)
                .FilterNewsArticles(parameters)
                .Sort(parameters.OrderBy)
                .ToPagedListAsync(parameters);
        }

        public new async Task Create(NewsArticle entity)
        {
            var lastestNews = await _dao.GetAll(false)
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();
            if (lastestNews != null)
            {
                var newId = int.Parse(lastestNews.NewsArticleId) + 1;
                entity.NewsArticleId = newId.ToString();
            }
            else
            {
                entity.NewsArticleId = "1";
            }
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            _dao.Create(entity);
        }
    }
}
