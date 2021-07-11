using System.Collections.Generic;

namespace AthenaApp.Models
{
    public class Activity
    {
        public string Id { get; set; }
        public string ActivityType { get; set; }
        public double MaxSpeed { get; set; }
        public string Description { get; set; }
        public bool SetManualyByUser { get; set; }
        public List<Activity> activitiesList { get; set; }

        public override string ToString()
        {
            return "Activity: " + Id + " " + ActivityType + " " + MaxSpeed + " " + Description + " " + SetManualyByUser;
        }
    }
}
