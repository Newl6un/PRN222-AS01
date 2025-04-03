using AutoMapper;

using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Shared.ViewModels.News;

namespace LeHoangNhatTanRazorPages.MappingProfiles
{
    public class NewsArticleProfile : Profile
    {
        public NewsArticleProfile()
        {
            CreateMap<NewsArticle, NewsArticleViewModel>()
                .ForMember(src => src.CategoryName, opts =>
                {
                    opts.PreCondition(src => src.Category != null);
                    opts.MapFrom(src => src.Category!.CategoryName);
                })
                .ForMember(src => src.CreatorName, opts =>
                {
                    opts.PreCondition(src => src.CreatedBy != null);
                    opts.MapFrom(src => src.CreatedBy!.AccountName);
                });

            CreateMap<NewsArticleViewModel, NewsArticle>();
        }
    }
}
