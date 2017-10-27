using AutoMapper;
using GigHub.DTOs;
using GigHub.Models;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile
    {      

        protected override void Configure()
        {
            Mapper.CreateMap<ApplicationUser, UserDto>();
            Mapper.CreateMap<Genre, GenreDto>();
            Mapper.CreateMap<Gig, GigDto>();
            Mapper.CreateMap<Notification, NotificationDto>();
        }
    }
}