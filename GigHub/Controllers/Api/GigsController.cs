﻿using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;



        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCanceled)
            {
                return NotFound();
            }

            gig.IsCanceled = true;



            /////////////////// notification

            var notification = new Notification(gig, NotificationType.GigCanceled);


            var attendes = _context.Attendances
                .Where(a => a.GigId == gig.Id)
                .Select(a => a.Attendee)
                .ToList();

            foreach (var user in attendes)
            {
                user.Notify(notification);
            }

            ////////////////////////////

            _context.SaveChanges();

            return Ok();
        }
    }
}
