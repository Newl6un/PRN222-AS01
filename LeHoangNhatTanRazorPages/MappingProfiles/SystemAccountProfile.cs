using AutoMapper;

using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Account;

namespace LeHoangNhatTanRazorPages.MappingProfiles
{
    public class SystemAccountProfile : Profile
    {
        public SystemAccountProfile()
        {
            CreateMap<SystemAccount, AccountViewModel>();
            CreateMap<AccountViewModel, SystemAccount>();

            CreateMap<SystemAccount, ProfileViewModel>()
                .ForMember(dest => dest.NewsCount, opts =>
                {
                    opts.PreCondition(src => src.NewsArticles != null);
                    opts.MapFrom(src => src.NewsArticles.Count);
                });

            CreateMap<ProfileViewModel, SystemAccount>()
                .ForMember(dest => dest.AccountRole, opt => opt.Ignore());
        }
    }
}
