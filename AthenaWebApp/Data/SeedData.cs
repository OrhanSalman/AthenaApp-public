using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AthenaWebApp.Data;
using System;
using System.Linq;
using AthenaWebApp.Areas.Identity.IdentityModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AthenaWebApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Context(
                serviceProvider.GetRequiredService<DbContextOptions<Context>>()))
            {

                if (context.Users.Any() && context.Roles.Any())
                {
                    return;
                }

                context.Roles.AddRange(
                    new IdentityRole
                    {
                        Id = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole
                    {
                        Name = "Supervisor",
                        NormalizedName = "SUPERVISOR"
                    },
                    new IdentityRole
                    {
                        Name = "MobileUser",
                        NormalizedName = "MOBILEUSER"
                    }
                );
                context.SaveChanges();
                // ToDo: Set Users to Roles

                context.Company.AddRange(
                    new Company
                    {

                        CompanyName = "Universität Siegen",
                        Country = "Germany",
                        EmailContext = "uni-siegen.de"
                    },
                    new Company
                    {
                        Id = "d33df339-1c29-42e3-bb45-6d7e6c990f1e",
                        CompanyName = "Athena",
                        Country = "International",
                        EmailContext = "athena.com"
                    });
                context.SaveChanges();


                context.Users.AddRange(
                    new UserExtension
                    {
                        Id = "d52f4e8e-7a86-4279-82d1-749b67b99a92",
                        Email = "admin@athena.com",
                        NormalizedEmail = "ADMIN@ATHENA.COM",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "Admin",
                        NormalizedUserName = "ADMIN",
                        CompanyId = "d33df339-1c29-42e3-bb45-6d7e6c990f1e"
                    });
                context.SaveChanges();

                context.UserRoles.AddRange(
                    new IdentityUserRole<string>
                    {
                        UserId = "d52f4e8e-7a86-4279-82d1-749b67b99a92",
                        RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1"
                    });
                context.SaveChanges();

                context.Activity.AddRange(
                    new Activity
                    {
                        Id = "t33df349-1c29-42e3-bf45-6d7e6c990f1h",
                        ActivityType = "Swimming",
                        Description = "Go swimming",
                        MaxSpeed = 10,
                        SetManualyByUser = true
                    });
                context.SaveChanges();


                context.UserActivity.AddRange(
                    new UserActivity
                    {
                        Id = "b51f4e8e-5b86-4279-82d1-749b67b99a98",
                        UserId = "d52f4e8e-7a86-4279-82d1-749b67b99a92",
                        ActivityId = "t33df349-1c29-42e3-bf45-6d7e6c990f1h",
                        CompanyId = "d33df339-1c29-42e3-bb45-6d7e6c990f1e",
                        StartTime = new DateTime(2021, 07, 13, 15, 02, 05),    //year, month, day, hour, min, seconds
                        StopTime = new DateTime(2021, 07, 13, 15, 02, 20),     
                        SumTime = new TimeSpan(0, 0, 15),                      // hours, minutes, seconds, milliseconds.
                        SumDistance = 119.89286339442317
                    });
                context.SaveChanges();
            }
        }
    }
}