using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

namespace LeHoangNhatTanRazorPages.Repository.Interfaces
{
    public interface ITagRepository : IRepositoryBase<Tag>
    {
        Task<Tag?> GetTagByNameAsync(string tagName, bool trackChanges);

        Task<(IEnumerable<Tag> existTag, IEnumerable<string> nonExistTag)> GetTagByNamesAsync(IEnumerable<string> tagNames, bool trackChanges);

        Task<PagedList<Tag>> GetTagsAsync(TagParameters parameters, bool trackChanges);

        new Task Create(Tag entity);
    }
}
