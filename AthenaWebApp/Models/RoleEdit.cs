using AthenaWebApp.Areas.Identity.IdentityModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AthenaWebApp.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<UserExtension> Members { get; set; }
        public IEnumerable<UserExtension> NonMembers { get; set; }
    }
}
