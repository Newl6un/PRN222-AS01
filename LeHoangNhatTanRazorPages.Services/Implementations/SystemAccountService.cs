using AutoMapper;

using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Repository.Interfaces;
using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Account;

namespace LeHoangNhatTanRazorPages.Services.Implementations
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public SystemAccountService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<bool> ChangePasswordAsync(short userId, string currentPassword, string newPassword)
        {
            var account = await _repositoryManager.SystemAccount.GetByIdAsync(trackChanges: true, userId);
            if (account == null)
                return false;

            // Kiểm tra mật khẩu hiện tại
            if (!account.AccountPassword!.Equals(currentPassword))
            {
                throw new Exception();
            }

            // Cập nhật mật khẩu mới
            account.AccountPassword = newPassword;
            await _repositoryManager.SaveAsync();
            return true;
        }

        public async Task CreateAccountAsync(AccountViewModel accountViewModel)
        {
            var account = _mapper.Map<SystemAccount>(accountViewModel);

            _repositoryManager.SystemAccount.Create(account);

            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteAccountAsync(short accountId)
        {
            var account = await _repositoryManager.SystemAccount.GetAccountAsync(accountId, true);
            if (account == null)
            {
                throw new Exception("Tài khoản không tồn tại");
            }
            _repositoryManager.SystemAccount.Delete(account);
            await _repositoryManager.SaveAsync();
        }

        public async Task<AccountViewModel?> GetAccountAsync(short accountId, bool trackChanges)
        {
            var account = await _repositoryManager.SystemAccount.GetAccountAsync(accountId, trackChanges);

            var accountViewModel = _mapper.Map<AccountViewModel>(account);

            return accountViewModel;
        }

        public async Task<PagedList<AccountViewModel>> GetAccountsAsync(SystemAccountParameters systemAccountParameters, bool trackChanges)
        {
            var account = await _repositoryManager.SystemAccount.GetPagedAsync(systemAccountParameters, trackChanges);

            var accountViewModel = _mapper.Map<IEnumerable<AccountViewModel>>(account);

            return PagedList<AccountViewModel>.ToPagedList(accountViewModel, account.MetaData);
        }

        public async Task<ProfileViewModel?> GetProfileAsync(int accountId, bool trackChanges)
        {
            var account = await _repositoryManager.SystemAccount.GetAccountAsync(accountId, trackChanges);

            var profileViewModel = _mapper.Map<ProfileViewModel>(account);

            return profileViewModel;
        }

        public async Task UpdateAccountAsync(AccountViewModel accountViewModel)
        {
            var account = await _repositoryManager.SystemAccount.GetAccountAsync(accountViewModel.AccountId, true);
            if (account == null)
            {
                throw new Exception("Tài khoản không tồn tại");
            }

            _mapper.Map(accountViewModel, account);

            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateProfileAsync(ProfileViewModel profileViewModel)
        {
            var account = await _repositoryManager.SystemAccount.GetAccountAsync(profileViewModel.AccountId, true);

            if (account == null)
            {
                throw new Exception("Tài khoản không tồn tại");
            }

            _mapper.Map(profileViewModel, account);

            _repositoryManager.SystemAccount.Update(account);
            await _repositoryManager.SaveAsync();
        }


        public async Task<AccountViewModel?> ValidateUserAsync(string email, string password, bool trackChanges)
        {
            var account = await _repositoryManager.SystemAccount.GetAccountAsync(email, password, trackChanges);

            var accountViewModel = _mapper.Map<AccountViewModel>(account);

            return accountViewModel;
        }
    }
}
