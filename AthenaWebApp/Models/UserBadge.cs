using AthenaWebApp.Areas.Identity.IdentityModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AthenaWebApp.Models
{
    public class UserBadge
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [ForeignKey("BadgeId")]
        public Badge Badge { get; set; }
        public string BadgeId { get; set; }

        [ForeignKey("UserId")]
        public UserExtension UserExtension { get; set; }
        public string UserId { get; set; }
    }
}
