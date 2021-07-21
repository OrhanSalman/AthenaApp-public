using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthenaWebApp.Controllers;
using AthenaWebApp.Models;
using AthenaWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using AthenaWebApp.Services;
//using AthenaWebApp.Repositories.PatternInterfaces;
using AthenaWebApp.Areas.Identity.IdentityModels;
using Newtonsoft.Json.Serialization;

namespace AthenaWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddControllers().AddNewtonsoftJson();
            services.AddRazorPages();
            services.AddDefaultIdentity<UserExtension>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Context>();
            /*
            services.AddAuthorization(opts => {
                opts.AddPolicy("Admin", policy => {
                    policy.RequireRole("Admin");
//                    policy.RequireClaim("Admin");
                });
            });
            */
            services.AddAuthorization(options =>
            {
                // User claim-section
                options.AddPolicy("Create User", policy => policy.RequireClaim("Create User", "true"));
                options.AddPolicy("Edit User", policy => policy.RequireClaim("Edit User", "true")); ;
                options.AddPolicy("Delete User", policy => policy.RequireClaim("Delete User", "true"));
                // Company claim-section
                options.AddPolicy("Create Company", policy => policy.RequireClaim("Create Company", "true"));
                options.AddPolicy("Edit Company", policy => policy.RequireClaim("Edit Company", "true")); ;
                options.AddPolicy("Delete Company", policy => policy.RequireClaim("Delete Company", "true"));
                // Activities claim-section
                options.AddPolicy("Create Activities", policy => policy.RequireClaim("Create Activities", "true"));
                options.AddPolicy("Edit Activities", policy => policy.RequireClaim("Edit Activities", "true")); ;
                options.AddPolicy("Delete Activities", policy => policy.RequireClaim("Delete Activities", "true"));
                // UserActivities claim-section
                options.AddPolicy("Create UserActivities", policy => policy.RequireClaim("Create UserActivities", "true"));
                options.AddPolicy("Edit UserActivities", policy => policy.RequireClaim("Edit UserActivities", "true")); ;
                options.AddPolicy("Delete UserActivities", policy => policy.RequireClaim("Delete UserActivities", "true"));
                // Badge claim-section
                options.AddPolicy("Create Badge", policy => policy.RequireClaim("Create Badge", "true"));
                options.AddPolicy("Edit Badge", policy => policy.RequireClaim("Edit Badge", "true")); ;
                options.AddPolicy("Delete Badge", policy => policy.RequireClaim("Delete Badge", "true"));
                // Template claim-section
                options.AddPolicy("Create Template", policy => policy.RequireClaim("Create Template", "true"));
                options.AddPolicy("Edit Template", policy => policy.RequireClaim("Edit Template", "true")); ;
                options.AddPolicy("Delete Template", policy => policy.RequireClaim("Delete Template", "true"));
                // Roles claim-section
                options.AddPolicy("Create Roles", policy => policy.RequireClaim("Create Roles", "true"));
                options.AddPolicy("Edit Roles", policy => policy.RequireClaim("Edit Roles", "true")); ;
                options.AddPolicy("Delete Roles", policy => policy.RequireClaim("Delete Roles", "true"));
                // Claims claim-section
                options.AddPolicy("Create Claims", policy => policy.RequireClaim("Create Claims", "true"));
                options.AddPolicy("Edit Claims", policy => policy.RequireClaim("Edit Claims", "true")); ;
                options.AddPolicy("Delete Claims", policy => policy.RequireClaim("Delete Claims", "true"));


            });

            services.AddMvc(options =>
            {
                // This pushes users to login if not authenticated
                options.Filters.Add(new AuthorizeFilter());

            });

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddDbContext<Context>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ServerConnection")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }


    }
}