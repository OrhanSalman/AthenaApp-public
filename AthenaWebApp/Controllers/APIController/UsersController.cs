using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AthenaWebApp.Data;
using AthenaWebApp.Models;
using AthenaWebApp.Areas.Identity.IdentityModels;
using AthenaWebApp.Repositories.PatternInterfaces;

namespace AthenaWebApp.Controllers.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _context;

        public UsersController(IUserRepository context)
        {
            _context = context;
        }
/*
        // GET: api/Users
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AthenaIdentityUser>>> GetUser()
        {
            try
            {
                return (await _context.GetUsers()).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
*/
        // GET: api/Users/5
        [HttpGet("{Email}")]
        public async Task<ActionResult<AthenaIdentityUser>> GetUserByEmail(string Email)
        {
            try
            {
                var result = await _context.GetUserByEmail(Email);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
            // GET: api/Users/5
            [HttpGet("{Email}/{PasswordHash}")]
            public async Task<ActionResult<AthenaIdentityUser>> GetUserLogin(string Email, string PasswordHash)
            {
                try
                {
                    var result = await _context.GetUserLogin(Email, PasswordHash);

                    if (result == null) return NotFound();

                    return result;
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error retrieving data from the database");
                }

                /*
                            // PUT: api/Users/5
                            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
                        [HttpPut("{id}")]
                        public async Task<IActionResult> PutUser(string id, AthenaIdentityUser user)
                        {
                            if (id != user.Id)
                            {
                                return BadRequest();
                            }

                            _context.Entry(user).State = EntityState.Modified;

                            try
                            {
                                await _context.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                if (!UserExists(id))
                                {
                                    return NotFound();
                                }
                                else
                                {
                                    throw;
                                }
                            }

                            return NoContent();
                */
            }
        
        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AthenaIdentityUser>> PostUser(AthenaIdentityUser user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                var newUser = await _context.AddUser(user);
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
            /*
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            */
        }
/*
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
*/
    }
}
