using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool Authorized { get; set; }
        public string Heading { get; set; }
        public string Search { get; set; }
        public ILookup<int,Attendance> Attendances { get; internal set; }


    }
}