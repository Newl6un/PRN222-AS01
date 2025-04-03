using AutoMapper;

using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Category;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeHoangNhatTanRazorPages.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategorySelect, SelectListItem>()
                .ForMember(dest => dest.Value, opts =>
                {
                    opts.MapFrom(src => src.CategoryId.ToString());
                })
                .ForMember(dest => dest.Text, opts =>
                {
                    opts.MapFrom(src => src.Level <= 1 ? src.CategoryName : string.Concat(Enumerable.Repeat("-- ", src.Level - 1)) + src.CategoryName);
                });

            CreateMap<Category, CategorySelect>();

            CreateMap<Category, CategoryViewModel>()
                .ForMember(dest => dest.ParentCategoryName, opts =>
                {
                    opts.PreCondition(src => src.ParentCategory != null);
                    opts.MapFrom(src => src.ParentCategory!.CategoryName);
                })
                .ForMember(dest => dest.NewsCount, opts =>
                {
                    opts.PreCondition(src => src.NewsArticles != null);
                    opts.MapFrom(src => src.NewsArticles.Count);
                })
                .ForMember(dest => dest.SubCategories, opts =>
                {
                    opts.PreCondition(src => src.InverseParentCategory != null);
                    opts.MapFrom(src => src.InverseParentCategory);
                });
            ;

            CreateMap<CategoryViewModel, Category>()
                .ForMember(dest => dest.ParentCategoryId, opts =>
                {
                    opts.PreCondition(src => src.ParentCategoryId != null && src.ParentCategoryId != 0);
                    opts.MapFrom(src => src.ParentCategoryId);
                });
        }
    }
}
