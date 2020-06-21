using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VseTut.Core.Categories;
using VseTut.Core.Categories.Model;
using VseTut.Core.SubCategories.Dto;
using VseTut.Core.SubCategories.Model;
using VseTut.Core.Users.Dto;
using VseTut.Core.Users.Model;

namespace VseTut.Web.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<Category, CategoryDto>()
                .ForMember(x => x.SubCategories, options => options.MapFrom(x => x.SubCategories));
            CreateMap<CategoryDto, Category>();

            CreateMap<SubCategory, SubCategoryDto>().ReverseMap();
        }
    }
}
