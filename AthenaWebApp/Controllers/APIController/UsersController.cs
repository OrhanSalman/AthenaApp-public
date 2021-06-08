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
using Microsoft.Extensions.Configuration;
using AthenaApp.Models;

namespace AthenaWebApp.Controllers.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<AthenaIdentityUser> userManager;
        private readonly AthenaDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public UsersController(IUserRepository context, IEmailSender emailSender, UserManager<AthenaIdentityUser> userManager, AthenaDbContext dbContext, IConfiguration configuration)
        {
            _context = context;
            _emailSender = emailSender;
            this.userManager = userManager;
            _dbContext = dbContext;
            _configuration = configuration;
        }

//        [HttpPost("/Register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var userExists = await userManager.FindByEmailAsync(user.Email);
            if(userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "The user with this E-Mail already exists. Choose another one." });
            }
            AthenaIdentityUser athenaIdentityUser = new AthenaIdentityUser()
            {
                Email = user.Email,
                UserName = user.UserName,
                CompanyId = user.CompanyId,
                Company = user.Company,
            };

            var result = await userManager.CreateAsync(athenaIdentityUser, user.Password);
            if(!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Irgendein Error" });
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully" });
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
        [HttpPost("/Register")]
        public async Task<ActionResult<User>> PostUser(AthenaIdentityUser user)
        {
            
            

            try
            {
                //                user = new AthenaIdentityUser { UserName = Input.UserName, Email = Input.Email, Company = Input.Company, CompanyId = 1 };
                //                var result = await _userManager.CreateAsync(user, Input.Password);
                user = new AthenaIdentityUser();
                

                var result = await _context.PostNewUser(user);

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

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }/*
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

*/
        private bool UserExists(string id)
        {
            return _dbContext.Users.Any(e => e.Id == id);
        }
    }
}
