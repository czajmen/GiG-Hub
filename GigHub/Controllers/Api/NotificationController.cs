using AutoMapper;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{

    [Authorize]
    [Route("api/notification/")]
    public class NotificationController : ApiController
    {
        private ApplicationDbContext _context;


        public NotificationController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {

            var userId = User.Identity.GetUserId();

            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();


         

       


            return notifications.Select(Mapper.Map<Notification, NotificationDto>);

            //return notifications.Select(n => new NotificationDto()
            //{
            //    DateTime = n.DateTime,
            //    Gig = new GigDto()
            //    {
            //        Artist = new ApplicationUserDto()
            //        {
            //            Id = n.Gig.Artist.Id,
            //            Name = n.Gig.Artist.Name
            //        },
            //        DateTime = n.Gig.DateTime,
            //        Id = n.Gig.Id,
            //        IsCanceled = n.Gig.IsCanceled,
            //        Venue = n.Gig.Venue
            //    },
            //    OrigialDateTime = n.OrigialDateTime,
            //    OriginalVenue = n.OriginalVenue,
            //    Type = n.Type
            //});
        }

    }
}
