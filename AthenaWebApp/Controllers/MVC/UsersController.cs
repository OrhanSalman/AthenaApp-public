using AthenaWebApp.Areas.Identity.IdentityModels;
using AthenaWebApp.Data;
using AthenaWebApp.Pagings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Controllers.MVC
{
    public class UsersController : Controller
    {

        private readonly Context _context;
        private readonly UserManager<UserExtension> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public UsersController(Context context, UserManager<UserExtension> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /*
                public async Task<IActionResult> GetUserCount()
                {
                    var x = _context.Employees
            .Include("Department")
            .GroupBy(e => e.Department.Name)
            .Select(y => new MyViewModel
            {
                Department = y.Key,
                count = y.Count()
            }).ToList();

                    return await _context.Users.FromSqlRaw("SELECT COUNT(*) FROM [dbo].[AspNetUsers]");
                }
        */
        // GET: Users
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {

            // ToDo: Check if ROLE HAS THE RIGHT CLAIMS, if yes, just show users for the company

            // Default
            var isSupervisor = false;
            /*
            // Get the actual UserId
            var userId = _userManager.GetUserId(User);
            // Check the role of the user
            var roleIdofUser = _context.UserRoles
                .Where(r => r.UserId == userId) // userId.Contains(r.UserId))
                .Select(r => r.RoleId)
                .ToString();

            var getRole = _roleManager.Roles
                .Where(r => r.Id == roleIdofUser)
                .Select(r => r.)
            var roles = _context.Roles.Where(r => r.userRoleIds ==


            string loggedInUserId = _userManager.GetUserId(User);
            template.UserId = loggedInUserId;

            var userRoleIds = _roleManager.Roles.Select(r => r.Id);
            */

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CompanySortParm"] = String.IsNullOrEmpty(sortOrder) ? "company_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["RegisteredSinceSortParm"] = sortOrder == "RegisteredSince" ? "RegisteredSince_desc" : "RegisteredSince";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            var users = from s in _context.Users
                .Include(b => b.Company)
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.UserName.Contains(searchString)
                                       || s.Email.Contains(searchString)
                                       || s.CompanyId.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(s => s.UserName);
                    break;
                case "company_desc":
                    users = users.OrderByDescending(s => s.Company.CompanyName);
                    break;
                case "Date":
                    users = users.OrderBy(s => s.LastActivity);
                    break;
                case "date_desc":
                    users = users.OrderByDescending(s => s.LastActivity);
                    break;
                case "RegisteredSince":
                    users = users.OrderBy(s => s.RegisteredSince);
                    break;
                case "RegisteredSince_desc":
                    users = users.OrderByDescending(s => s.RegisteredSince);
                    break;
                default:
                    users = users.OrderBy(s => s.UserName);
                    break;
            }
            int pageSize = 6;
            return View(await UserPaginatedList<UserExtension>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(b => b.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [Authorize(Policy = "Create User")]
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyName", "CompanyName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Create User")]
        public async Task<IActionResult> Create([Bind("Id,CompanyId,RegisteredSince,UserName,Email,EmailConfirmed,PasswordHash")] UserExtension user)
        {
            // Get the CompanyId from Company of the new created User
            string compId = _context.Company.Where(x => x.CompanyName == user.CompanyId).Select(x => x.Id).FirstOrDefault();
            // Here we have to set the variables, because of the SelectedList in the View, UserId will include an UserName
            user.CompanyId = compId;

            // Create User
            if (ModelState.IsValid)
            {
                user.NormalizedEmail = user.Email.ToUpper();
                user.NormalizedUserName = user.UserName.ToUpper();
                _context.Add(user);
                await _context.SaveChangesAsync();

                /*
                // ToDo ?
                if (user.EmailConfirmed == false)
                {
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
                }
                */

                // Hash Password
                string pwToHash = user.PasswordHash.ToString();
                await _userManager.CreateAsync(user, pwToHash);

                return RedirectToAction(nameof(Index));
            }

            // Create Role


            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize(Policy = "Edit User")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Edit User")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email,EmailConfirmed,Company")] UserExtension user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Policy = "Delete User")]
        public async Task<IActionResult> Delete(string id)
        {

            // ToDo: If User is deleted, first check where the UserId is in relation
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(b => b.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Delete User")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
