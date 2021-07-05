using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Models
{
    public class UserActivity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ActivityId { get; set; }
        public string CompanyId { get; set; }

        // Distanzen
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public TimeSpan SumTime { get; set; }
        public double SumDistance { get; set; }

    }
}
