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

        public async Task<SystemAccount?> GetAccountAsync(string email, string password, bool trackChanges)
        {
            var account = await _dao.GetByCondition(x => x.AccountEmail == email && x.AccountPassword == password, trackChanges).FirstOrDefaultAsync();

            return account;
        }

        public async Task<SystemAccount?> GetAccountAsync(int accountId, bool trackChanges)
        {
            var account = await _dao.GetByCondition(x => x.AccountId == accountId, trackChanges)
                .Include(a => a.NewsArticles)
                .FirstOrDefaultAsync();

            return account;
        }
    }
}
