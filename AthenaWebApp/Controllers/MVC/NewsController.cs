using AthenaWebApp.Areas.Identity.IdentityModels;
using AthenaWebApp.Data;
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AthenaWebApp.Controllers.MVC
{
    public class NewsController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<UserExtension> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public NewsController(Context context, UserManager<UserExtension> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            var context = _context.News.Include(b => b.Company);
            return View(await context.ToListAsync());
        }

        // GET: News/Details/5
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



        // GET: News/Create
        [Authorize(Policy = "Create News")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Create News")]
        public async Task<IActionResult> Create([Bind("Id,Title,FirstContent,SecondContent,ThirdContent,Foto,IsActive")] News news, IFormFile Image)
        {
            UserExtension user = await _userManager.GetUserAsync(User);
            news.CompanyId = user.CompanyId.ToString();

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

        // GET: News/Edit/5
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

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Edit News")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,FirstContent,SecondContent,ThirdContent,Foto,IsActive")] News news, IFormFile Image)
        {
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
            return View(news);
        }

        // GET: News/Delete/5
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

        // POST: News/Delete/5
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
