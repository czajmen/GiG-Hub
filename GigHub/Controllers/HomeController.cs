﻿using GigHub.Models;
using GigHub.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(string query = null)
        {
            var upCommingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!string.IsNullOrWhiteSpace(query))
            {
                upCommingGigs =
                    upCommingGigs.Where(
                        g => g.Artist.Name.Contains(query) || 
                        g.Genre.Name.Contains(query) ||
                        g.Venue.Contains(query));
            }

            string userID = User.Identity.GetUserId();
            var attendances = _context.Attendances
                .Where(a => a.AttendeeId == userID 
                        && a.Gig.DateTime > DateTime.Now)
                .ToList().ToLookup(a => a.GigId);

            var vm = new GigsViewModel
            {
                UpcomingGigs = upCommingGigs,
                Authorized = User.Identity.IsAuthenticated,
                Heading = "Lista dostępnych Gigów!",
                Search = query,
                Attendances = attendances
            };


            return View("Gigs", vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}