using AthenaWebApp.Areas.Identity.IdentityModels;
using AthenaWebApp.Data;
using AthenaWebApp.Models;
using AthenaWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
    public class TemplatesController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<UserExtension> _userManager;
        private readonly EmailSender _emailSender;

        public TemplatesController(Context context, UserManager<UserExtension> userManager, IEmailSender emailSender)

        {
            _context = context;
            _userManager = userManager;
            _emailSender = (EmailSender)emailSender;

        }

        // GET: Templates
        public async Task<IActionResult> Index()
        {
            /*
            var athenaWebAppContext = _context.Template.Include(t => t.UserId);
            return View(await athenaWebAppContext.ToListAsync());
            */
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            string theUser = "";
            
            if (currentUser.IsInRole("Admin") == false)
            {
                theUser = _userManager.GetUserId(User);
                return View(await _context.Template
                .Include(i => i.UserExtension)
                .Where(i => i.UserId == theUser)
                .ToListAsync());
            }
            else // Admin can see all templates
            {
                return View(await _context.Template
                .Include(i => i.UserExtension)
                .ToListAsync());
            }
        }

        // GET: Templates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Template
                .Include(t => t.UserId)
                .FirstOrDefaultAsync(m => m.TemplateId == id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // GET: Templates/Create
        [Authorize(Policy = "Create Template")]
        public ActionResult<Template> Create(int? id)
        {
            if (id == null)
            {
                return View(new Template());
            }
            else
            {
                var template = GetTemplateById((int)id);
                return View(template);
            }
        }

        // POST: Templates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // Create template
        // Method to display 'CreateTemplate' page to update or create new template

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Create Template")]
        public async Task<IActionResult> Create(Template template)
        {
            string loggedInUserId = _userManager.GetUserId(User);
            template.UserId = loggedInUserId;

            if (template.TemplateId > 0)
                UpdateTemplate(template);
            else
                CreateTemplate(template);

            if (await SaveChanges())
                return RedirectToAction("Index");
            else
                return View(template);
        }

        // GET: Templates/Edit/5
        [Authorize(Policy = "Edit Template")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Template.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<UserExtension>(), "Id", "Id", template.UserId);
            return View(template);
        }

        // POST: Templates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Edit Template")]
        public async Task<IActionResult> Edit(int id, [Bind("TemplateId,UserId,TemplateTitle,Description,DateTimeCreated")] Template template)
        {
            if (id != template.TemplateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(template);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemplateExists(template.TemplateId))
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
            ViewData["UserId"] = new SelectList(_context.Set<UserExtension>(), "Id", "Id", template.UserId);
            return View(template);
        }

        /*
        // GET: Templates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Template
                .Include(t => t.UserId)
                .FirstOrDefaultAsync(m => m.TemplateId == id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }
        */



        // POST: Templates/Send
        // Filters the Users with the same companyID and sends them the newsletter -> e.g. uni.siegen.de students
        [Authorize(Policy = "Send Template")]
        public async Task<ActionResult> Send(int id, string userId)
        {
            bool send = true;   // default auf false setzen

            /*
            // Display "To you really want to send this template to all User of the specific Company?
            if ("yes.gedrückt")
            {
                send = true;
            }
            */

            if (send == true)
            {
                var template = await _context.Template.FindAsync(id);
                var getCompanyIdFromUserId = _context.Users.Where(i => i.Id == userId)
                .Select(i => i.CompanyId)
                .FirstOrDefault()
                .ToString();

                var getUsersOfCompany = _context.Users.Where(i => i.CompanyId == getCompanyIdFromUserId)
                    .Select(i => i.Email)
                    .ToList();

                var emailusers = await _context.Users.ToListAsync();

                foreach (var userMail in getUsersOfCompany)
                {
                    await _emailSender.SendEmailAsync(userMail, template.TemplateTitle = "", template.Description);
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // POST: Templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Delete Template")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var template = await _context.Template.FindAsync(id);
            _context.Template.Remove(template);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        

        // Here start Template Methods -> Raphael

        private bool TemplateExists(int id)
        {
            return _context.Template.Any(e => e.TemplateId == id);
        }

        //Method to create the template
        [Authorize(Policy = "Create Template")]
        public void CreateTemplate(Template template)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }
            _context.Template.Add(template);
        }

        //Method to update the template
        [Authorize(Policy = "Edit Template")]
        public void UpdateTemplate(Template template)
        {
            _context.Template.Update(template);
        }


        /************************************************************************************
        ////Display all Templates
        //Method to display all Templates which is received from 'GetAllTemplates' method
        public IActionResult AllTemplates()
        {
            var Templates = GetAllTemplates();
            return View(Templates);
        }

        //Method to get all Templates in the database
        public IEnumerable<Template> GetAllTemplates()
        {
            return _context.Template.ToList();
        }
        */

        /************************************************************************************/
        ////Display single template
        //Method to view a single template on SingleTemplateView
        [Authorize(Policy = "Edit Template")]
        public IActionResult SingleTemplate(int id)
        {
            var template = GetTemplateById(id);

            return View(template);
        }


        /************************************************************************************/
        ////Delete template
        //Method called by the view to delete the template
        [Authorize(Policy = "Delete Template")]
        public async Task<IActionResult> Delete(int id)
        {
            _context.Template.Remove(GetTemplateById(id));
            await SaveChanges();
            return RedirectToAction("Index");
        }



        /************************************************************************************/
        ////Supportive methods
        //To get Templates by TemplateID
        [Authorize(Policy = "Edit Template")]
        public Template GetTemplateById(int id)
        {
            return _context.Template.FirstOrDefault(a => a.TemplateId == id);
        }

        //Method to save the changes in the database
        [Authorize(Policy = "Edit Template")]
        public Task<bool> SaveChanges()
        {
            return Task.FromResult(_context.SaveChanges() >= 0);
        }
    }
}