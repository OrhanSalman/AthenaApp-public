using AthenaWebApp.Areas.Identity.IdentityModels;
using AthenaWebApp.Data;
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace AthenaWebApp.Controllers.MVC
{
    //    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly Context _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<UserExtension> userManager;

        public RoleController(Context context, RoleManager<IdentityRole> roleMgr, UserManager<UserExtension> userMrg)
        {
            _context = context;
            roleManager = roleMgr;
            userManager = userMrg;
        }

        public bool IsChecked { get; set; }
        public string Id { get; set; }

        string usedRoleId = "";

        public ViewResult Index() => View(roleManager.Roles);

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            // 1. Create the new Role
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    // Add default claims to role
                    var createdRole = await roleManager.FindByNameAsync(name);
                    foreach (var claim in ClaimData.ClaimTypes)
                    {
                        /*
                        var claimsToRole = new IdentityRoleClaim<string>()
                        { 
                            ClaimType = claim,
                            ClaimValue = "false"
                        };
                        */
                        await _context.RoleClaims.AddAsync(
                            new IdentityRoleClaim<string>
                            {
                                RoleId = createdRole.Id,
                                ClaimType = claim,
                                ClaimValue = "false"
                            });
                        _context.SaveChanges();

//                        var aclaimResult = await roleManager.AddClaimAsync(createdRole, claim);
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    Errors(result);
                }
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



        /*
        public void ClaimsModel(RoleManager<IdentityRole> mgr)
        {
            RoleManager = mgr;
        }
        public RoleManager<IdentityRole> RoleManager { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public IEnumerable<Claim> Claims { get; set; }
        */

        
        public async Task<IActionResult> Claims(string id)
        {
            // if-Break
            if (id == null)
            {
                return NotFound();
            }

            var roleClaimValues = await _context.RoleClaims.Where(c => c.RoleId == id).ToListAsync();
            usedRoleId = roleClaimValues.Select(c => c.RoleId).ToString();
            return View(roleClaimValues);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int id)
        {

            // Search for ClaimId
            IdentityRoleClaim<string> roleClaimData = new IdentityRoleClaim<string>();
            roleClaimData = _context.RoleClaims.SingleOrDefault(e => e.Id == id);
            roleClaimData.ClaimValue = "true";
            _context.SaveChanges();
            /*
//            identityRoleClaim.RoleId = usedRoleId;
            if (id != roleClaimData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(identityRoleClaim);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Claims));
            }
            */
            await Claims (roleClaimData.RoleId.ToString());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id, [Bind("Id,RoleId,ClaimType,ClaimValue")] IdentityRoleClaim<string> identityRoleClaim)
        {
            identityRoleClaim.ClaimValue = "false";
            identityRoleClaim.RoleId = usedRoleId;

            if (id != identityRoleClaim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(identityRoleClaim);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Claims));
            }
            return View(identityRoleClaim);
        }






        [HttpPost]
        [ActionName("SaveChanges")]
        public async Task<IActionResult> SaveChanges([FromBody] Claim claim)
        {
            /*
            public ActionResult Index(int[] selectedObjects)
            {
                //get the id from each selected checkbox
                foreach (int item in selectedObjects)
                {
                    //update your row here using the item you get
                }
            }
            */
            IdentityRole role = await roleManager.FindByIdAsync(Id);

//            var claim = new Claim(type, value);
            var result = await roleManager.AddClaimAsync(role, claim);
            if (result.Succeeded)
                return RedirectToAction("Claims");
            else
                Errors(result);
            return View();
            /*
            if (ModelState.IsValid)
            {



            }
            */

            /*
            var role = await _context.Roles.FindAsync(Id);
            Claim claim = new Claim(claimType, claimValue, ClaimValueTypes.String);
            IdentityResult result = await roleManager.AddClaimAsync(role, claim);

            if (result.Succeeded)
                return RedirectToAction("Claims");
            else
                Errors(result);
            return View();
            */
        }
        /*
        public async Task<IActionResult> SaveSelectedClaimssss([Bind("Id,CompanyName,Country,EmailContext")] Role company)
        {
            // RoleId
            if (id == null)
            {
                return NotFound();
            }

            var saveClaim = await roleManager.CreateAsync()

                            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);



            var company = await _context.Company.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);

            if (string.IsNullOrEmpty(Id))
            {
                //Redirect to NotFound
                return RedirectToPage("/");
            }
            IdentityRole role = await RoleManager.FindByIdAsync(Id);
            Claims = await RoleManager.GetClaimsAsync(role);
            return View();
        }
        */
        /*
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
        */



        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

    }
}