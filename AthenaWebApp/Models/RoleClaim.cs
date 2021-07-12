using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AthenaWebApp.Models
{
    public class RoleClaim
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        public string RoleId { get; set; }

        public RoleClaim()
        {
            Claims = new List<RoleClaimValues>();
        }

        public List<RoleClaimValues> Claims { get; set; }

    }
}
