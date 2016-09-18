using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

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
