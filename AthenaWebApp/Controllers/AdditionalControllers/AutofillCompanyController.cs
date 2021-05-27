/*
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AthenaWebApp.Models;
//using Microsoft.AspNetCore.Mvc;

namespace AthenaWebApp.Controllers.AditionalControllers
{
    public class AutofillCompanyController : Controller
    {
        // GET: Home  
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Index(string Prefix)
        {
            //Note : you can bind same list from database  
            List<Company> ObjList = new List<Company>()
            {

                new Company {Id=1,CompanyName="Latur" },
                new Company {Id=2,CompanyName="Mumbai" },
                new Company {Id=3,CompanyName="Pune" },
                new Company {Id=4,CompanyName="Delhi" },
                new Company {Id=5,CompanyName="Dehradun" },
                new Company {Id=6,CompanyName="Noida" },
                new Company {Id=7,CompanyName="New Delhi" }

        };
            //Searching records from list using LINQ query  
            var Name = (from N in ObjList
                        where N.CompanyName.StartsWith(Prefix)
                        select new { N.CompanyName });
            return Json(Name, JsonRequestBehavior.AllowGet);
        }
    }
}
*/