using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AthenaWebApp.Data;
using System;
using System.Linq;

namespace AthenaWebApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new UserContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<UserContext>>()))
            {
                // Look for any movies.
                if (context.User.Any())
                {
                    return;   // DB has been seeded
                }

                // Test Data Samples
                context.User.AddRange(
                    new User
                    {   
                        UserMail = "lena@uni-siegen.de",
                        Nickname = "Lena"
                    },

                    new User
                    {
                        UserMail = "jasmin@uni-siegen.de",
                        Nickname = "Jasmin"
                    },

                    new User
                    {
                        UserMail = "alex@uni-siegen.de",
                        Nickname = "Alex"
                    },

                    new User
                    {
                        UserMail = "raphael@uni-siegen.de",
                        Nickname = "Raphael"
                    },

                    new User
                    {
                        UserMail = "domenik@uni-siegen.de",
                        Nickname = "Domenik"
                    },

                    new User
                    {
                        UserMail = "orhan@uni-siegen.de",
                        Nickname = "Orhan"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}