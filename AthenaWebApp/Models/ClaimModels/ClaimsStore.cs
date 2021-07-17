using System.Collections.Generic;
using System.Security.Claims;

namespace AthenaWebApp.Models.ClaimModels
{
    public class ClaimsStore
    {
        public static List<Claim> Claims = new List<Claim>()
        {
            new System.Security.Claims.Claim("Create", "Create"),
            new System.Security.Claims.Claim("Edit","Edit"),
            new System.Security.Claims.Claim("Details","Details"),
            new System.Security.Claims.Claim("Delete","Delete")
        };

        /*
        public static List<Claim> UserClaims = new List<Claim>()
        {
            new System.Security.Claims.Claim("Create User", "Create User"),
            new System.Security.Claims.Claim("Edit User","Edit User"),
            new System.Security.Claims.Claim("Details User","Details User"),
            new System.Security.Claims.Claim("Delete User","Delete User")
        };
        public static List<Claim> CompanyClaims = new List<Claim>()
        {
            new System.Security.Claims.Claim("Create Company", "Create Company"),
            new System.Security.Claims.Claim("Edit Company","Edit Company"),
            new System.Security.Claims.Claim("Details Company","Details Company"),
            new System.Security.Claims.Claim("Delete Company","Delete Company")
        };
        */
    }
}

