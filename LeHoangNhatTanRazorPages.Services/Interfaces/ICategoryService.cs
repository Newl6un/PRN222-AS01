using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Category;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeHoangNhatTanRazorPages.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<PagedList<CategoryViewModel>> GetCategoriesAsync(CategoryParameters parameters, bool trackChanges);

        public Task<CategoryViewModel> GetCategoryAsync(short categoryId, bool trackChanges);

        public Task<IEnumerable<SelectListItem>> GetParentCategorySelectListAsync(short? parentId = null);

        Task CreateCategoryAsync(CategoryViewModel categoryViewModel);

        Task UpdateCategoryAsync(CategoryViewModel categoryViewModel);

        Task DeleteCategoryAsync(short categoryId);
    }
}
