using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AthenaWebApp.Models.Claims
{
    public class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new System.Security.Claims.Claim("Create Role", "Create Role"),
            new System.Security.Claims.Claim("Edit Role","Edit Role"),
            new System.Security.Claims.Claim("Delete Role","Delete Role")
        };
    }
}

