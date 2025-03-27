using System.Linq.Expressions;

namespace LeHoangNhatTanRazorPages.DataAccess.Interfaces
{
    public interface IBaseDAO<T> where T : class
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        Task<T?> FindByIdAsync(bool trackChanges, params object[] keyValues);
    }

}
