using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AthenaWebApp.Data
{
    public static class SeedDataApplicationRoles
    {
        public static void SeedAspNetRoles(RoleManager<IdentityRole> roleManager)
        {
            List<string> roleList = new List<string>()
            {
                "Admin",
                "Supervision",
                "MobileUser"
            };

            foreach (var role in roleList)
            {
                var result = roleManager.RoleExistsAsync(role).Result;
                if (!result)
                {
                    roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}