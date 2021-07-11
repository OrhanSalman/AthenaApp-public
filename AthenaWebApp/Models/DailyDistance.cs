using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
