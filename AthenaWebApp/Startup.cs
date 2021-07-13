//using AthenaWebApp.Repositories.PatternInterfaces;
using AthenaWebApp.Areas.Identity.IdentityModels;
using AthenaWebApp.Data;
using AthenaWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
