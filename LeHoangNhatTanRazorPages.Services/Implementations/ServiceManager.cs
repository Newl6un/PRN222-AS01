using LeHoangNhatTanRazorPages.Services.Interfaces;

namespace LeHoangNhatTanRazorPages.Services.Implementations
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<ISystemAccountService> _systemAccountService;
        private readonly Lazy<ITagService> _tagService;
        private readonly Lazy<INewsArticleService> _newsArticleService;
        private readonly Lazy<IReportService> _reportService;

        public ServiceManager(
            Lazy<ICategoryService> categoryService,
            Lazy<ISystemAccountService> systemAccountService,
            Lazy<ITagService> tagService,
            Lazy<INewsArticleService> newsArticleService,
            Lazy<IReportService> reportService)
        {
            _categoryService = categoryService;
            _systemAccountService = systemAccountService;
            _tagService = tagService;
            _newsArticleService = newsArticleService;
            _reportService = reportService;
        }

        public ISystemAccountService SystemAccount => _systemAccountService.Value;
        public ICategoryService Category => _categoryService.Value;
        public ITagService Tag => _tagService.Value;
        public INewsArticleService NewsArticle => _newsArticleService.Value;
        public IReportService Report => _reportService.Value;
    }
}
