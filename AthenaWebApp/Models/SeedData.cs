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
            using (var context = new AthenaIdentityContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AthenaIdentityContext>>()))
            {
                // Look for any movies.
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                context.Companys.AddRange(
                    new Company
                    {
                        CompanyName = "Universität Siegen",
                        Country = "Germany"
                     }
                  );
                context.SaveChanges();


                context.Users.AddRange(
                    new User
                    {
                        UserMail = "lena@student.uni-siegen.de",
                        Nickname = "Lena",
                        CompanyId = 1
                    },
                    new User
                    {
                        UserMail = "jasmin@student.uni-siegen.de",
                        Nickname = "Jasmin",
                        CompanyId = 1
                    },
                    new User
                    {
                        UserMail = "domenik@student.uni-siegen.de",
                        Nickname = "Domenik",
                        CompanyId = 1
                    },
                    new User
                    {
                        UserMail = "alex@student.uni-siegen.de",
                        Nickname = "Alex",
                        CompanyId = 1
                    },
                    new User
                    {
                        UserMail = "raphael@student.uni-siegen.de",
                        Nickname = "Raphael",
                        CompanyId = 1
                    },
                    new User
                    {
                        UserMail = "orhan@student.uni-siegen.de",
                        Nickname = "Orhan",
                        CompanyId = 1
                    },
                    new User
                    {
                        UserMail = "admin@student.uni-siegen.de",
                        Nickname = "Admin",
                        CompanyId = 1
                    },
                    new User
                    {
                        UserMail = "supervisor@student.uni-siegen.de",
                        Nickname = "Supervisor",
                        CompanyId = 1
                    }
                );
                context.SaveChanges();

                context.Distances.AddRange(
                    new Distance
                    {
                        Meters = 1500,
                        UserId = 1
                    },
                    new Distance
                    {
                        Meters = 2500,
                        UserId = 2
                    },
                    new Distance
                    {
                        Meters = 3000,
                        UserId = 3
                    },
                    new Distance
                    {
                        Meters = 1500,
                        UserId = 4
                    },
                    new Distance
                    {
                        Meters = 2500,
                        UserId = 5
                    },
                    new Distance
                    {
                        Meters = 3000,
                        UserId = 6
                    }
                );
                context.SaveChanges();

                context.Roles.AddRange(

                    new Role
                    {
                        UserId = 1,
                        IsUser = 1
                    },
                    new Role
                    {
                        UserId = 2,
                        IsUser = 1
                    },
                    new Role
                    {
                        UserId = 3,
                        IsUser = 1
                    },
                    new Role
                    {
                        UserId = 4,
                        IsUser = 1
                    },
                    new Role
                    {
                        UserId = 5,
                        IsUser = 1
                    },
                    new Role
                    {
                        UserId = 6,
                        IsUser = 1
                    },
                    new Role
                    {
                        UserId = 7,
                        IsAdmin = 1
                    },
                    new Role
                    {
                        UserId = 8,
                        IsSupervisor = 1
                    }
                );
                context.SaveChanges();
            }
        }
    }
}