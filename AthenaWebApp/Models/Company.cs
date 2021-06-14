using AthenaWebApp.Areas.Identity.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Models
{
    public class Company
    {

        [Key, Display(Name = "Company")]
        public string CompanyName { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "E-Mail Header")]
        public string EmailContext { get; set; }
    }
}
