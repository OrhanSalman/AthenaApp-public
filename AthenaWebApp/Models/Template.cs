using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AthenaWebApp.Data;
using AthenaWebApp.Models;

namespace AthenaWebApp.Models
{
    public class Template
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TemplateId { get; set; }

        [ForeignKey("UserId"), DisplayName("Created by")]
        public User User { get; set; }
        public string UserId { get; set; }
        public string TemplateTitle { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeCreated { get; set; } = DateTime.Now;
//        public virtual User User  { get; set; }
    }
}
