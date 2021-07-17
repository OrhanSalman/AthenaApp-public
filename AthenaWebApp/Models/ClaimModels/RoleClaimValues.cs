﻿using System.ComponentModel.DataAnnotations;

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
