using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;
using GigHub.Models.Dtos;

namespace GigHub.Controllers.Api
{
    public class AttendancesController : ApiController
    {

        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();

        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(AttendanceDto dto)
        {

            var userId = User.Identity.GetUserId();

            if (userId == null)
                return Unauthorized();

            if (_context.Attendances.SingleOrDefault(a => a.Gig.Id == dto.GigId)== null)
            {
                return NotFound();
            }

            try
            {
                var attendance = _context.Attendances.SingleOrDefault(a => a.AttendeeId == userId && a.Gig.Id == dto.GigId);

                if (attendance == null)
                    return NotFound();

                _context.Attendances.Remove(attendance);
                _context.SaveChanges();
            
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }


        [Authorize]
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            try
            {
                var userId = User.Identity.GetUserId();

                if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId))
                {
                    return BadRequest("Już obserwujesz to wydarzenie!");
                }


                var attendance = new Attendance()
                {
                    GigId = dto.GigId,
                    AttendeeId = userId
                };

                _context.Attendances.Add(attendance);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest("błąd" + ex.Message);
            }

        }
    }
}
