using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthenaWebApp.Data;
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AthenaWebApp.Areas.Identity.IdentityModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AthenaWebApp.Models.Claims;
using AthenaWebApp.Pagings;

namespace AthenaWebApp.Controllers.MVC
{
    // Admin Authorize hinzufügen

    public class TemplateController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<UserExtension> _userManager;

        public IActionResult Index()
        {
            return View();
        }

        //Constructor
        public TemplateController(Context context, UserManager<UserExtension> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        /************************************************************************************/
        ////Create template
        //Method to display 'CreateTemplate' page to update or create new template
        [HttpPost]
        public async Task<IActionResult> WriteTemplate(Template template)
        {
            string loggedInUserId = _userManager.GetUserId(User);
            template.UserId = loggedInUserId;

            if (template.TemplateId > 0)
                UpdateTemplate(template);
            else
                CreateTemplate(template);

            if (await SaveChanges())
                return RedirectToAction("AllTemplates");
            else
                return View(template);
        }
        [HttpGet]
        public ActionResult<Template> WriteTemplate(int? id)
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
        //Method to create the template
        public void CreateTemplate(Template template)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }
            _context.UserTemplates.Add(template);
        }

        //Method to update the template
        public void UpdateTemplate(Template template)
        {
            _context.UserTemplates.Update(template);
        }


        /************************************************************************************/
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
            return _context.UserTemplates.ToList();
        }


        /************************************************************************************/
        ////Display single template
        //Method to view a single template on SingleTemplateView
        public IActionResult SingleTemplate(int id)
        {
            var template = GetTemplateById(id);

            return View(template);
        }


        /************************************************************************************/
        ////Delete template
        //Method called by the view to delete the template
        public async Task<IActionResult> RemoveTemplate(int id)
        {
            DeleteTemplate(id);
            await SaveChanges();
            return RedirectToAction("AllTemplates");
        }

        //Method to delete the template from the database
        public void DeleteTemplate(int id)
        {
            _context.UserTemplates.Remove(GetTemplateById(id));
        }


        /************************************************************************************/
        ////Supportive methods
        //To get Templates by TemplateID
        public Template GetTemplateById(int id)
        {
            return _context.UserTemplates.FirstOrDefault(a => a.TemplateId == id);
        }

        //Method to save the changes in the database
        public Task<bool> SaveChanges()
        {
            return Task.FromResult(_context.SaveChanges() >= 0);
        }
    }
}
