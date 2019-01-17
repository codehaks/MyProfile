using AutoMapper;
using MyProfile.Models;
using MyProfile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProfile.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<string, string>().ConvertUsing(new StringTrimmer());
            CreateMap<GenderType, string>().ConvertUsing(new GenderMapper());

            CreateMap<UserAddModel, User>();
        }
    }
}
