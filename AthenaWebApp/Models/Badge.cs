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

        [Display(Name = "Badge")]
        public string BadgeName { get; set; }

        [Display(Name = "General Badge?")]
        public bool GeneralBadge { get; set; }

        [Display(Name = "Km for Badge")]
        public double DistanceForBadge { get; set; }

        [Display(Name = "Picture")]
        public byte[] BadgeImage { get; set; }

        [Display(Name = "Description")]
        public string BadgeDescription { get; set; }

    }
}