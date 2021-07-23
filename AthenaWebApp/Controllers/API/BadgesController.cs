using AthenaWebApp.Data;
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Controllers.API
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]

    public class BadgesController : ControllerBase
    {
        private readonly Context _context;

        public BadgesController(Context context)
        {
            _context = context;
        }

        // GET: api/Activities
        [HttpGet]
        [Route("GetCheckBadges")]
        public async Task<ActionResult<IEnumerable<Badge>>> GetActivity()
        {
            return await _context.Badge.ToListAsync();
        }

        private bool BadgeExists(string id)
        {
            return _context.Badge.Any(e => e.Id == id);
        }
    }
}
