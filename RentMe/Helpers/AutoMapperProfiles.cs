using AutoMapper;
using RentMe.Models;
using RentMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegister, User>();
            CreateMap<User, UserProfileDetails>();
            CreateMap<User, UserWithRoles>()
                .ForMember(
                    dest => dest.Roles,
                    opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name).ToArray<string>())
                );

            CreateMap<Photo, PhotoForDetailed>();
            CreateMap<PhotoForCreation, Photo>();
            CreateMap<Photo, PhotoForReturn>();
            CreateMap<AnnouncementForAdd, Announcement>();
            CreateMap<Announcement, AnnouncementWithDetails>()
                .ForMember(
                    dest => dest.PostedByName,
                    opt => opt.MapFrom(src => src.PostedBy.UserName)
                );


        }
    }
}
