using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthenaWebApp.Data;
using AthenaWebApp.Models;

namespace AthenaWebApp.Models
{
    public class Template
    {
        public int TemplateId { get; set; }

        public string UserId { get; set; }
        public string TemplateTitle { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public virtual User User  { get; set; }
    }
}
