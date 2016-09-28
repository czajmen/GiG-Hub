using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Models.Dtos;
using GigHub.Persistence;
using GigHub.Repositories;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext _context;
        private UnitOfWork _unitOfWork;

        public GigsController()
        {
            _context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: Gigs
        [Authorize]
        public ActionResult Create()
        {

            var vm = new GigFormViewModel
            {
                Genres = _unitOfWork.Genre.GetGenres(),
                Heading = "Zaplanuj nowy koncert!"


            };

            return View("GigForm", vm);
        }

  


        [Authorize]
        public ActionResult Edit(int id)
        {

            var userId = User.Identity.GetUserId();

            var gig = _unitOfWork.Gigs.GetGigById(id);

            if (gig.ArtistId != userId)
                return new HttpUnauthorizedResult();

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
            var vm = new GigsViewModel
            {
                UpcomingGigs =_unitOfWork.Gigs.GetGigsUserAttending(User.Identity.GetUserId()),
                Authorized = User.Identity.IsAuthenticated,
                Heading = "Koncerty na którę idę!",
                Attendances = _unitOfWork.Attendance.GetFutureAttendances(User.Identity.GetUserId()).ToLookup(a => a.GigId)

        };

            return View("Gigs", vm);

        }



  

        [Authorize]
        public ActionResult Mine()
        {
            var gigs = _unitOfWork.Gigs.GetFutureActiveGigsByUser(User.Identity.GetUserId());

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

                _unitOfWork.Gigs.Add(gig);
               _unitOfWork.Complete();

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
              //  var gig = _context.Gigs.Single(g => g.Id == vm.Id && g.ArtistId == userId);

                var gig = _unitOfWork.Gigs.GetGigById(vm.Id);

                if (gig.ArtistId != userId)
                    return new HttpUnauthorizedResult();

                var notification = new Notification(gig, NotificationType.GigUpdated);

                notification.OrigialDateTime = gig.DateTime;
                notification.OriginalVenue = gig.Venue;

                gig.Venue = vm.Venue;
                gig.DateTime = vm.GetDateTime(vm.Date, vm.Time);
                gig.GenreId = vm.Genre;
              
                var attendes = _unitOfWork.Attendance.GetAttendancesByGig(gig);

                foreach (var user in attendes)
                {
                    user.Notify(notification);
                }

              _unitOfWork.Complete();

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
            var gig = _unitOfWork.Gigs.GetGigWithArtistByGigId(id);
        
            if (gig==null)
            {
                return HttpNotFound();
            }

            var vm = new GigDetailsViewModel
            {
                Gig = AutoMapper.Mapper.Map<Gig, GigDto>(gig)
              
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                vm.User = AutoMapper.Mapper.Map<ApplicationUser, ApplicationUserDto>(gig.Artist);
                vm.isGoing = _unitOfWork.Attendance.GetUserAttendingConfirm(id, userId);
            }

            return View("Details", vm);
        }
    

        [HttpPost]
        public ActionResult Search(GigsViewModel vm)
        {
            return RedirectToAction("Index", "Home", new {query = vm.Search});
        }
    }
}