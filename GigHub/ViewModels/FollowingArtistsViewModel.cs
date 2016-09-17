using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class FollowingArtistsViewModel
    {
        public IEnumerable<ApplicationUser> Artists { get; set; }

    }
}
