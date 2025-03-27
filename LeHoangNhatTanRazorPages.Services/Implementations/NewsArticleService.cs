using AutoMapper;

using LeHoangNhatTanRazorPages.Repository.Interfaces;
using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.DataTransferObjects.NewsArticle;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

namespace LeHoangNhatTanRazorPages.Services.Implementations
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public NewsArticleService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public Task<NewsArticleDto?> GetNewsArticle(short newsArticleId)
        {
            throw new NotImplementedException();
        }
        public async Task<PagedList<NewsArticleDto>> GetNewsArticles(NewsArticleParameters newsArticleParameters, bool trackChanges)
        {
            var newsArticles = await _repositoryManager.NewsArticle.GetNewsArticles(newsArticleParameters, trackChanges);

            var newsArticleDtos = _mapper.Map<IEnumerable<NewsArticleDto>>(newsArticles);

            return new PagedList<NewsArticleDto>(newsArticleDtos, newsArticles.MetaData);
        }
    }
}
