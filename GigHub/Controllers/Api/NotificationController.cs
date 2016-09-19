using AutoMapper;
using GigHub.Models;
using GigHub.Models.Dtos;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{

    [Authorize]

    public class NotificationController : ApiController
    {
        private ApplicationDbContext _context;


        public NotificationController()
        {
            _context = new ApplicationDbContext();
        }

        [Route("api/notification/")]
        public IEnumerable<NotificationDto> GetNewNotifications()
        {

            var userId = User.Identity.GetUserId();

            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }
        [HttpPost]
        [Route("api/notification/readed/")]
        public IHttpActionResult MakeAsReaded()
        {
            try
            {
                var userId = User.Identity.GetUserId();

                var notifications =
                   _context.UserNotifications.Where(un => un.UserId == userId && !un.IsRead)
                        .ToList();


                foreach (var notification in notifications)
                {
                    notification.IsRead = true;
                }


                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest("Error" + ex.Message);
            }

        }

    }
}
