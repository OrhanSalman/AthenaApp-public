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
using System.Net.Mail;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace AthenaWebApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Context _context;
        private readonly UserManager<UserExtension> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public UsersController(Context context, UserManager<UserExtension> userManager, RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        // GET: api/Users/AppLoginRequest?Email=example@mail.com
        [HttpGet]
        [Route("AppLoginRequest")]
        public async Task<ActionResult<UserExtension>> AppLoginRequest(string email)
        {

            var user = await _context.Users
                    .FirstOrDefaultAsync(e => e.Email == email);

            // Check if Email is confirmed
            if (user.EmailConfirmed == false)
            {
                return NotFound("Please confirm your Email first.");
            }     
            // ToDo: New Handler, for Email confirm check

            // Check if User exists
            if (user == null)
            {
                return NotFound("User doesn't exists.");
            }

            // Check if User is blocked
            if (user.LockoutEnabled == true)
            {
                return NotFound("Unfortunately you are blocked. Please contact your administrator: admin@athena.com");
            }

            /*
                        var user_LoginData = new MobileUser_LoginData()
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            Email = user.Email,
                            CompanyName = user.CompanyName,
                            SecurityStamp = user.SecurityStamp,
                            ProfilePicture = user.ProfilePicture
                        };
             */
            //            string JsonUserObject = JsonSerializer.Serialize<MobileUser_LoginData>(user_LoginData);
            // ToDo: return Token, Id, UserName, Email, SecurityStamp

            //           var userSerialize = new JsonSerializer();
            //           userSerialize.Serialize(UserExtension);
            //           return JsonString(user.SecurityStamp);

            // ToDo: Return CompanyId
            return user;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("AppRegisterRequest")]
        public async Task<ActionResult<UserExtension>> AppRegisterRequest([FromBody] UserExtension recievedUserData)
        {
            // Split E-Mail 
            string splittedMail = recievedUserData.Email.Split('@').Last();

            // ToDo: Check if Email contains the EmailContext of an Company (Unterscheidung zw. @uni-siegen / @student.uni-siegen
            string companyName = _context.Company
                                .Where(a => a.EmailContext == splittedMail)
                                .Select(a => a.CompanyName)
                                .FirstOrDefault();

            // Check if the company exists (it has to be a valid University-Domain
            if (companyName == null)
            {
                return BadRequest("Registration failed. Please register with an accepted Company E-Mail");
            }
            else
            {
                var user = new UserExtension()
                {
                    Email = recievedUserData.Email,
                    UserName = recievedUserData.UserName,
                    CompanyId = companyName.ToString()
                };
                _context.Users.Add(user);

                try
                {
                    _context.Users.Add(user);

                    if (EmailExists(user.Email))     // Check if E-Mail is already in use
                    {
                        return Conflict("The E-Mail you entered is already in use.");
                    }
                    else if (UserExists(user.UserName))  // Check if UserName is already in use
                    {
                        return Conflict("The Username is already in use.");
                    }
                    else
                    {
                        await _context.SaveChangesAsync();                                      // Save User
                        var newUserId = CreatedAtAction("GetUser", new { id = user.Id });       // Create Auto Id
                        await _userManager.AddToRoleAsync(user, "MobileUser");                  // Add new User to Role
                    }
                }
                catch (DbUpdateException)
                {
                    throw;
                }

                string returnUrl = null;
                returnUrl ??= Url.Content("~/");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(user.Email, "Confirm your registration",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(returnUrl)}'>clicking here</a>.");
                // ToDo: return Token
                // ToDo: Send E-Mail-Verification + automaticly login (maybe AppLoginRequest method?)
                // ToDo: Check + Set Company in UserTable, but also FK in CompanyTable
                // ToDo: Set UserRole + UserClaims

                // Gives Code 200 + Company-Data + the hole User data (but return user give's the same return) 
                return Ok(user);

            }
        }


        // Post: api/Users/AppUpdateAccountRequest/
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


        private bool EmailExists(string Email)
        {
            return _context.Users.Any(e => e.Email == Email);
        }
        private bool UserExists(string UserName)
        {
            return _context.Users.Any(e => e.UserName == UserName);
        }
    }
}
