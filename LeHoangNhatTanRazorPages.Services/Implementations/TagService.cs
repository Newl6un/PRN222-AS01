using AutoMapper;

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
    }
}
