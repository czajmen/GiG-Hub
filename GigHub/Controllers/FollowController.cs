using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class FollowController : Controller
    {
        private ApplicationDbContext _context;

        public FollowController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Follow
        public ActionResult Follow()
        {
            var userId = User.Identity.GetUserId();

            var artists = _context.Followers
                .Where(f => f.FollowerId == userId)
                .Select(a => a.Followee)
                .ToList();


            var vm = new FollowingArtistsViewModel()
            {
                Artists = artists

            };


            return View(vm);
        }
    }
}