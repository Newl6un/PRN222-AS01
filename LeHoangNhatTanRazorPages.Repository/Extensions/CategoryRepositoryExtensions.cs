using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

using Microsoft.EntityFrameworkCore;

using System.Linq.Dynamic.Core;

namespace LeHoangNhatTanRazorPages.Repository.Extensions
{
    public static class CategoryRepositoryExtensions
    {
        public static IQueryable<Category> FilterByStatus(this IQueryable<Category> categories, bool isActive)
        {
            if (!isActive) return categories;
            return categories.Where(c => c.IsActive == isActive);
        }

        public static IQueryable<Category> Search(this IQueryable<Category> categories, string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return categories;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return categories.Where(c => c.CategoryName.ToLower().Contains(lowerCaseSearchTerm) || c.CategoryDesciption.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Category> IncludeNewsArticles(this IQueryable<Category> categories, bool includeNewsArticles)
        {
            if (!includeNewsArticles) return categories;
            return categories.Include(c => c.NewsArticles);
        }

        public static IQueryable<Category> IncludeSubCategories(this IQueryable<Category> categories, bool includeSubCategories)
        {
            if (!includeSubCategories) return categories;
            return categories.Include(c => c.InverseParentCategory);
        }

        public static IQueryable<Category> IncludeParentCategory(this IQueryable<Category> categories, bool includeParentCategory)
        {
            if (!includeParentCategory) return categories;
            return categories.Include(c => c.ParentCategory);
        }

        public static IQueryable<Category> FilterByParentCategoryId(this IQueryable<Category> categories, short? parentCategoryId)
        {
            if (parentCategoryId == null) return categories;
            return categories.Where(c => c.ParentCategoryId == parentCategoryId);
        }

        public static IQueryable<Category> HasMostPost(this IQueryable<Category> categories, bool hasMostPost)
        {
            if (!hasMostPost) return categories;
            return categories.OrderByDescending(category => category.NewsArticles.Count());
        }

        public static IQueryable<Category> FilterCategories(this IQueryable<Category> categories, CategoryParameters parameters)
        {
            return categories
                .FilterByStatus(parameters.IsActive)
                .Search(parameters.SearchTerm)
                .FilterByParentCategoryId(parameters.ParentCategoryId)
                .HasMostPost(parameters.HasMostPost)
                .IncludeNewsArticles(parameters.IncludeNewsArticles)
                .IncludeSubCategories(parameters.IncludeSubCategories)
                .IncludeParentCategory(parameters.IncludeParentCategory);
        }

    }
}
