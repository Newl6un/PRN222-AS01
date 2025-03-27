using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

namespace LeHoangNhatTanRazorPages.Repository.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        void Create(T entity);

        void Delete(T entity);

        void Update(T entity);

        Task<T?> FindByIdAsync(bool trackChanges, params object[] keyValues);

        Task<IEnumerable<T>> FindAll(bool trackChanges);

        Task<PagedList<T>> FindAll(RequestParameters requestParameters, bool trackChanges);
    }
}
