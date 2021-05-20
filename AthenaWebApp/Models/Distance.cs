using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AthenaWebApp.Models
{
    public class Distance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float Meters { get; set; }
    }
}
