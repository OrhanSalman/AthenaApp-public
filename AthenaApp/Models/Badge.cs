using AthenaApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AthenaWebApp.Models
{
    public class Badge
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [ForeignKey("ActivityId")]
        public Activity Activity { get; set; }

        [Display(Name = "Activity")]
        public string ActivityId { get; set; } // One Activitiy, lot of Badges // A badge is in relation with a activity
        public string BadgeName { get; set; }
        public double DistanceIntervallBegin { get; set; }
        public double DistanceIntervallEnd { get; set; }
        [Display(Name = "Picture")]
        public byte[] BadgeImage { get; set; }
        public string BadgeDescription { get; set; }
    }
}