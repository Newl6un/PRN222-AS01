using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

namespace LeHoangNhatTanRazorPages.Services.Interfaces
{
    public interface ITagService
    {
        Task<Tag?> GetTag(short tagId);
        Task<PagedList<Tag>> GetTags(TagParameters tagParameters, bool trackChanges);
    }
}
