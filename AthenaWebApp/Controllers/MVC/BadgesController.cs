using AthenaWebApp.Data;
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Controllers.MVC
{
    public class BadgesController : Controller
    {
        private readonly Context _context;

        public BadgesController(Context context)
        {
            _context = context;
        }

        // GET: Badges
        public async Task<IActionResult> Index()
        {
            var context = _context.Badge.Include(b => b.Activity);
            return View(await context.ToListAsync());
        }

        // GET: Badges/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var badge = await _context.Badge
                .Include(b => b.Activity)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (badge == null)
            {
                return NotFound();
            }

            return View(badge);
        }

        // GET: Badges/Create
        public IActionResult Create()
        {
            ViewData["ActivityId"] = new SelectList(_context.Activity, "Id", "Id");
            return View();
        }

        // POST: Badges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActivityId,BadgeName,DistanceIntervallBegin,DistanceIntervallEnd,BadgeImage,BadgeDescription")] Badge badge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(badge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityId"] = new SelectList(_context.Activity, "Id", "Id", badge.ActivityId);
            return View(badge);
        }

        // GET: Badges/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var badge = await _context.Badge.FindAsync(id);
            if (badge == null)
            {
                return NotFound();
            }
            ViewData["ActivityId"] = new SelectList(_context.Activity, "Id", "Id", badge.ActivityId);
            return View(badge);
        }

        // POST: Badges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ActivityId,BadgeName,DistanceIntervallBegin,DistanceIntervallEnd,BadgeImage,BadgeDescription")] Badge badge)
        {
            if (id != badge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(badge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BadgeExists(badge.Id))
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
            ViewData["ActivityId"] = new SelectList(_context.Activity, "Id", "Id", badge.ActivityId);
            return View(badge);
        }

        // GET: Badges/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var badge = await _context.Badge
                .Include(b => b.Activity)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (badge == null)
            {
                return NotFound();
            }

            return View(badge);
        }

        // POST: Badges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var badge = await _context.Badge.FindAsync(id);
            _context.Badge.Remove(badge);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BadgeExists(string id)
        {
            return _context.Badge.Any(e => e.Id == id);
        }
    }
}
