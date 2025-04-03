using AutoMapper;

using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Repository.Interfaces;
using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Tag;

namespace LeHoangNhatTanRazorPages.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public TagService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task CreateTagAsync(TagViewModel tag)
        {
            var tagEntity = _mapper.Map<Tag>(tag);

            await _repositoryManager.Tag.Create(tagEntity);

            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteTagAsync(int tagId)
        {
            var tag = await _repositoryManager.Tag.GetByIdAsync(true, tagId);
            if (tag is null)
                throw new Exception($"Tag with id {tagId} not found");

            _repositoryManager.Tag.Delete(tag);

            await _repositoryManager.SaveAsync();
        }

        public async Task<TagViewModel?> GetTagAsync(int tagId, bool trackChanges)
        {
            var tag = await _repositoryManager.Tag.GetByIdAsync(trackChanges, tagId);

            if (tag is null)
                throw new Exception($"Tag with id {tagId} not found");

            var tagViewModel = _mapper.Map<TagViewModel>(tag);

            return tagViewModel;
        }

        public async Task<TagViewModel?> GetTagByNameAsync(string tagName, bool trackChanges)
        {
            var tag = await _repositoryManager.Tag.GetTagByNameAsync(tagName, trackChanges);

            var tagViewModel = _mapper.Map<TagViewModel>(tag);

            return tagViewModel;
        }

        public async Task<PagedList<TagViewModel>> GetTagsAsync(TagParameters parameters, bool trackChanges)
        {
            var tags = await _repositoryManager.Tag.GetTagsAsync(parameters, trackChanges);

            var tagsViewModel = _mapper.Map<IEnumerable<TagViewModel>>(tags);

            return PagedList<TagViewModel>.ToPagedList(tagsViewModel, tags.MetaData);
        }

        public async Task UpdateTagAsync(TagViewModel tag)
        {
            var tagEntity = await _repositoryManager.Tag.GetByIdAsync(true, tag.TagId);

            if (tagEntity is null)
                throw new Exception($"Tag with id {tag.TagId} not found");

            _mapper.Map(tag, tagEntity);

            await _repositoryManager.SaveAsync();
        }
    }
}
