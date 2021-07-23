using AthenaWebApp.Controllers.BadgeDistributor;
using AthenaWebApp.Data;
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Controllers.API
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
//    [Authorize(Roles = "MobileUser")]
    public class UserActivitiesController : ControllerBase
    {
        private readonly Context _context;

        public UserActivitiesController(Context context)
        {
            _context = context;
        }

        // GET: api/UserActivities
        [HttpGet]
        [Route("GetUserActivitiesOfUserId")]
        public async Task<ActionResult<IEnumerable<UserActivity>>> GetUserActivity()
        {
            return await _context.UserActivity.ToListAsync();
        }

        // GET: api/UserActivities/5
        [HttpGet]
        [Route("GetDailyUserActivityOfUserId")]
        public async Task<ActionResult<UserActivity>> GetUserActivity(string id)
        {
            var userActivity = await _context.UserActivity.FindAsync(id);

            if (userActivity == null)
            {
                return NotFound();
            }

            return userActivity;
        }


        // POST: api/UserActivities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("PostUserActivity")]
        public async void PostUserActivity([FromBody] UserActivity userActivity)
        {
            _context.UserActivity.Add(userActivity);
            await _context.SaveChangesAsync();

            string activityId = userActivity.ActivityId;
            string userId = userActivity.UserId;

            /* Section - Possible Badges */

            // Count the distances for the selected User (Badge based on Activity)
            var sumDistanceForActivity = _context.UserActivity
                .Where(u => u.UserId == userId && u.ActivityId == activityId)
                .Sum(u => u.SumDistance);

            // Count the distances for all Activites of the User (Overall Badges)
            var sumOfAllDistances = _context.UserActivity
                .Where(u => u.UserId == userId)
                .Sum(u => u.SumDistance);

            // Get all possible Badges for the ActivityId for the newly posted UserActivity
            var badgeIntervalls = await _context.Badge
                .Where(u => u.ActivityId == activityId)
                .ToListAsync();

            // Get all possible GeneralBadges for the newly posted UserActivity
            var generalBadgeIntervalls = await _context.Badge
                .Where(u => u.GeneralBadge == true)
                .ToListAsync();

            string badgeFor = "";
            List<string> newBadge = new List<string>();
            UserBadgesController userBadgesController = new UserBadgesController(_context);

            // Check if User gets a Badge (based on an Activity)
            foreach (var distance in badgeIntervalls)
            {
                if (sumDistanceForActivity >= distance.DistanceForBadge)
                {
                    badgeFor = userActivity.UserId.ToString();
                    newBadge.Add(distance.Id.ToString());
                }
            }
            // Check if User gets a general Badge
            foreach (var distance in generalBadgeIntervalls)
            {
                if (sumOfAllDistances >= distance.DistanceForBadge)
                {
                    badgeFor = userActivity.UserId.ToString();
                    newBadge.Add(distance.Id.ToString());
                }
            }

            // Exclude already forgiven Badges for this User
            var forgivenBadges = _context.UserBadge.Where(i => i.UserId == userId).ToList();
            foreach(var badge in forgivenBadges)
            {
                newBadge.Remove(badge.ToString());
            }
            if(newBadge.Any())
            {
                // Send data to Distribution-Controller
                userBadgesController.PostUserBadge(badgeFor, newBadge);
            }
            // No return! The return has to be in userBadgesController.GetMyBadges (from PostUserBadge startetd)
//            return CreatedAtAction("GetUserActivity", new { id = userActivity.Id }, userActivity);
        }


        private bool UserActivityExists(string id)
        {
            return _context.UserActivity.Any(e => e.Id == id);
        }
    }
}
