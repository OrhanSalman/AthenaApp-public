using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AthenaWebApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserMail { get; set; }
        public string Nickname { get; set; }


        /*
                 [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
         */
    }
}
