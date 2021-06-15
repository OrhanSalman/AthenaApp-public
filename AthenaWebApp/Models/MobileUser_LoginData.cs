using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Models
{
    public class MobileUser_LoginData
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string SecurityStamp { get; set; }
    }
}
