using AthenaWebApp.Data;
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Controllers.BadgeDistributor
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]

    public class UserBadgesController : ControllerBase
    {
        private readonly Context _context;

        public UserBadgesController(Context context)
        {
            _context = context;
        }


        // GET: api/GetMyBadges
        [HttpGet]
        [Route("GetMyBadges")]
        public async Task<ActionResult<List<byte[]>>> GetMyBadges(string id)
        {
            /*
            var queryToGetImage = from b in _context.Badge
                        join ub in _context.UserBadge on b.Id equals ub.BadgeId
                        where ub.UserId == id
                        select b.BadgeImage;
            */

            var badgeImage = _context.UserBadge
                .Include(t => t.Badge)
                .Where(a => a.UserId == id)
                .Select(t => t.Badge.BadgeImage)
                .ToListAsync();

            if(badgeImage != null)
            {
                return Ok(await badgeImage);
            }

            return Ok("No badges");

        }

        // GET: api/GetMyBadges
        [HttpGet]
        [Route("GetMyNewBadge")]
        public async Task<IReadOnlyList<Byte[]>> GetMyNewBadge(string id)
        {
            /*
            var queryToGetImage = from b in _context.Badge
                        join ub in _context.UserBadge on b.Id equals ub.BadgeId
                        where ub.UserId == id
                        select b.BadgeImage;
            */

            // ToDo: Only get the new badge/s
            var badgeImage = _context.UserBadge
                .Include(t => t.Badge)
                .Where(a => a.UserId == id)
                .Select(t => t.Badge.BadgeImage)
                .FirstOrDefault();

            List<Byte[]> bgs = new List<byte[]>();

            return (IReadOnlyList<byte[]>)Response.Body.WriteAsync(badgeImage, 0, badgeImage.Length);
        }


        [HttpPost]
        [Route("PostUserBadge")]
        public async Task PostUserBadge(string userId, string badgeId)
        {
            // Only started, if UserActivitiesController (Api) says Yes, there is a new badge (line 116)
//            for (int i = 0; i <= badgeId.Count - 1; i++)
//            {
                var userBadge = new UserBadge
                {
//                    BadgeId = badgeId[i],
                    BadgeId = badgeId,
                    UserId = userId
                };
                // Check if the Badge already exists
                // Nach jeder Aktivität Get_Activity und Get_UserActivity_Count. IF Staffelung true, diese Methode hier aufrufen
                _context.UserBadge.Add(userBadge);
                await GetMyNewBadge(userId);    // No await needed here

                await _context.SaveChangesAsync();

                CreatedAtAction("GetMyBadges", new { id = userBadge.Id }, userBadge);
//            }

        }

        private bool BadgeExists(string id)
        {
            return _context.Badge.Any(e => e.Id == id);
        }
    }
}
