using AthenaWebApp.Areas.Identity.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AthenaWebApp.Models;

namespace AthenaWebApp.Data
{
    public class Context : IdentityDbContext<UserExtension>
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        //        public virtual DbSet<Template> UserTemplates { get; set; }
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
                entity.HasKey(e => e.TemplateId)
                    .HasName("PK__UserTemplate");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.TemplateId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("template_id");

                entity.Property(e => e.TemplateTitle)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("template_title");

                entity.Property(e => e.DateTimeCreated)
                    .HasDefaultValueSql("getdate()");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTemplates)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserTemplate_fk_User");
            });
            */
        }

        public DbSet<AthenaWebApp.Models.Company> Company { get; set; }
        public DbSet<AthenaWebApp.Models.Template> Template { get; set; }

        public DbSet<AthenaWebApp.Models.Activity> Activity { get; set; }

        public DbSet<AthenaWebApp.Models.UserActivity> UserActivity { get; set; }

        public DbSet<AthenaWebApp.Models.Badge> Badge { get; set; }


    }

}