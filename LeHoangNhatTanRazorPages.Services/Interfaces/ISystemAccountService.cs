using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Account;

namespace LeHoangNhatTanRazorPages.Services.Interfaces
{
    public interface ISystemAccountService
    {
        Task<AccountViewModel?> ValidateUserAsync(string email, string password, bool trackChanges);

        Task<ProfileViewModel?> GetProfileAsync(int accountId, bool trackChanges);

        Task UpdateProfileAsync(ProfileViewModel profileViewModel);

        Task<bool> ChangePasswordAsync(short userId, string currentPassword, string newPassword);

        Task<PagedList<AccountViewModel>> GetAccountsAsync(SystemAccountParameters systemAccountParameters, bool trackChanges);

        Task<AccountViewModel?> GetAccountAsync(short accountId, bool trackChanges);

        Task CreateAccountAsync(AccountViewModel accountViewModel);

        Task UpdateAccountAsync(AccountViewModel accountViewModel);

        Task DeleteAccountAsync(short accountId);
    }
}
