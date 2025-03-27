using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.DataAccess.Interfaces;
using LeHoangNhatTanRazorPages.Repository.Interfaces;

namespace LeHoangNhatTanRazorPages.Repository.Implementation
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IBaseDAO<Category> dao) : base(dao)
        {
        }
    }
}
