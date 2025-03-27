using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Repository.Interfaces;
using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

namespace LeHoangNhatTanRazorPages.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;

        public CategoryService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public Task<Category?> GetCategory(short categoryId)
        {
            throw new NotImplementedException();
        }
        public Task<PagedList<Category>> GetCategories(CategoryParameters categoryParameters, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
