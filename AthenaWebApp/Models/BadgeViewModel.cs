using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Models
{
    public class BadgeViewModel
    {
        public string ActivityId { get; set; } 

        [Display(Name = "Badge")]
        public string BadgeName { get; set; }

        [Display(Name = "Start")]
        public double DistanceIntervallBegin { get; set; }

        [Display(Name = "End")]
        public double DistanceIntervallEnd { get; set; }

        [Display(Name = "Picture")]
        public IFormFile BadgeImage { get; set; }

        [Display(Name = "Description")]
        public string BadgeDescription { get; set; }
    }
}
