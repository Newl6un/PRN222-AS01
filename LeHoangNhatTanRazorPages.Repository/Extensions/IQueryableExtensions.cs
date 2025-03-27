using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

using Microsoft.EntityFrameworkCore;

namespace LeHoangNhatTanRazorPages.Repository.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PagedList<T>> ToPagedList<T>(this IQueryable<T> query, RequestParameters requestParameters)
        {
            var count = await query.CountAsync();
            var list = await query
                .Skip((requestParameters.PageNumber - 1) * requestParameters.PageSize)
                .Take(requestParameters.PageSize)
                .ToListAsync();

            return new PagedList<T>(list, count, requestParameters.PageNumber, requestParameters.PageSize);
        }
    }
}
