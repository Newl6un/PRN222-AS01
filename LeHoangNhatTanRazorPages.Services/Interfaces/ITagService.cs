using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Tag;

namespace LeHoangNhatTanRazorPages.Services.Interfaces
{
    public interface ITagService
    {
        public Task<TagViewModel?> GetTagByNameAsync(string tagName, bool trackChanges);
        public Task<PagedList<TagViewModel>> GetTagsAsync(TagParameters parameters, bool trackChanges);

        public Task<TagViewModel?> GetTagAsync(int tagId, bool trackChanges);

        public Task CreateTagAsync(TagViewModel tag);

        public Task UpdateTagAsync(TagViewModel tag);

        public Task DeleteTagAsync(int tagId);
    }
}
