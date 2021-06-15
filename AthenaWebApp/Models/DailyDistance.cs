using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Models
{
    public class DailyDistance
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public double TotalMeter { get; set; }

        // ToDo: After 24h a mew Data Model has to be created
        public DateTime dateTime { get; set; } = DateTime.Now;
    }
}
