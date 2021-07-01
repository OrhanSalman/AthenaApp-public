using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AthenaWebApp.Data;
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace AthenaWebApp.Controllers.API
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<UserActivity>> PostUserActivity([FromBody] UserActivity userActivity)
        {
            _context.UserActivity.Add(userActivity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserActivity", new { id = userActivity.Id }, userActivity);
        }


        private bool UserActivityExists(string id)
        {
            return _context.UserActivity.Any(e => e.Id == id);
        }
    }
}
