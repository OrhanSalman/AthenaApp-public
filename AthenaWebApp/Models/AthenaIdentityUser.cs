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
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public byte[] ProfilePicture { get; set; }

    }
}
