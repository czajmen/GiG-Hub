using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        IEnumerable<ApplicationUser> GetAttendancesByGig(Gig gig);
        bool GetUserAttendingConfirm(int Gigid, string userId);
    }
}