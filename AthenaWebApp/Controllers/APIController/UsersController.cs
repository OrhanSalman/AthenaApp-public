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
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace AthenaWebApp.Controllers.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<AthenaIdentityUser> _userManager; 
        //        private readonly ICompanyRepository _companyRepository;

        public UsersController(IUserRepository context, IEmailSender emailSender, UserManager<AthenaIdentityUser> userManager)
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<AthenaIdentityUser>> PostRegisterUser([Bind("UserName,Email,CompanyId,Company")] AthenaIdentityUser user, string returnUrl = null)
        {
            try
            {
                if (!ModelState.IsValid || user == null)
                    return BadRequest();

                var newUser = await _context.PostRegisterUser(user);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);


                await _emailSender.SendEmailAsync("Email", "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new user record");
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
