namespace LeHoangNhatTanRazorPages.Services.Interfaces
{
    public interface IServiceManager
    {
        ISystemAccountService SystemAccount { get; }
        ICategoryService Category { get; }
        ITagService Tag { get; }
        INewsArticleService NewsArticle { get; }
    }
}
