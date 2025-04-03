using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.DataAccess.Interfaces;
using LeHoangNhatTanRazorPages.Repository.Extensions;
using LeHoangNhatTanRazorPages.Repository.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

using Microsoft.EntityFrameworkCore;

namespace LeHoangNhatTanRazorPages.Repository.Implementation
{
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(IBaseDAO<Tag> dao) : base(dao)
        {
        }

        public async Task<Tag?> GetTagByNameAsync(string tagName, bool trackChanges)
        {
            var tag = await _dao.GetByCondition(t => t.TagName == tagName, trackChanges).FirstOrDefaultAsync();

            return tag;
        }

        public async Task<(IEnumerable<Tag> existTag, IEnumerable<string> nonExistTag)> GetTagByNamesAsync(IEnumerable<string> tagNames, bool trackChanges)
        {
            var tagNameNormalizeList = tagNames.Select(name => name.Trim().ToLower()).ToList();

            // Lấy các tag tồn tại trong DB
            var existTags = await _dao.GetByCondition(
                t => tagNameNormalizeList.Contains(t.TagName.ToLower().Trim()), trackChanges
            ).ToListAsync();

            // Lấy danh sách tên tag đã tồn tại (để so sánh)
            var existTagNames = existTags.Select(t => t.TagName.Trim().ToLower()).ToHashSet();

            // Lọc ra các tag chưa tồn tại
            var nonExistTags = tagNameNormalizeList
                .Where(name => !existTagNames.Contains(name))
                .Distinct()
                .ToList();

            return (existTags, nonExistTags);
        }

        public async Task<PagedList<Tag>> GetTagsAsync(TagParameters parameters, bool trackChanges)
        {
            var tags = await _dao.GetAll(trackChanges)
                .FilterTag(parameters)
                .Sort(parameters.OrderBy)
                .ToPagedListAsync(parameters);

            return tags;
        }

        public new async Task Create(Tag entity)
        {
            var lastestTag = await _dao.GetAll(false)
                .OrderByDescending(t => t.TagId)
                .FirstOrDefaultAsync();

            entity.TagId = lastestTag?.TagId + 1 ?? 1;
            base.Create(entity);
        }
    }
}
