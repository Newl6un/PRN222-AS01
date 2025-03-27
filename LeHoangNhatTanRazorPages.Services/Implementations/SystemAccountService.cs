using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Repository.Interfaces;
using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

namespace LeHoangNhatTanRazorPages.Services.Implementations
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly IRepositoryManager _repositoryManager;

        public SystemAccountService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<SystemAccount?> FindSystemAccount(string email, string password)
        {
            return await _repositoryManager.SystemAccount.FindSystemAccount(email, password, false);
        }

        public Task<SystemAccount?> GetSystemAccount(short accountId)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<SystemAccount>> GetSystemAccounts(SystemAccountParameters systemAccountParameters, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
