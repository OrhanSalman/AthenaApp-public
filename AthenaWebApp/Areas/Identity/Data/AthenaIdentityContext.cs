using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthenaWebApp.Areas.Identity.Data;
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AthenaWebApp.Data
{
    public class AthenaIdentityContext : IdentityDbContext<AthenaIdentityUser>
    {
        public AthenaIdentityContext(DbContextOptions<AthenaIdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }


        new public DbSet<Company> Companys { get; set; }
        new public DbSet<User> Users { get; set; }
        new public DbSet<Distance> Distances { get; set; }
        new public DbSet<Role> Roles { get; set; }
    }
}
