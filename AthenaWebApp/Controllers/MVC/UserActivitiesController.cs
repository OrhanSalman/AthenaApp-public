using AthenaWebApp.Data;
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Controllers.MVC
{
    public class UserActivitiesController : Controller
    {
        private readonly Context _context;

        public UserActivitiesController(Context context)
        {
            _context = context;
        }

        // GET: UserActivities
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserActivity
                .Include(b => b.Activity)
                .Include(b => b.Company)
                .Include(b => b.UserExtension)
                .ToListAsync());
        }

        // GET: UserActivities/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userActivity = await _context.UserActivity
                .Include(b => b.Activity)
                .Include(b => b.Company)
                .Include(b => b.UserExtension)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userActivity == null)
            {
                return NotFound();
            }

            return View(userActivity);
        }

        // GET: UserActivities/Create
        [Authorize(Policy = "Create UserActivity")]
        public IActionResult Create()
        {

            ViewData["UserId"] = new SelectList(_context.Users, "UserName", "UserName");
            ViewData["ActivityId"] = new SelectList(_context.Activity, "ActivityType", "ActivityType");
            return View();
        }

        // POST: UserActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Create UserActivity")]
        public async Task<IActionResult> Create([Bind("Id,UserId,ActivityId,StartTime,StopTime,SumDistance")] UserActivity userActivity)
        {
            // Get the CompanyId of the new created UserActivity
            string userId = _context.Users.Where(x => x.UserName == userActivity.UserId).Select(x => x.Id).FirstOrDefault(); ;
            string compId = _context.Users.Where(x => x.Id == userId).Select(x => x.CompanyId).FirstOrDefault();
            string actId = _context.Activity.Where(x => x.ActivityType == userActivity.ActivityId).Select(x => x.Id).FirstOrDefault();
            // Here we have to set the variables, because of the SelectedList in the View, UserId will include an UserName
            userActivity.CompanyId = compId;
            userActivity.UserId = userId;
            userActivity.ActivityId = actId;


            //  Auto SumTime
            TimeSpan diff = userActivity.StopTime.Subtract(userActivity.StartTime);
            userActivity.SumTime = diff;

            if (ModelState.IsValid)
            {
                _context.Add(userActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userActivity);
        }

        // GET: UserActivities/Edit/5
        [Authorize(Policy = "Edit UserActivity")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // ToDo
            var userActivity = await _context.UserActivity.FindAsync(id);
            if (userActivity == null)
            {
                return NotFound();
            }
            ViewData["ActivityId"] = new SelectList(_context.Activity, "ActivityType", "ActivityType", userActivity.ActivityId);
            return View(userActivity);
        }

        // POST: UserActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Edit UserActivity")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ActivityId,StartTime,StopTime,SumTime,SumDistance")] UserActivity userActivity)
        {
            if (id != userActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserActivityExists(userActivity.Id))
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
            return View(userActivity);
        }

        // GET: UserActivities/Delete/5
        [Authorize(Policy = "Delete UserActivity")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userActivity = await _context.UserActivity
                .Include(b => b.Activity)
                .Include(b => b.Company)
                .Include(b => b.UserExtension)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userActivity == null)
            {
                return NotFound();
            }

            return View(userActivity);
        }

        // POST: UserActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Delete UserActivity")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userActivity = await _context.UserActivity.FindAsync(id);
            _context.UserActivity.Remove(userActivity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserActivityExists(string id)
        {
            return _context.UserActivity.Any(e => e.Id == id);
        }
    }
}
