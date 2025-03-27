using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.DataAccess.Interfaces;
using LeHoangNhatTanRazorPages.Repository.Interfaces;

namespace LeHoangNhatTanRazorPages.Repository.Implementation
{
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(IBaseDAO<Tag> dao) : base(dao)
        {
        }
    }
}
