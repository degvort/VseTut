using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VseTut.Core.Categories;
using VseTut.Core.Categories.Model;
using VseTut.Core.Users.Dto;
using VseTut.Core.Users.Model;

namespace VseTut.Web.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
