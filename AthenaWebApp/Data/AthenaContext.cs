using Microsoft.EntityFrameworkCore;
using AthenaWebApp.Models;

namespace AthenaWebApp.Data
{
    public class AthenaContext : DbContext
    {
        public AthenaContext(DbContextOptions<AthenaContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Distance> Distances { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}