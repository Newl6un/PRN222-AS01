using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

namespace LeHoangNhatTanRazorPages.Services.Interfaces
{
    public interface ISystemAccountService
    {
        Task<SystemAccount?> FindSystemAccount(string email, string password);

        Task<SystemAccount?> GetSystemAccount(short accountId);

        Task<PagedList<SystemAccount>> GetSystemAccounts(SystemAccountParameters systemAccountParameters, bool trackChanges);
    }
}
