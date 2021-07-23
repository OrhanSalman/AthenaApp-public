using AthenaWebApp.Areas.Identity.IdentityModels;
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace AthenaWebApp.Data
{
    public class Context : IdentityDbContext<UserExtension>
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            base.OnModelCreating(builder);
            //            builder.HasDefaultSchema("Athena");
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            /*
            builder.Entity<Template>(entity =>
            {
                entity.ToTable("Template");

            });
            */
        }

        public DbSet<AthenaWebApp.Models.Company> Company { get; set; }
        public DbSet<AthenaWebApp.Models.Template> Template { get; set; }
        public DbSet<AthenaWebApp.Models.Activity> Activity { get; set; }
        public DbSet<AthenaWebApp.Models.UserActivity> UserActivity { get; set; }
        public DbSet<AthenaWebApp.Models.Badge> Badge { get; set; }
        public DbSet<AthenaWebApp.Models.UserBadge> UserBadge { get; set; }
        public DbSet<AthenaWebApp.Models.News> News { get; set; }



    }

}