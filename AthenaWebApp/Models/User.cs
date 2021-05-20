using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AthenaWebApp.Models
{
    public class User
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Benutzer E-Mail")]
        public string UserMail { get; set; }
        public string Nickname { get; set; }

        [Display(Name = "Universität")]
        public int CompanyId { get; set; }


        public ICollection<Distance> Distances { get; set; }
         
    }
}
