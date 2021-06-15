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

            if(context.Users.Any())
            {
                return;
            }
           
                context.Company.AddRange(
                    new Company
                    {
                        CompanyName = "Universität Siegen",
                        Country = "Germany",
                        EmailContext = "uni-siegen.de"
                    }
                );
                context.SaveChanges();

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
                        Email = "admin@athena.com",
                        EmailConfirmed = true,
                        PasswordHash = "Test_123",
                        UserName = "Admin",
                        CompanyName = "Universität Siegen"
                    },
                    new UserExtension
                    {
                        Email = "supervisor@uni-siegen.de",
                        EmailConfirmed = true,
                        PasswordHash = "Test_123",
                        UserName = "Supervisor",
                        CompanyName = "Universität Siegen"
                    }
                );
                context.SaveChanges();

                context.Roles.AddRange(
                    new IdentityRole
                    {
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
            }
        }
    }
}