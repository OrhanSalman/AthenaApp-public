
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Models
{
    public class User 
    {
        public User()
        {
            UserTemplates = new HashSet<Template>();
          }


        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Display(Name = "E-Mail Confirmed")]
        public string EmailConfirmed { get; set; }

        [Display(Name = "University")]
        public string Company { get; set; }

        [Display(Name = "Registered on")]
        public DateTime RegisteredSince { get; set; }

        [Display(Name = "Last session")]
        public DateTime LastActivity { get; set; }

        [Display(Name = "Activ")]
        public bool LoggedIn { get; set; }

        [Display(Name = "Locked")]
        public bool Locked { get; set; }

        [Display(Name = "Picture")]
        public byte[] ProfilePicture { get; set; }

        public virtual ICollection<Template> UserTemplates { get; set; }
        
}
    
}
