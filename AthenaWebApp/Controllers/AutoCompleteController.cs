using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AthenaWebApp.Data;
using AthenaWebApp.Models;

namespace AthenaWebApp.Controllers.MVC
{
    public class AutoCompleteController : Controller
    {
        private readonly Context _context;

        public AutoCompleteController(Context context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(_context.Company);
        }

        // For Autocomplete
        [HttpPost]
        public ActionResult PostAutocompleteAction(string searchTerm)
        {
            List<Company> companies;
            if (string.IsNullOrEmpty(searchTerm))
            {
                companies = _context.Company.ToList();
            }
            else
            {
                companies = _context.Company
                    .Where(s => s.CompanyName.StartsWith(searchTerm)).ToList();
            }
            return View(companies);
        }

        public JsonResult GetAutocompleteAction(string term)
        {
            List<string> companies = _context.Company.Where(s => s.CompanyName.StartsWith(term))
                .Select(x => x.CompanyName).ToList();
            return Json(companies, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }
    }
}
