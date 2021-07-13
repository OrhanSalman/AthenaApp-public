using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AthenaWebApp.Models
{
    public class Activity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string ActivityType { get; set; }
        public double MaxSpeed { get; set; }
        public string Description { get; set; }
        public bool SetManualyByUser { get; set; }
    }
}
