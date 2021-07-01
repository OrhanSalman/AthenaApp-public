using System;
using System.Collections.Generic;
using System.Text;

namespace AthenaApp.Models
{
    class UserActivity
    {
//        public string Id { get; set; }
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
