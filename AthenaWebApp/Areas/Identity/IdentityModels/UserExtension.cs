using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace AthenaWebApp.Areas.Identity.IdentityModels
{
    // Add profile data for application users by adding properties to the UserExtension class
    public class UserExtension : IdentityUser
    {
        [ForeignKey("CompanyId"), DisplayName("Associated Company")]
        public Company Company { get; set; }

        [Display(Name = "Company")]
        public string CompanyId { get; set; }

        [Display(Name = "Registered since"), DataType(DataType.Date)]
        public DateTime RegisteredSince { get; set; } = DateTime.Now.Date;

        [Display(Name = "Last session")]
        public DateTime LastActivity { get; set; }

        [Display(Name = "Online Status")]
        public bool LoggedIn { get; set; }

        [Display(Name = "Picture")]
        public byte[] ProfilePicture { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
    }
}