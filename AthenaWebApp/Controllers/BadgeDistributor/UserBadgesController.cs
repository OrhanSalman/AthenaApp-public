using AthenaWebApp.Data;
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<IActionResult> GetMyBadges(string id)
        {
            /*
            
            var queryToGetImage = from b in _context.Badge
                        join ub in _context.UserBadge on b.Id equals ub.BadgeId
                        where ub.UserId == id
                        select b.BadgeImage;
            

            var byyte = _context.Badge.Select(a => a.BadgeImage).ToArray();
            Console.WriteLine(byyte.ToString());
            Debug.WriteLine(byyte);

            var badgeImage = _context.UserBadge
                .Include(t => t.Badge)
                .Where(a => a.UserId == id)
                .Select(t => t.Badge.BadgeImage)
                .ToListAsync();

            
                            byte[] bytes = (byte[])GetData("SELECT Data FROM Images WHERE Id =" + id).Rows[0]["Data"];
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            byte[] sqlbinary = login.FirstOrDefault().Password.ToArray();
            
            if (badgeImage != null)
            {
                return Ok(await badgeImage);
            }

            return Ok("No badges");
            */
//            List <UserBadge> bgs = new List<UserBadge>();
            var badge = await _context.UserBadge
                .Include(t => t.Badge.BadgeImage)
                .Where(a => a.UserId == id)
                .ToListAsync();

            Debug.WriteLine(id);
            Debug.WriteLine(badge);
            return Ok(badge);
        }

        // GET: api/GetMyBadges
        [HttpGet]
        [Route("GetMyNewBadge")]
        public async Task<IActionResult> GetMyNewBadge(string id)
        {

            List<UserBadge> bgs = new List<UserBadge>();
            var badge = await _context.UserBadge
                .Include(t => t.Badge.BadgeImage)
                .Where(a => a.UserId == id)
//                .Select(t => t.Badge.BadgeImage)
                .ToListAsync();


            return Ok(badge);
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

                await _context.SaveChangesAsync();
                GetMyNewBadge(userId);    // No await needed here

//            CreatedAtAction("GetMyBadges", new { id = userBadge.Id }, userBadge);
//            }

        }

        private bool BadgeExists(string id)
        {
            return _context.Badge.Any(e => e.Id == id);
        }
    }
}
