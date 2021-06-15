using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Models
{
    public class SportActivity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Hiking { get; set; } = "Hiking";
        public string Swimming { get; set; } = "Swimming";
        public string Running { get; set; } = "Running";
        public string Biking { get; set; } = "Biking";
        public string Walking { get; set; } = "Walking";

    }
}
