using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace AthenaWebApp.Areas.Identity.IdentityModels
{
    // Add profile data for application users by adding properties to the AthenaIdentityUser class
    public class AthenaIdentityUser : IdentityUser
    {
        // Company MUSS hier stehen, sonst gibts Ärger mit Identity Insert!
//        public Company company { get; set; }

        //        [Key, ForeignKey("ProfileMeta")]
        [ForeignKey("CompanyId"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public byte[] ProfilePicture { get; set; }




        //        public virtual ICollection<IdentityUser> IdentityUsers { get; set; }
    }
}