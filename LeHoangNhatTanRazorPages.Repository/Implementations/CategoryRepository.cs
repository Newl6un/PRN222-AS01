using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.DataAccess.Interfaces;
using LeHoangNhatTanRazorPages.Repository.Extensions;
using LeHoangNhatTanRazorPages.Repository.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

namespace LeHoangNhatTanRazorPages.Repository.Implementation
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IBaseDAO<Category> dao) : base(dao)
        {
        }

        public async Task<PagedList<Category>> GetCategoriesAsync(CategoryParameters parameters, bool trackChanges)
        {
            var pagedCategories = await _dao.GetAll(trackChanges)
                .FilterCategories(parameters)
                .Sort(parameters.OrderBy)
                .ToPagedListAsync(parameters);

            return pagedCategories;
        }
    }
}
