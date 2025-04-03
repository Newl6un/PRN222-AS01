using AutoMapper;

using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Tag;

namespace LeHoangNhatTanRazorPages.MappingProfiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagViewModel>();

            CreateMap<TagViewModel, Tag>();
        }
    }
}
