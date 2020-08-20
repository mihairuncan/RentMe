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
                )
                .ForMember(
                    dest => dest.City,
                    opt => opt.MapFrom(src => src.PostedBy.City)
                )
                .ForMember(
                    dest => dest.PhoneNumber,
                    opt => opt.MapFrom(src => src.PostedBy.PhoneNumber)
                );

            CreateMap<Announcement, AnnouncementForList>()
                .ForMember(
                    dest => dest.SubcategoryName,
                    opt => opt.MapFrom(src => src.Subcategory.DisplayName)
                )
                .ForMember(
                    dest => dest.MainPhotoUrl,
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain == true).Url)
                )
                .ForMember(
                    dest => dest.City,
                    opt => opt.MapFrom(src => src.PostedBy.City)
                );

            CreateMap<MessageForCreation, Message>().ReverseMap();
            CreateMap<Message, MessageToReturn>();
            CreateMap<Message, MessageForList>();

        }
    }
}
