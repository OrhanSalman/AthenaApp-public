using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AthenaWebApp.Areas.Identity.Pages
{
    public class ClaimsModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public ClaimsModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        public RoleManager<IdentityRole> RoleManager { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public IEnumerable<Claim> Claims { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(Id))
            {
                //Redirect to NotFound
                return RedirectToPage("/");
            }
            IdentityRole role = await _roleManager.FindByIdAsync(Id);
            Claims = await _roleManager.GetClaimsAsync(role);
            return Page();
        }

        public async Task<IActionResult> OnPostAddClaimAsync([Required] string type,
                                                             [Required] string value)
        {

            IdentityRole role = await _roleManager.FindByIdAsync(Id);

            if (ModelState.IsValid)
            {
                var claim = new Claim(type, value);
                var result = await _roleManager.AddClaimAsync(role, claim);
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }
            }
            Claims = await _roleManager.GetClaimsAsync(role);
            return Page();
        }

        /*
        public async Task<IActionResult> OnPostEditClaimAsync([Required] string type,
                                                              [Required] string value,
                                                              [Required] string oldValue)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(Id);
            if (ModelState.IsValid)
            {
                var claimNew = new Claim(type, value);
                var claimOld = new Claim(type, oldValue);
                var result = await _roleManager.ReplaceClaimAsync(role, claimOld, claimNew);
            }
            Claims = await _roleManager.GetClaimsAsync(role);
            return Page();
        }
        */

        public async Task<IActionResult> OnPostDeleteClaimAsync([Required] string type,
                                                                [Required] string value)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(Id);
            if (ModelState.IsValid)
            {
                var claim = new Claim(type, value);
                var result = await RoleManager.RemoveClaimAsync(role, claim);
            }
            Claims = await _roleManager.GetClaimsAsync(role);
            return Page();

        }

    }
}
