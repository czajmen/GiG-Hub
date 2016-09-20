using AutoMapper;
using GigHub.Models;
using GigHub.Models.Dtos;

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
            

            //Żeby działało do global.asax.cs trzeba dodać wpis o inicjalizacji

        }
    }
}