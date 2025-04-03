using LeHoangNhatTanRazorPages.BO.Models;

namespace LeHoangNhatTanRazorPages.Repository.Interfaces
{
    public interface ISystemAccountRepository : IRepositoryBase<SystemAccount>
    {
        Task<SystemAccount?> GetAccountAsync(string email, string password, bool trackChanges);

        Task<SystemAccount?> GetAccountAsync(int accountId, bool trackChanges);
    }
}
