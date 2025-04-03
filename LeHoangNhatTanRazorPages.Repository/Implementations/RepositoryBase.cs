using LeHoangNhatTanRazorPages.DataAccess.Interfaces;
using LeHoangNhatTanRazorPages.Repository.Extensions;
using LeHoangNhatTanRazorPages.Repository.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

using Microsoft.EntityFrameworkCore;

namespace LeHoangNhatTanRazorPages.Repository.Implementation
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly IBaseDAO<T> _dao;

        protected RepositoryBase(IBaseDAO<T> dao)
        {
            _dao = dao;
        }

        public void Create(T entity)
        {
            _dao.Create(entity);
        }

        public void Delete(T entity)
        {
            _dao.Delete(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges)
        {
            return await _dao.GetAll(trackChanges).ToListAsync();
        }

        public async Task<PagedList<T>> GetPagedAsync(RequestParameters requestParameters, bool trackChanges)
        {
            return await _dao.GetAll(trackChanges).ToPagedListAsync(requestParameters);
        }

        public async Task<T?> GetByIdAsync(bool trackChanges, params object[] keyValues)
        {
            return await _dao.GetByIdAsync(trackChanges, keyValues);
        }

        public void Update(T entity)
        {
            _dao.Update(entity);
        }
    }
}
