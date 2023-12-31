﻿using AthenaWebApp.Areas.Identity.IdentityModels;
using AthenaWebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AthenaWebApp.Controllers.API
{
    [AllowAnonymous]
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

            // Check if User exists
            if (user == null)
            {
                return NotFound("User doesn't exists.");
            }
            // Check if Email is confirmed
            else if (user.EmailConfirmed == false)
            {
                return NotFound("Please confirm your Email first.");
            }
            // ToDo: New Handler, for Email confirm check
            // Check if User is blocked
            else if (user.LockoutEnabled == true)
            {
                return NotFound("Unfortunately you are blocked. Please contact your administrator: admin@athena.com");
            }
            return user;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("AppRegisterRequest")]
        public async Task<ActionResult<UserExtension>> AppRegisterRequest([FromBody] UserExtension recievedUserData)
        {
            // Split E-Mail 
            string splittedMail = recievedUserData.Email.Split('@').Last();
            var companyMailContexts = _context.Company.Select(a => a.EmailContext).ToList();

            string correctMailContext = null;
            string companyId = null;

            // Check if Email is allowed [in DB @uni-siegen.de -> XYZ.uni-siegen.de is allowed]
            bool found = false;
            for (int c = 0; c <= companyMailContexts.Count - 1; c++)
            {
                if (splittedMail.Contains(companyMailContexts[c]))
                {
                    Console.WriteLine("c: " + c + " : " + companyMailContexts[c]);
                    correctMailContext = companyMailContexts[c];
                    found = true;
                    // If there is a match, get the CompanyId
                    if (found)
                    {
                        companyId = _context.Company
                                        .Where(a => a.EmailContext.Contains(correctMailContext))
                                        .Select(a => a.Id)
                                        .FirstOrDefault();
                        break;
                    }
                }
            }


            // Check if the company exists (it has to be a valid University-Domain)
            if (String.IsNullOrEmpty(companyId))
            {
                return BadRequest("Registration failed. Please register with an accepted Company E-Mail");
            }
            else
            {
                var user = new UserExtension()
                {
                    Email = recievedUserData.Email,
                    UserName = recievedUserData.UserName,
                    CompanyId = companyId.ToString()
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
                    "/Users/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);

                Regex regex = new Regex("LINK");
                string html = new System.IO.StreamReader(@"wwwroot/html/confirmation_email.html").ReadToEnd();
                html = regex.Replace(html, callbackUrl);
                // ToDo: Email confirmation load template
                await _emailSender.SendEmailAsync(user.Email, "Confirm your registration",
                html);

                // ToDo: return Token
                // ToDo: Send E-Mail-Verification + automaticly login (maybe AppLoginRequest method?)
                // ToDo: Check + Set Company in UserTable, but also FK in CompanyTable
                // ToDo: Set UserRole + UserClaims

                // Gives Code 200 + Company-Data + the hole User data (but return user give's the same return) 
                
                return Ok(user);

            }
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("ConfirmEmail")]
        public void ConfirmEmail(string userid, string token)
        {
            UserExtension user = _userManager.FindByIdAsync(userid).Result;
            IdentityResult result = _userManager.
                        ConfirmEmailAsync(user, token).Result;
      /*      if (result.Succeeded)
            
                return View("Success");
            }
            else
            {
                ViewBag.Message = "Error while confirming your email!";
                return View("Error");
            }  */
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
