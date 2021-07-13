using System.ComponentModel.DataAnnotations;

namespace AthenaWebApp.Models
{
    public class Admin
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "E-Mail")]
        public string Email { get; set; }
    }
}
