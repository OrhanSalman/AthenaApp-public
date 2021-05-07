using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebService.Models;


namespace WebService.Controllers
{
    public class UserDataController : ApiController
    {

        public IHttpActionResult GetAllUser()
        {
            IList<UserViewModel> user = null;

            using (var ctx = new AthenaMainConfiguration())
            {
                user = ctx.user.Include("All Users")
                            .Select(s => new UserViewModel()
                            {
                                Id = s.Id,
                                email = s.email,
                                pwhash = s.pwhash
                            }).ToList<UserViewModel>();
            }

            if (user.Count == 0)
            {
                return NotFound();
            }

            return Ok(user);
        }

        public IHttpActionResult GetUserById(int id)
        {
            UserViewModel user = null;

            using (var ctx = new AthenaMainConfiguration())
            {
                user = ctx.user.Include("User by Id")
                    .Where(s => s.Id == id)
                    .Select(s => new UserViewModel()
                    {
                        Id = s.Id,
                        email = s.email,
                        pwhash = s.pwhash
                    }).FirstOrDefault<UserViewModel>();
            }

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        // Other methods, like "GetUserByDistance"
        // POST
        // DELETE
        // ...
    }
}