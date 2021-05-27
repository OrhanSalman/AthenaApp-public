using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AthenaWebApp.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }

        // A Company has one or more Users
        public ICollection<UserView> Users { get; set; }

//        public User User { get; set; }
    }
}
