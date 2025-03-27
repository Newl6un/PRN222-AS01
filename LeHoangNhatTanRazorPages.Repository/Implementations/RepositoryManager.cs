using LeHoangNhatTanRazorPages.DataAccess;
using LeHoangNhatTanRazorPages.Repository.Interfaces;

namespace LeHoangNhatTanRazorPages.Repository.Implementation
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly FUNewsManagementContext _context;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<ITagRepository> _tagRepository;
        private readonly Lazy<INewsArticleRepository> _newsArticleRepository;
        private readonly Lazy<ISystemAccountRepository> _systemAccountRepository;
        public RepositoryManager(
            Lazy<ICategoryRepository> categoryRepository,
            Lazy<ITagRepository> tagRepository,
            Lazy<INewsArticleRepository> newsArticleRepository,
            Lazy<ISystemAccountRepository> systemAccountRepository,
            FUNewsManagementContext context)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _newsArticleRepository = newsArticleRepository;
            _systemAccountRepository = systemAccountRepository;

        }
        public ICategoryRepository Category => _categoryRepository.Value;
        public ITagRepository Tag => _tagRepository.Value;
        public INewsArticleRepository NewsArticle => _newsArticleRepository.Value;
        public ISystemAccountRepository SystemAccount => _systemAccountRepository.Value;
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            await _context.Database.CurrentTransaction!.CommitAsync();
        }
        public async Task RollbackTransactionAsync()
        {
            await _context.Database.CurrentTransaction!.RollbackAsync();
        }
    }
}
