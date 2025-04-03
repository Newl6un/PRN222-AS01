using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Tag;

namespace LeHoangNhatTanRazorPages.Services.Interfaces
{
    public interface ITagService
    {
        public Task<TagViewModel?> GetTagByNameAsync(string tagName, bool trackChanges);
        public Task<PagedList<TagViewModel>> GetTagsAsync(TagParameters parameters, bool trackChanges);
    }
}
