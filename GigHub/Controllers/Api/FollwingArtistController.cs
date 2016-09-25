using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;
using GigHub.Models.Dtos;

namespace GigHub.Controllers.Api
{
    public class FollwingArtistController : ApiController
    {
        private ApplicationDbContext _context;

        public FollwingArtistController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        [HttpPost]
        [Route("api/unfollow/")]
        public IHttpActionResult CancelFollowing(FollowingArtistDto dto)
        {
            var userId = User.Identity.GetUserId();


            if (!_context.Followers.Any(f => f.FollowerId == userId && f.FolloweeId == dto.ArtistId))
            {
                return BadRequest("Nie śledzisz tego wykonawcy");
            }

            var follow = _context.Followers.Single(f => f.FolloweeId == dto.ArtistId && f.FollowerId == userId);

          

            _context.Followers.Remove(follow);
            _context.SaveChanges();

            return Ok();
        }


        [Authorize]
        [HttpPost]
        [Route("api/follow/check/")]
        public IHttpActionResult IsFollowing(FollowingArtistDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followers.Any(f => f.FollowerId == userId && f.FolloweeId == dto.ArtistId))
            {

                return Ok();
            }

            return BadRequest();
        }



        [Authorize]
        [HttpPost]
        [Route("api/follow/")]
        public IHttpActionResult Follow(FollowingArtistDto dto)
        {
            try
            {
                var userId = User.Identity.GetUserId();

                if (_context.Followers.Any(f => f.FollowerId == userId && f.FolloweeId == dto.ArtistId))
                {
                    return BadRequest("Już śledzisz tego wykonawcę!");
                }

                var following = new FollowingArtist()
                {
                    FolloweeId = dto.ArtistId,
                    FollowerId = userId
                };

                _context.Followers.Add(following);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception)
            {

                return (BadRequest("Obserwowanie nie powiodło się."));


            }
        }


    }
}
