using AutoMapper;

using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Shared.DataTransferObjects.NewsArticle;

namespace LeHoangNhatTanRazorPages.MappingProfiles
{
    public class NewsArticleProfile : Profile
    {
        public NewsArticleProfile()
        {
            CreateMap<NewsArticle, NewsArticleDto>();
        }

    }
}
