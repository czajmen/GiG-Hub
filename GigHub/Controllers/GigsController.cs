using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Models.Dtos;

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
                Genres = _context.Genre.ToList(),
                Heading = "Zaplanuj nowy koncert!"


            };

            return View("GigForm", vm);
        }


        [Authorize]
        public ActionResult Edit(int id)
        {

            var userId = User.Identity.GetUserId();

            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            var vm = new GigFormViewModel
            {
                Id = gig.Id,
                Genres = _context.Genre.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edytuj Dane o koncercie"

            };

            return View("GigForm", vm);
        }




        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var vm = new GigsViewModel
            {
                UpcomingGigs = gigs,
                Authorized = User.Identity.IsAuthenticated,
                Heading = "Koncerty na którę idę!"

            };

            return View("Gigs", vm);

        }

        [Authorize]
        public ActionResult Mine()
        {

            var userId = User.Identity.GetUserId();

            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId &&
                         g.DateTime > DateTime.Now &&
                         g.IsCanceled == false)
                .Include(g => g.Genre)
                .ToList();




            return View(gigs);
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

                return RedirectToAction($"Mine", $"Gigs");
            }
            else
            {
                vm.Genres = _context.Genre.ToList();

                return (View("GigForm", vm));
            }




        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Update(GigFormViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var gig = _context.Gigs.Single(g => g.Id == vm.Id && g.ArtistId == userId);


                var notification = new Notification(gig, NotificationType.GigUpdated);

                notification.OrigialDateTime = gig.DateTime;
                notification.OriginalVenue = gig.Venue;



                gig.Venue = vm.Venue;
                gig.DateTime = vm.GetDateTime(vm.Date, vm.Time);
                gig.GenreId = vm.Genre;


              

                var attendes = _context.Attendances
                   .Where(a => a.GigId == gig.Id)
                   .Select(a => a.Attendee)
                   .ToList();

                foreach (var user in attendes)
                {
                    user.Notify(notification);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Mine", "Gigs");
            }
            else
            {
                vm.Genres = _context.Genre.ToList();

                return (View("GigForm", vm));
            }
        }


        public ActionResult Details(int id)
        {
            var userId = User.Identity.GetUserId();
       
            var gig = _context.Gigs.Where(g => g.Id == id)
                .Include(a => a.Artist).Single();

            var vm = new GigDetailsViewModel
            {
                Gig = AutoMapper.Mapper.Map<Gig, GigDto>(gig),
                User = AutoMapper.Mapper.Map<ApplicationUser, ApplicationUserDto>(gig.Artist),
                isGoing = _context.Attendances.Where(g => g.Gig.Id == id).Any(g => g.Attendee.Id == userId)

            };
                  
            return View("Details", vm);
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel vm)
        {
            return RedirectToAction("Index", "Home", new {query = vm.Search});
        }
    }
}