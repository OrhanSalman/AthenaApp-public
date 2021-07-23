using AthenaWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Activity = System.Diagnostics.Activity;
using AthenaWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace AthenaWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, Context context)
        {
            _context = context;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            /*
            var news = _context.News.SingleAsync();
            return View(news);
            */
            
            return View();
            
        }

        [AllowAnonymous]
        public  IActionResult Dashboard()
        {
            /*
             return View( _context.UserActivity
                 .Include(b => b.Activity)
                 .Include(b => b.Company)
                 .Include(b => b.UserExtension)
                 .ToListAsync());
             */

            return View();
        }


        

       

        // GET: UserActivities

        

        [AllowAnonymous]
        public IActionResult AdPage()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
