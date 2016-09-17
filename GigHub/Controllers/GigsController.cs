using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Gigs
        [Authorize]
        public ActionResult Create()
        {

            var vm = new GigFormViewModel
            {
                Genres = _context.Genre.ToList()

            };

            return View(vm);
        }



        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var gigs = _context.Attendances
                .Where(a => a.AtendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var vm = new GigsViewModel
            {
                UpcomingGigs = gigs,
                Authorized = User.Identity.IsAuthenticated,
                Heading = "Gigi na którę idę!"

            };




            return View("Gigs", vm);

        }



        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(GigFormViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var gig = new Gig
                {
                    ArtistId = User.Identity.GetUserId(),
                    DateTime = vm.GetDateTime(vm.Date, vm.Time),
                    GenreId = vm.Genre,
                    Venue = vm.Venue
                };

                _context.Gigs.Add(gig);

                await _context.SaveChangesAsync();

                return RedirectToAction($"Index", $"Home");
            }
            else
            {
                vm.Genres = _context.Genre.ToList();

                return (View($"Create", vm));
            }




        }
    }
}