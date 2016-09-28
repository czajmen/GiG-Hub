using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Models;

namespace GigHub.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                  .Where(a => a.AttendeeId == userId
                   && a.Gig.DateTime > DateTime.Now)
                  .ToList();
        }


        public IEnumerable<ApplicationUser> GetAttendancesByGig(Gig gig)
        {
            return _context.Attendances
                .Where(a => a.GigId == gig.Id)
                .Select(a => a.Attendee)
                .ToList();
        }


        public bool GetUserAttendingConfirm(int Gigid, string userId)
        {
            return _context.Attendances.Where(g => g.Gig.Id == Gigid).Any(g => g.Attendee.Id == userId);
        }


    }
}