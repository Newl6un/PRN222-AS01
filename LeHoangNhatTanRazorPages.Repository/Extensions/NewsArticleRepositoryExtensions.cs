using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

using Microsoft.EntityFrameworkCore;

using System.Linq.Dynamic.Core;

namespace LeHoangNhatTanRazorPages.Repository.Extensions
{
    public static class NewsArticleRepositoryExtensions
    {
        public static IQueryable<NewsArticle> FilterByCategory(this IQueryable<NewsArticle> newsArticles, short? categoryId)
        {
            if (categoryId == null)
            {
                return newsArticles;
            }
            return newsArticles.Where(n => n.CategoryId == categoryId);
        }

        public static IQueryable<NewsArticle> FilterByTag(this IQueryable<NewsArticle> newsArticles, string? tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
            {
                return newsArticles;
            }
            return newsArticles.Where(n => n.Tags.Any(t => t.TagName != null && t.TagName.Equals(tag)));
        }

        public static IQueryable<NewsArticle> Search(this IQueryable<NewsArticle> newsArticles, string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return newsArticles;
            }
            var lowerCaseSearch = search.ToLower().Trim();
            return newsArticles
                .Where(n => n.NewsTitle.Contains(lowerCaseSearch) ||
                n.Headline.ToLower().Contains(lowerCaseSearch) ||
                n.NewsContent.ToLower().Contains(lowerCaseSearch) ||
                n.Tags.Any(tag => tag.TagName.ToLower().Contains(lowerCaseSearch)));
        }

        public static IQueryable<NewsArticle> IncludeTags(this IQueryable<NewsArticle> newsArticles, bool includeTags)
        {
            if (!includeTags)
            {
                return newsArticles;
            }
            return newsArticles.Include(n => n.Tags);
        }

        public static IQueryable<NewsArticle> IncludeCategory(this IQueryable<NewsArticle> newsArticles, bool includeCategory)
        {
            if (!includeCategory)
            {
                return newsArticles;
            }
            return newsArticles.Include(n => n.Category);
        }

        public static IQueryable<NewsArticle> IncludeCreatedBy(this IQueryable<NewsArticle> newsArticles, bool includeCreatedBy)
        {
            if (!includeCreatedBy)
            {
                return newsArticles;
            }
            return newsArticles.Include(n => n.CreatedBy);
        }

        public static IQueryable<NewsArticle> FilterByNewsStatus(this IQueryable<NewsArticle> newsArticles, bool? newsStatus)
        {
            if (newsStatus == null)
            {
                return newsArticles;
            }
            return newsArticles.Where(n => n.NewsStatus == newsStatus);
        }

        public static IQueryable<NewsArticle> FilterByDateRange(this IQueryable<NewsArticle> newsArticles, DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate == null || toDate == null)
            {
                return newsArticles;
            }
            return newsArticles.Where(n => n.CreatedDate >= fromDate && n.CreatedDate <= toDate);
        }

        public static IQueryable<NewsArticle> ExcludeNewsArticle(this IQueryable<NewsArticle> newsArticles, string? excludeNewsArticleId)
        {
            if (string.IsNullOrWhiteSpace(excludeNewsArticleId))
            {
                return newsArticles;
            }
            return newsArticles.Where(n => n.NewsArticleId != excludeNewsArticleId);
        }

        public static IQueryable<NewsArticle> FilterByCreatedById(this IQueryable<NewsArticle> newsArticles, short? createdById)
        {
            if (createdById == null)
            {
                return newsArticles;
            }
            return newsArticles.Where(n => n.CreatedById == createdById);
        }

        public static IQueryable<NewsArticle> FilterNewsArticles(this IQueryable<NewsArticle> newsArticles, NewsArticleParameters newsArticleParameters)
        {
            return newsArticles
                .FilterByCategory(newsArticleParameters.CategoryId)
                .FilterByTag(newsArticleParameters.Tag)
                .IncludeTags(newsArticleParameters.IncludeTags)
                .IncludeCategory(newsArticleParameters.IncludeCategory)
                .IncludeCreatedBy(newsArticleParameters.IncludeCreatedBy)
                .FilterByNewsStatus(newsArticleParameters.NewsStatus)
                .FilterByDateRange(newsArticleParameters.FromDate, newsArticleParameters.ToDate)
                .FilterByCreatedById(newsArticleParameters.CreatedById)
                .ExcludeNewsArticle(newsArticleParameters.ExcludeNewsArticleId)
                .Search(newsArticleParameters.SearchTerm);
        }
    }
}
