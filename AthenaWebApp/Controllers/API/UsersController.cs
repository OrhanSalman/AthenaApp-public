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

namespace AthenaWebApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Context _context;

        public UsersController(Context context)
        {
            _context = context;
        }

        // GET: api/Users/AppLoginRequest?Email=example@mail.com
        [HttpGet]
        [Route("AppLoginRequest")]
        public async Task<ActionResult<UserExtension>> AppLoginRequest(string email)
        {
            var user = await _context.Users
                    .FirstOrDefaultAsync(e => e.Email == email);

            if (user == null)
            {
                return NotFound();
            }

            // ToDo: return Token
            return user;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("AppRegisterRequest")]
        public async Task<ActionResult<UserExtension>> AppRegisterRequest(string email, string userName)
        {

            var user = new UserExtension()
            {
                Email = email,
                UserName = userName
            };

            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
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

            // ToDo: return Token
            // ToDo: Send E-Mail-Verification + automaticly login (maybe AppLoginRequest method?)
            // ToDo: Check + Set Company in UserTable, but also FK in CompanyTable
            // ToDo: Set UserRole + UserClaims
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }




        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Route("AppUpdateAccountRequest")]
        public async Task<IActionResult> AppUpdateAccountRequest(string id, UserExtension user)
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
        }

        // DELETE: api/Users/5
        [HttpDelete]
        [Route("AppDeleteAccountRequest")]
        public async Task<IActionResult> AppDeleteAccountRequest(string id) // UserExtension ?
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
    }
}
