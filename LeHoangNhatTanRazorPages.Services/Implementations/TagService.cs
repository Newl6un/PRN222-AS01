using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Repository.Interfaces;
using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

namespace LeHoangNhatTanRazorPages.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly IRepositoryManager _repositoryManager;

        public TagService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public Task<Tag?> GetTag(short tagId)
        {
            throw new NotImplementedException();
        }
        public Task<PagedList<Tag>> GetTags(TagParameters tagParameters, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
