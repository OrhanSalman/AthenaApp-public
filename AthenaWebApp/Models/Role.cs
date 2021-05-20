using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Models
{
    public class Role
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int IsAdmin { get; set; }
        public int IsSupervisor { get; set; }
        public int IsUser { get; set; }
//        public int UserId { get; set; }
    }
}
