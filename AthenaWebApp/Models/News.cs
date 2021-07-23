using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Models
{
    public class News
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [ForeignKey("CompanyId"), Display(Name = "News for")]
        public Company Company { get; set; }
        public string CompanyId { get; set; }
        public string Title { get; set; }

        public string FirstContent { get; set; }
        public string SecondContent { get; set; }
        public string ThirdContent { get; set; }

        public byte[] Foto { get; set; }
        public bool IsActive { get; set; }

    }
}
