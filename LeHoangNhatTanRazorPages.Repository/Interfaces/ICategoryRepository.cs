using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

namespace LeHoangNhatTanRazorPages.Repository.Interfaces
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        public Task<PagedList<Category>> GetCategoriesAsync(CategoryParameters parameters, bool trackChanges);
    }
}
