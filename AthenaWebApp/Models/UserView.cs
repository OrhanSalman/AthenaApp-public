using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AthenaWebApp.Models
{
    public class UserView
    {
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "E-Mail")]
        public string UserMail { get; set; }

        [Display(Name = "Universität-Id")]
        public int CompanyId { get; set; }
        [Display(Name = "Universität")]
        public string Company { get; set; }

        


//        public ICollection<Distance> Distances { get; set; }
         
    }
}