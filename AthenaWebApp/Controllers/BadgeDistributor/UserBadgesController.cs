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
        public async Task<ActionResult<IEnumerable<UserBadge>>> GetMyBadges(string id)
        {
            return await _context.UserBadge.Where(i => i.UserId == id).ToListAsync();
        }

        [HttpPost]
        [Route("PostUserBadge")]
        public async Task<ActionResult<UserBadge>> PostUserBadge(string userId, List<string> badgeId)
        {
            for (int i = 0; i <= badgeId.Count; i++)
            {
                var userBadge = new UserBadge
                {
                    BadgeId = badgeId[i],
                    UserId = userId
                };
                // Nach jeder Aktivität Get_Activity und Get_UserActivity_Count. IF Staffelung true, diese Methode hier aufrufen
                _context.UserBadge.Add(userBadge);
                await _context.SaveChangesAsync();

                CreatedAtAction("GetMyBadges", new { id = userBadge.Id }, userBadge);
            }
            return Ok();
        }

        private bool BadgeExists(string id)
        {
            return _context.Badge.Any(e => e.Id == id);
        }
    }
}
