using AthenaWebApp.Areas.Identity.IdentityModels;
using AthenaWebApp.Data;
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AthenaWebApp.Controllers.MVC
{
//    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly Context _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<UserExtension> userManager;

        public RoleController(RoleManager<IdentityRole> roleMgr, UserManager<UserExtension> userMrg)
        {
            roleManager = roleMgr;
            userManager = userMrg;
        }

        public ViewResult Index() => View(roleManager.Roles);

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", roleManager.Roles);
        }

        public async Task<IActionResult> Update(string id)
        {

            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<UserExtension> members = new List<UserExtension>();
            List<UserExtension> nonMembers = new List<UserExtension>();
            foreach (UserExtension user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    UserExtension user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    UserExtension user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Update(model.RoleId);
        }




        public void ClaimsModel(RoleManager<IdentityRole> mgr)
        {
            RoleManager = mgr;
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
            IdentityRole role = await RoleManager.FindByIdAsync(Id);
            Claims = await RoleManager.GetClaimsAsync(role);
            return View();
        }

        public async Task<IActionResult> OnPostAddClaimAsync([Required] string type,
                                                             [Required] string value)
        {

            IdentityRole role = await RoleManager.FindByIdAsync(Id);

            if (ModelState.IsValid)
            {
                var claim = new Claim(type, value);
                var result = await RoleManager.AddClaimAsync(role, claim);
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }
            }
            Claims = await RoleManager.GetClaimsAsync(role);
            return View();
        }

        /*
        public async Task<IActionResult> OnPostEditClaimAsync([Required] string type,
                                                              [Required] string value,
                                                              [Required] string oldValue)
        {
            IdentityRole role = await RoleManager.FindByIdAsync(Id);
            if (ModelState.IsValid)
            {
                var claimNew = new Claim(type, value);
                var claimOld = new Claim(type, oldValue);
                var result = await roleMgr.ReplaceClaimAsync(role, claimOld, claimNew);
            }
            Claims = await RoleManager.GetClaimsAsync(role);
            return Page();
        }
        */

        public async Task<IActionResult> OnPostDeleteClaimAsync([Required] string type,
                                                                [Required] string value)
        {
            IdentityRole role = await RoleManager.FindByIdAsync(Id);
            if (ModelState.IsValid)
            {
                var claim = new Claim(type, value);
                var result = await RoleManager.RemoveClaimAsync(role, claim);
            }
            Claims = await RoleManager.GetClaimsAsync(role);
            return View();

        }




        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }





    }
}