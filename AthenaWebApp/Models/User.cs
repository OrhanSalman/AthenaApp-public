using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserMail { get; set; }
        public string Nickname { get; set; }

//        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
