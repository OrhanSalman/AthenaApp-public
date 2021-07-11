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
    public class ActivitiesController : ControllerBase
    {
        private readonly Context _context;

        public ActivitiesController(Context context)
        {
            _context = context;
        }

        // GET: api/Activities
        [HttpGet]
        [Route("GetActivities")]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivity()
        {
            return await _context.Activity.ToListAsync();
        }

        // GET: api/Activities/5
        [HttpGet]
        [Route("GetActivity")]
        public async Task<ActionResult<Activity>> GetActivity(string id)
        {
            var activity = await _context.Activity.FindAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            return activity;
        }


        private bool ActivityExists(string id)
        {
            return _context.Activity.Any(e => e.Id == id);
        }
    }
}
