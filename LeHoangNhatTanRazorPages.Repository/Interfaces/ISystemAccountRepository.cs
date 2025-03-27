using LeHoangNhatTanRazorPages.BO.Models;

namespace LeHoangNhatTanRazorPages.Repository.Interfaces
{
    public interface ISystemAccountRepository : IRepositoryBase<SystemAccount>
    {
        public Task<SystemAccount?> FindSystemAccount(string email, string password, bool trackChanges);
    }
}
