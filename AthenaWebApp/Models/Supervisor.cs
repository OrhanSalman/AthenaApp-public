using System.ComponentModel.DataAnnotations;

namespace AthenaWebApp.Models
{
    public class Supervisor
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Display(Name = "Responsible for")]
        public string Company { get; set; }
    }
}
