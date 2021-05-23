using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AthenaWebApp.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the AthenaIdentityUser class
    public class AthenaIdentityUser : IdentityUser
    {
        /*
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Display(Name = "Nickname")]
        public string Nickname { get; set; }

        [Display(Name = "Uni-Kennschlüssel")]
        public int CompanyId { get; set; }

        [Display(Name = "Universität")]
        public int CompanyName { get; set; }
        */
    }
}
