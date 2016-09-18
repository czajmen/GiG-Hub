using AutoMapper;
using GigHub.Models;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile

    {
        public MappingProfile()
        {
         
                CreateMap<ApplicationUser, ApplicationUserDto>();
                CreateMap<Genre, GenreDto>();
                CreateMap<Gig, GigDto>();
                CreateMap<Notification, NotificationDto>();
         
        }
    }
}