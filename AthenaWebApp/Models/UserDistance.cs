using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Models
{
    public class UserDistance
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [ForeignKey("Company")]
        public string UserId { get; set; }
        public string ActivityId { get; set; }
        public double Meter { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;

        // Counter: Daily activities
        
    }
}
