using AthenaWebApp.Data;
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Controllers.MVC
{
    public class NewssController : Controller
    {
        private readonly Context _context;

        public NewssController(Context context)
        {
            _context = context;
        }

        // GET: Newss
        public async Task<IActionResult> Index()
        {
            var context = _context.News.Include(b => b.Company);
            return View(await context.ToListAsync());
        }

        // GET: Newss/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(b => b.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }



        // GET: Newss/Create
        [Authorize(Policy = "Create News")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Newss/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Create News")]
        public async Task<IActionResult> Create([Bind("Id,CompanyId,Title,FirstContent,SecondContent,ThirdContent,Foto,IsActive")] News news, IFormFile Image)
        {

            if (ModelState.IsValid)
            {
                // Image Upload
                if (Image != null)
                {
                    if (Image.Length > 0)
                    //Convert Image to byte and save to database
                    {
                        byte[] p1 = null;
                        using (var fs1 = Image.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                        news.Foto = p1;
                    }
                }

                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: Newss/Edit/5
        [Authorize(Policy = "Edit News")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: Newss/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Edit News")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,CompanyId,Title,FirstContent,SecondContent,ThirdContent,Foto,IsActive")] News news, IFormFile Image)
        {
            // ToDo: if general news is set to false, display message that a CompanyId has to set
            if (id != news.Id)
            {
                return NotFound();
            }

            // If the image shouldn't be edited, backup the actual image
            byte[] img = news.Foto;          // ToDo: This doesn't work

            if (ModelState.IsValid)
            {
                try
                {
                    // Image Upload
                    if (Image != null)
                    {
                        if (Image.Length > 0)
                        //Convert Image to byte and save to database
                        {
                            byte[] p1 = null;
                            using (var fs1 = Image.OpenReadStream())
                            using (var ms1 = new MemoryStream())
                            {
                                fs1.CopyTo(ms1);
                                p1 = ms1.ToArray();
                            }
                            news.Foto = p1;
                        }
                    }
                    else
                    {
                        news.Foto = img;
                    }

                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Id", news.CompanyId);
            return View(news);
        }

        // GET: Newss/Delete/5
        [Authorize(Policy = "Delete News")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(b => b.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: Newss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Delete News")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var news = await _context.News.FindAsync(id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(string id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
