using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

namespace LeHoangNhatTanRazorPages.Repository.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        void Create(T entity);

        void Delete(T entity);

        void Update(T entity);

        Task<T?> GetByIdAsync(bool trackChanges, params object[] keyValues);

        Task<IEnumerable<T>> GetAllAsync(bool trackChanges);

        Task<PagedList<T>> GetPagedAsync(RequestParameters requestParameters, bool trackChanges);
    }
}
