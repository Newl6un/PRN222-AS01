using LeHoangNhatTanRazorPages.DataAccess.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace LeHoangNhatTanRazorPages.DataAccess.Implementations
{

    public class BaseDAO<Entity> : IBaseDAO<Entity> where Entity : class
    {
        protected FUNewsManagementContext _fUNewsManagementContext { get; set; }

        public BaseDAO(FUNewsManagementContext fUNewsManagementContext)
        {
            _fUNewsManagementContext = fUNewsManagementContext;
        }

        public async Task<Entity?> GetByIdAsync(bool trackChanges, params object[] keyValues)
        {
            var entity = await _fUNewsManagementContext.FindAsync<Entity>(keyValues);
            if (!trackChanges && entity != null)
                _fUNewsManagementContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        public IQueryable<Entity> GetAll(bool trackChanges)
        => !trackChanges ?
             _fUNewsManagementContext.Set<Entity>()
             .AsNoTracking() :
             _fUNewsManagementContext.Set<Entity>();


        public IQueryable<Entity> GetByCondition(Expression<Func<Entity, bool>> expression, bool trackChanges)
        => !trackChanges ?
            _fUNewsManagementContext.Set<Entity>()
            .Where(expression)
            .AsNoTracking() :
            _fUNewsManagementContext.Set<Entity>()
            .Where(expression);

        public void Create(Entity entity) => _fUNewsManagementContext.Set<Entity>().Add(entity);
        public void Update(Entity entity) => _fUNewsManagementContext.Set<Entity>().Update(entity);
        public void Delete(Entity entity) => _fUNewsManagementContext.Set<Entity>().Remove(entity);
    }
}
