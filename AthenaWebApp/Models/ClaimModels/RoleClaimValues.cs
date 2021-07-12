using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Models.ClaimModels
{
    public class RoleClaimValues
    {
        [Key]
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public bool IsSelected { get; set; }
    }
}
