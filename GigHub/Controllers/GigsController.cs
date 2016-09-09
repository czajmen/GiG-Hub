using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
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
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(GigFormViewModel vm)
        {


            var artistId = User.Identity.GetUserId();
            var artist = _context.Users.Single(u => u.Id == artistId);
            var genre = _context.Genre.Single(g => g.Id == vm.Genre);




            var gig = new Gig
            {
                Artist = artist,
                DateTime = DateTime.Parse(string.Format("{0} {1}", vm.Date, vm.Time)),
                Genre = genre,
                Venue = vm.Venue
            };

            _context.Gigs.Add(gig);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");

        }
    }
}