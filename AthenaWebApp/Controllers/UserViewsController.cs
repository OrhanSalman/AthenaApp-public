using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AthenaWebApp.Data;
using AthenaWebApp.Models;
using AthenaWebApp.Areas.Identity.IdentityModels;

namespace AthenaWebApp.Controllers
{
    public class UserViewsController : Controller
    {
        private readonly AthenaIdentityUserDbContext _context;

        public UserViewsController(AthenaIdentityUserDbContext context)
        {
            _context = context;
        }

        // GET: UserViews
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: UserViews/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userView = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userView == null)
            {
                return NotFound();
            }

            return View(userView);
        }

        // GET: UserViews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserViews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,UserMail,CompanyId,Company")] AthenaIdentityUser userView)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userView);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userView);
        }

        // GET: UserViews/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userView = await _context.Users.FindAsync(id);
            if (userView == null)
            {
                return NotFound();
            }
            return View(userView);
        }

        // POST: UserViews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,UserMail,CompanyId,Company")] AthenaIdentityUser userView)
        {
            if (id != userView.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userView);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserViewExists(userView.Id))
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
            return View(userView);
        }

        // GET: UserViews/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userView = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userView == null)
            {
                return NotFound();
            }

            return View(userView);
        }

        // POST: UserViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userView = await _context.Users.FindAsync(id);
            _context.Users.Remove(userView);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserViewExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
