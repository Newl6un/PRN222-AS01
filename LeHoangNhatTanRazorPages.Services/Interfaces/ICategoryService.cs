using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

namespace LeHoangNhatTanRazorPages.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category?> GetCategory(short categoryId);
        Task<PagedList<Category>> GetCategories(CategoryParameters categoryParameters, bool trackChanges);
    }
}
