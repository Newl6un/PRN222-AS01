using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

using Microsoft.EntityFrameworkCore;

using System.Linq.Dynamic.Core;

namespace LeHoangNhatTanRazorPages.Repository.Extensions
{
    public static class TagRepositoryExtensions
    {
        public static IQueryable<Tag> Search(this IQueryable<Tag> query, string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return query;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(t => t.TagName.ToLower().Contains(lowerCaseSearchTerm) ||
            t.Note.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Tag> IsIncludeNews(this IQueryable<Tag> query, bool isIncludeNews)
        {
            if (!isIncludeNews)
                return query;
            return query.Include(t => t.NewsArticles);
        }

        public static IQueryable<Tag> HasMostNews(this IQueryable<Tag> tags, bool hasMostNews)
        {
            if (!hasMostNews)
                return tags;
            return tags.OrderByDescending(tag => tag.NewsArticles.Count());
        }

        public static IQueryable<Tag> FilterTag(this IQueryable<Tag> query, TagParameters parameters)
        {
            return query
                .HasMostNews(parameters.HasMostNews)
                .Search(parameters.SearchTerm)
                .IsIncludeNews(parameters.IsIncludeNews);
        }


    }
}
