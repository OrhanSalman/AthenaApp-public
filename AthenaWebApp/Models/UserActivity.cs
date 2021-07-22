using AthenaWebApp.Areas.Identity.IdentityModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AthenaWebApp.Models
{
    public class UserActivity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }


        [ForeignKey("UserId")]
        public UserExtension UserExtension { get; set; }
        public string UserId { get; set; }


        [ForeignKey("ActivityId")]
        public Activity Activity { get; set; }
        public string ActivityId { get; set; }


        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        public string CompanyId { get; set; }


        // Distances
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public TimeSpan SumTime { get; set; }

        [Display(Name = "Meters")]
        public double SumDistance { get; set; }

    }
}
