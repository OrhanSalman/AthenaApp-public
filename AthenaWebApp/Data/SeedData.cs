using AthenaWebApp.Areas.Identity.IdentityModels;
using AthenaWebApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AthenaWebApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Context(
                serviceProvider.GetRequiredService<DbContextOptions<Context>>()))
            {

                if (context.Users.Any())
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
                /*
                context.Users.AddRange(
                    new UserExtension
                    {
                        Email = "lena@student.uni-siegen.de",
                        EmailConfirmed = true,
                        UserName = "Lena",
                        CompanyName = "Universität Siegen"
                    },
                    new UserExtension
                    {
                        Email = "jasmin@student.uni-siegen.de",
                        EmailConfirmed = true,
                        UserName = "Jasmin",
                        CompanyName = "Universität Siegen"
                    },
                    new UserExtension
                    {
                        Email = "domenik@student.uni-siegen.de",
                        EmailConfirmed = true,
                        UserName = "Domenik",
                        CompanyName = "Universität Siegen"
                    },
                    new UserExtension
                    {
                        Email = "alex@student.uni-siegen.de",
                        EmailConfirmed = true,
                        UserName = "Alex",
                        CompanyName = "Universität Siegen"
                    },
                    new UserExtension
                    {
                        Email = "raphael@student.uni-siegen.de",
                        EmailConfirmed = true,
                        UserName = "Raphael",
                        CompanyName = "Universität Siegen"
                    },
                    new UserExtension
                    {
                        Email = "orhan@student.uni-siegen.de",
                        EmailConfirmed = true,
                        UserName = "Orhan",
                        CompanyName = "Universität Siegen"
                    },
                    new UserExtension
                    {
                        Email = "supervisor@uni-siegen.de",
                        EmailConfirmed = true,
                        PasswordHash = "Test_123",
                        UserName = "Supervisor",
                        CompanyName = "Universität Siegen"
                    });
                context.SaveChanges();
                */

            }
        }
    }
}