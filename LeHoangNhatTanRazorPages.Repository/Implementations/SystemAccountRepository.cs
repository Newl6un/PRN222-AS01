using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.DataAccess.Interfaces;
using LeHoangNhatTanRazorPages.Repository.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace LeHoangNhatTanRazorPages.Repository.Implementation
{
    public class SystemAccountRepository : RepositoryBase<SystemAccount>, ISystemAccountRepository
    {
        public SystemAccountRepository(IBaseDAO<SystemAccount> dao) : base(dao)
        {
        }

        public async Task<SystemAccount?> FindSystemAccount(string email, string password, bool trackChanges)
        {
            return await _dao.FindByCondition(x => x.AccountEmail == email && x.AccountPassword == password, trackChanges).FirstOrDefaultAsync();
        }
    }
}
