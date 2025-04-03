using System.Linq.Expressions;

namespace LeHoangNhatTanRazorPages.DataAccess.Interfaces
{
    public interface IBaseDAO<T> where T : class
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> GetAll(bool trackChanges);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        Task<T?> GetByIdAsync(bool trackChanges, params object[] keyValues);
    }

}
