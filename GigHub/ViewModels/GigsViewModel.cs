using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool Authorized { get; set; }
        public string Heading { get; set; }
        public string Search { get; set; }
    }
}