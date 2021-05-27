using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AthenaWebApp.Models
{
    public class Company
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Organisation")]
        public string CompanyName { get; set; }

        [Display(Name = "Land")]
        public string Country { get; set; }

        // A Company has one or more Users
//        [Display(Name = "Teilnehmer")]
//        public ICollection<IdentityUser> UserCount { get; set; }

        [Display(Name = "Erreichte Distanzen")]
        public int CollectedDistances { get; set; }

//        public User User { get; set; }
    }
}
