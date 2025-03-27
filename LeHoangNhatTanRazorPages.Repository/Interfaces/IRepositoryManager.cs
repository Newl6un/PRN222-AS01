namespace LeHoangNhatTanRazorPages.Repository.Interfaces
{
    public interface IRepositoryManager
    {
        ICategoryRepository Category { get; }
        ITagRepository Tag { get; }
        INewsArticleRepository NewsArticle { get; }
        ISystemAccountRepository SystemAccount { get; }
        Task SaveAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
