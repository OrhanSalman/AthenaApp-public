using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using AthenaWebApp.Areas.Identity.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using AthenaWebApp.Data;
using AthenaWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AthenaWebApp.Controllers;
//using AthenaWebApp.Repositories.PatternInterfaces;

namespace AthenaWebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<UserExtension> _signInManager;
        private readonly UserManager<UserExtension> _userManager; 
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        //        private readonly ICompanyRepository _companyRepository;

        public RegisterModel(
            UserManager<UserExtension> userManager,
            SignInManager<UserExtension> signInManager,
            IEnumerable<Company> context,
            ILogger<RegisterModel> logger,
            //            ICompanyRepository companyRepository,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            //            _companyRepository = companyRepository;
        }



        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }


            //ToDo: Add UserName, Company

            [Required]
            [Display(Name = "UserName")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "Universität")]
            public string CompanyName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }


        //
        //

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            /*
                        string userInput = Input.Company.ToString();
                        Company company = new Company();

                        company.Id = 1;
                        company.CompanyName = "TestCompany";
                        company.Country = "Deutschland";
                        company.CollectedDistances = 10;

                       var findId = _context.Select(x => x.Id)
                                          .Where(i => company.CompanyName.Contains(userInput))
                                          .ToList();

                        List<int> IdList = _context.Select(x => x.Id)
                                          .Where(i => company.CompanyName.Contains(userInput))
                                          .ToList();

                        foreach (int value in findId)
                        {
                            idList.Add(value);
                        }
              */

            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                // ToDo
                //                var id = _companyRepository.Search(Input.Company);

                var user = new UserExtension { UserName = Input.UserName, Email = Input.Email, CompanyName = Input.CompanyName };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);


                    // ToDo: Implement "E-Mail-Provider-Send-Mail-Logic"
                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
