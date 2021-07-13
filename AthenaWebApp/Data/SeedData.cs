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
                        Id = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                        Name = "Supervisor",
                        NormalizedName = "SUPERVISOR"
                    },
                    new IdentityRole
                    {
                        Id = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                        Name = "MobileUser",
                        NormalizedName = "MOBILEUSER"
                    }
                );
                context.SaveChanges();
                // ToDo: Set Users to Roles

                context.Company.AddRange(
                    new Company
                    {
                        Id = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76",
                        CompanyName = "Universität Siegen",
                        Country = "Germany",
                        EmailContext = "uni-siegen.de"
                    },
                    new Company
                    {
                        Id = "fca9a2af-1ed2-430c-b9ae-e5476d36ca77",
                        CompanyName = "Athena",
                        Country = "International",
                        EmailContext = "athena.com"
                    },
                    new Company
                    {
                        Id = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78",
                        CompanyName = "University Maribor",
                        Country = "Slovenia",
                        EmailContext = "um.si"
                    },
                    new Company
                    {
                        Id = "hca9a2af-1ed2-430c-b9ae-e5476d36ca79",
                        CompanyName = "the Polytechnic Institute Porto",
                        Country = "Portugal",
                        EmailContext = "ipp.pt"
                    },
                    new Company
                    {
                        Id = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80",
                        CompanyName = "Hellenic Mediterranean University",
                        Country = "Greece",
                        EmailContext = "hmu.gr"
                    },
                    new Company
                    {
                        Id = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81",
                        CompanyName = "Unicusano",
                        Country = "Italy",
                        EmailContext = "pec.it"
                    },
                    new Company
                    {
                        Id = "kca9a2af-1ed2-430c-b9ae-e5476d36ca82",
                        CompanyName = "University Orléans",
                        Country = "Italy",
                        EmailContext = "univ-orleans.fr"
                    },
                    new Company
                    {
                        Id = "lca9a2af-1ed2-430c-b9ae-e5476d36ca83",
                        CompanyName = "Vilnius Tech",
                        Country = "Litaun",
                        EmailContext = "vilniustech.it"

                    }
                    );
                context.SaveChanges();

                // Später löschen
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
                        CompanyId = "fca9a2af-1ed2-430c-b9ae-e5476d36ca77"
                    });
                context.SaveChanges();

                context.UserRoles.AddRange(
                    new IdentityUserRole<string>
                    {
                        UserId = "d52f4e8e-7a86-4279-82d1-749b67b99a92",
                        RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1"
                    });
                context.SaveChanges();
                // Später löschen



                /*
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
                    },
                    new UserExtension
                    {
                        Id = "e52f4e8e-7a86-4279-82d1-749b67b99a93",
                        Email = "supervisor@um.si",
                        NormalizedEmail = "SUPERVISOR@UM.SI",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "Supervisor",
                        NormalizedUserName = "SUPERVISOR",
                        CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78"
                    },
                    new UserExtension
                    {
                        Id = "f52f4e8e-7a86-4279-82d1-749b67b99a94",
                        Email = "supervisor@uni-siegen.de",
                        NormalizedEmail = "SUPERVISOR@UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "Supervisor",
                        NormalizedUserName = "SUPERVISOR",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "g52f4e8e-7a86-4279-82d1-749b67b99a95",
                        Email = "supervisor@ipp.pt",
                        NormalizedEmail = "SUPERVISOR@IPP.PT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "Supervisor",
                        NormalizedUserName = "SUPERVISOR",
                        CompanyId = "hca9a2af-1ed2-430c-b9ae-e5476d36ca79"
                    },
                    new UserExtension
                    {
                        Id = "h52f4e8e-7a86-4279-82d1-749b67b99a96",
                        Email = "supervisor@hmu.gr",
                        NormalizedEmail = "SUPERVISOR@HMU.GR",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "Supervisor",
                        NormalizedUserName = "SUPERVISOR",
                        CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80"
                    },
                    new UserExtension
                    {
                        Id = "i52f4e8e-7a86-4279-82d1-749b67b99a97",
                        Email = "supervisor@pec.it",
                        NormalizedEmail = "SUPERVISOR@PEC.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "Supervisor",
                        NormalizedUserName = "SUPERVISOR",
                        CompanyId = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81"
                    },
                    new UserExtension
                    {
                        Id = "j52f4e8e-7a86-4279-82d1-749b67b99a98",
                        Email = "supervisor@univ-orleans.fr",
                        NormalizedEmail = "SUPERVISOR@UNIV-ORLEANS.FR",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "Supervisor",
                        NormalizedUserName = "SUPERVISOR",
                        CompanyId = "kca9a2af-1ed2-430c-b9ae-e5476d36ca82"
                    },
                    new UserExtension
                    {
                        Id = "k52f4e8e-7a86-4279-82d1-749b67b99a99",
                        Email = "supervisor@vilniustech.it",
                        NormalizedEmail = "SUPERVISOR@VILNIUSTECH.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "Supervisor",
                        NormalizedUserName = "SUPERVISOR",
                        CompanyId = "lca9a2af-1ed2-430c-b9ae-e5476d36ca83"
                    },
                    new UserExtension
                    {
                        Id = "l52f4e8e-7a86-4279-82d1-749b67b99a100",
                        Email = "hans.peter@um.si",
                        NormalizedEmail = "HANS.PETER@UM.SI",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "HansPeter",
                        NormalizedUserName = "HANSPETER",
                        CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78"
                    },
                    new UserExtension
                    {
                        Id = "m52f4e8e-7a86-4279-82d1-749b67b99a101",
                        Email = "mobileUser@um.si",
                        NormalizedEmail = "MOBILEUSER@UM.SI",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78"
                    },
                    new UserExtension
                    {
                        Id = "n52f4e8e-7a86-4279-82d1-749b67b99a102",
                        Email = "mobileUser@um.si",
                        NormalizedEmail = "MOBILEUSER@UM.SI",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78"
                    },
                    new UserExtension
                    {
                        Id = "o52f4e8e-7a86-4279-82d1-749b67b99a103",
                        Email = "mobileUser@um.si",
                        NormalizedEmail = "MOBILEUSER@UM.SI",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78"
                    },
                    new UserExtension
                    {
                        Id = "p52f4e8e-7a86-4279-82d1-749b67b99a104",
                        Email = "mobileUser@um.si",
                        NormalizedEmail = "MOBILEUSER@UM.SI",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78"
                    },
                    new UserExtension
                    {
                        Id = "q52f4e8e-7a86-4279-82d1-749b67b99a105",
                        Email = "mobileUser@um.si",
                        NormalizedEmail = "MOBILEUSER@UM.SI",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78"
                    },
                    new UserExtension
                    {
                        Id = "r52f4e8e-7a86-4279-82d1-749b67b99a106",
                        Email = "mobileUser@uni-siegen.de",
                        NormalizedEmail = "MOBILEUSER@UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "s52f4e8e-7a86-4279-82d1-749b67b99a107",
                        Email = "mobileUser@uni-siegen.de",
                        NormalizedEmail = "MOBILEUSER@UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "t52f4e8e-7a86-4279-82d1-749b67b99a108",
                        Email = "orhan.salman@student.uni-siegen.de",
                        NormalizedEmail = "MOBILEUSER@UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "u52f4e8e-7a86-4279-82d1-749b67b99a109",
                        Email = "mobileUser@uni-siegen.de",
                        NormalizedEmail = "MOBILEUSER@UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "v52f4e8e-7a86-4279-82d1-749b67b99a110",
                        Email = "mobileUser@uni-siegen.de",
                        NormalizedEmail = "MOBILEUSER@UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "w52f4e8e-7a86-4279-82d1-749b67b99a111",
                        Email = "mobileUser@uni-siegen.de",
                        NormalizedEmail = "MOBILEUSER@UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "x52f4e8e-7a86-4279-82d1-749b67b99a112",
                        Email = "mobileUser@uni-siegen.de",
                        NormalizedEmail = "MOBILEUSER@UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "y52f4e8e-7a86-4279-82d1-749b67b99a113",
                        Email = "mobileUser@uni-siegen.de",
                        NormalizedEmail = "MOBILEUSER@UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "z52f4e8e-7a86-4279-82d1-749b67b99a114",
                        Email = "mobileUser@uni-siegen.de",
                        NormalizedEmail = "MOBILEUSER@UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "a52f4e8e-7a86-4279-82d1-749b67b99a115",
                        Email = "mobileUser@ipp.it",
                        NormalizedEmail = "MOBILEUSER@IPP.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "hca9a2af-1ed2-430c-b9ae-e5476d36ca79"
                    },
                    new UserExtension
                    {
                        Id = "b52f4e8e-7a86-4279-82d1-749b67b99a116",
                        Email = "mobileUser@ipp.it",
                        NormalizedEmail = "MOBILEUSER@IPP.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "hca9a2af-1ed2-430c-b9ae-e5476d36ca79"
                    },
                    new UserExtension
                    {
                        Id = "c52f4e8e-7a86-4279-82d1-749b67b99a117",
                        Email = "mobileUser@ipp.it",
                        NormalizedEmail = "MOBILEUSER@IPP.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "hca9a2af-1ed2-430c-b9ae-e5476d36ca79"
                    },
                    new UserExtension
                    {
                        Id = "d52f4e8e-7a86-4279-82d1-749b67b99a23",
                        Email = "mobileUser@ipp.it",
                        NormalizedEmail = "MOBILEUSER@IPP.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "hca9a2af-1ed2-430c-b9ae-e5476d36ca79"
                    },
                    new UserExtension
                    {
                        Id = "e52f4e8e-7a86-4279-82d1-749b67b99a25",
                        Email = "mobileUser@hmu.gr",
                        NormalizedEmail = "MOBILEUSER@HMU.GR",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80"
                    },
                    new UserExtension
                    {
                        Id = "f52f4e8e-7a86-4279-82d1-749b67b99a20",
                        Email = "mobileUser@hmu.gr",
                        NormalizedEmail = "MOBILEUSER@HMU.GR",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80"
                    },
                    new UserExtension
                    {
                        Id = "g52f4e8e-7a86-4279-82d1-749b67b99a11",
                        Email = "mobileUser@hmu.gr",
                        NormalizedEmail = "MOBILEUSER@HMU.GR",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80"
                    },
                    new UserExtension
                    {
                        Id = "h52f4e8e-7a86-4279-82d1-749b67b99a22",
                        Email = "mobileUser@hmu.gr",
                        NormalizedEmail = "MOBILEUSER@HMU.GR",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80"
                    },
                    new UserExtension
                    {
                        Id = "i52f4e8e-7a86-4279-82d1-749b67b99a23",
                        Email = "mobileUser@hmu.gr",
                        NormalizedEmail = "MOBILEUSER@HMU.GR",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80"
                    },
                    new UserExtension
                    {
                        Id = "j52f4e8e-7a86-4279-82d1-749b67b99a24",
                        Email = "mobileUser@hmu.gr",
                        NormalizedEmail = "MOBILEUSER@HMU.GR",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80"
                    },
                    new UserExtension
                    {
                        Id = "k52f4e8e-7a86-4279-82d1-749b67b99a25",
                        Email = "mobileUser@pec.it",
                        NormalizedEmail = "MOBILEUSER@PEC.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81"
                    },
                    new UserExtension
                    {
                        Id = "l52f4e8e-7a86-4279-82d1-749b67b99a26",
                        Email = "mobileUser@pec.it",
                        NormalizedEmail = "MOBILEUSER@PEC.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81"
                    },
                    new UserExtension
                    {
                        Id = "m52f4e8e-7a86-4279-82d1-749b67b99a27",
                        Email = "mobileUser@pec.it",
                        NormalizedEmail = "MOBILEUSER@PEC.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81"
                    },
                    new UserExtension
                    {
                        Id = "n52f4e8e-7a86-4279-82d1-749b67b99a28",
                        Email = "mobileUser@pec.it",
                        NormalizedEmail = "MOBILEUSER@PEC.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81"
                    },
                    new UserExtension
                    {
                        Id = "o52f4e8e-7a86-4279-82d1-749b67b99a29",
                        Email = "mobileUser@pec.it",
                        NormalizedEmail = "MOBILEUSER@PEC.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81"
                    },
                    new UserExtension
                    {
                        Id = "p52f4e8e-7a86-4279-82d1-749b67b99a30",
                        Email = "mobileUser@pec.it",
                        NormalizedEmail = "MOBILEUSER@PEC.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81"
                    },
                    new UserExtension
                    {
                        Id = "q52f4e8e-7a86-4279-82d1-749b67b99a31",
                        Email = "mobileUser@univ-orleans.fr",
                        NormalizedEmail = "MOBILEUSER@UNIV-ORLEANS.FR",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "kca9a2af-1ed2-430c-b9ae-e5476d36ca82"
                    },
                    new UserExtension
                    {
                        Id = "r52f4e8e-7a86-4279-82d1-749b67b99a32",
                        Email = "mobileUser@univ-orleans.fr",
                        NormalizedEmail = "MOBILEUSER@UNIV-ORLEANS.FR",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "kca9a2af-1ed2-430c-b9ae-e5476d36ca82"
                    },
                    new UserExtension
                    {
                        Id = "s52f4e8e-7a86-4279-82d1-749b67b99a33",
                        Email = "mobileUser@univ-orleans.fr",
                        NormalizedEmail = "MOBILEUSER@UNIV-ORLEANS.FR",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "kca9a2af-1ed2-430c-b9ae-e5476d36ca82"
                    },
                    new UserExtension
                    {
                        Id = "t52f4e8e-7a86-4279-82d1-749b67b99a34",
                        Email = "mobileUser@univ-orleans.fr",
                        NormalizedEmail = "MOBILEUSER@UNIV-ORLEANS.FR",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "kca9a2af-1ed2-430c-b9ae-e5476d36ca82"
                    },
                    new UserExtension
                    {
                        Id = "u42f4e8e-7a86-4279-82d1-749b67b99a35",
                        Email = "mobileUser@univ-orleans.fr",
                        NormalizedEmail = "MOBILEUSER@UNIV-ORLEANS.FR",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "kca9a2af-1ed2-430c-b9ae-e5476d36ca82"
                    },
                    new UserExtension
                    {
                        Id = "u52f4e8e-7a86-4279-82d1-749b67b99a13",
                        Email = "mobileUser@vilniustech.it",
                        NormalizedEmail = "MOBILEUSER@VILNIUSTECH.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "lca9a2af-1ed2-430c-b9ae-e5476d36ca83"
                    },
                    new UserExtension
                    {
                        Id = "v52f4e8e-7a86-4279-82d1-749b67b99a136",
                        Email = "mobileUser@vilniustech.it",
                        NormalizedEmail = "MOBILEUSER@VILNIUSTECH.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "lca9a2af-1ed2-430c-b9ae-e5476d36ca83"
                    },
                    new UserExtension
                    {
                        Id = "w52f4e8e-7a86-4279-82d1-749b67b99a37",
                        Email = "mobileUser@vilniustech.it",
                        NormalizedEmail = "MOBILEUSER@VILNIUSTECH.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "lca9a2af-1ed2-430c-b9ae-e5476d36ca83"
                    },
                    new UserExtension
                    {
                        Id = "x52f4e8e-7a86-4279-82d1-749b67b99a38",
                        Email = "mobileUser@vilniustech.it",
                        NormalizedEmail = "MOBILEUSER@VILNIUSTECH.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "lca9a2af-1ed2-430c-b9ae-e5476d36ca83"
                    },
                    new UserExtension
                    {
                        Id = "y52f4e8e-7a86-4279-82d1-749b67b99a39",
                        Email = "mobileUser@vilniustech.it",
                        NormalizedEmail = "MOBILEUSER@VILNIUSTECH.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "lca9a2af-1ed2-430c-b9ae-e5476d36ca83"
                    },
                    new UserExtension
                    {
                        Id = "z52f4e8e-7a86-4279-82d1-749b67b99a40",
                        Email = "mobileUser@vilniustech.it",
                        NormalizedEmail = "MOBILEUSER@VILNIUSTECH.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "mobileUser",
                        NormalizedUserName = "MOBILEUSER",
                        CompanyId = "lca9a2af-1ed2-430c-b9ae-e5476d36ca83"
                    }
                    );
                context.SaveChanges();


                context.UserRoles.AddRange(
                    new IdentityUserRole<string>
                    {
                        UserId = "d52f4e8e-7a86-4279-82d1-749b67b99a92",
                        RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "e52f4e8e-7a86-4279-82d1-749b67b99a93",
                        RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0" // Selbe wie ganz oben, für jeden Nutzer
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "f52f4e8e-7a86-4279-82d1-749b67b99a94",
                        RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "g52f4e8e-7a86-4279-82d1-749b67b99a95",
                        RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "h52f4e8e-7a86-4279-82d1-749b67b99a96",
                        RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "i52f4e8e-7a86-4279-82d1-749b67b99a97",
                        RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "j52f4e8e-7a86-4279-82d1-749b67b99a98",
                        RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "k52f4e8e-7a86-4279-82d1-749b67b99a99",
                        RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "l52f4e8e-7a86-4279-82d1-749b67b99a100",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2" // MobileUser Uni Maribor
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "m52f4e8e-7a86-4279-82d1-749b67b99a101",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "n52f4e8e-7a86-4279-82d1-749b67b99a102",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "o52f4e8e-7a86-4279-82d1-749b67b99a103",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "p52f4e8e-7a86-4279-82d1-749b67b99a104",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "q52f4e8e-7a86-4279-82d1-749b67b99a105",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "r52f4e8e-7a86-4279-82d1-749b67b99a106",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2" // MobileUser Uni Siegen
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "s52f4e8e-7a86-4279-82d1-749b67b99a107",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "t52f4e8e-7a86-4279-82d1-749b67b99a108",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "u52f4e8e-7a86-4279-82d1-749b67b99a109",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "v52f4e8e-7a86-4279-82d1-749b67b99a110",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "w52f4e8e-7a86-4279-82d1-749b67b99a111",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "x52f4e8e-7a86-4279-82d1-749b67b99a112",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "y52f4e8e-7a86-4279-82d1-749b67b99a113",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "z52f4e8e-7a86-4279-82d1-749b67b99a114",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "a52f4e8e-7a86-4279-82d1-749b67b99a115", // Mobile User Uni Porto
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "b52f4e8e-7a86-4279-82d1-749b67b99a116",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "c52f4e8e-7a86-4279-82d1-749b67b99a117",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "d52f4e8e-7a86-4279-82d1-749b67b99a118",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "e52f4e8e-7a86-4279-82d1-749b67b99a119", // MobileUser Hellenic Mediterranean University
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "f52f4e8e-7a86-4279-82d1-749b67b99a120",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "g52f4e8e-7a86-4279-82d1-749b67b99a121",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "h52f4e8e-7a86-4279-82d1-749b67b99a122",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "i52f4e8e-7a86-4279-82d1-749b67b99a123",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "j52f4e8e-7a86-4279-82d1-749b67b99a124", // MobileUser Unicusano
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "k52f4e8e-7a86-4279-82d1-749b67b99a125",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "l52f4e8e-7a86-4279-82d1-749b67b99a126",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "m52f4e8e-7a86-4279-82d1-749b67b99a127",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "n52f4e8e-7a86-4279-82d1-749b67b99a128",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "o52f4e8e-7a86-4279-82d1-749b67b99a129",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "p52f4e8e-7a86-4279-82d1-749b67b99a130",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "q52f4e8e-7a86-4279-82d1-749b67b99a131", // Mobile User University of Orléans
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "r52f4e8e-7a86-4279-82d1-749b67b99a132",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "s52f4e8e-7a86-4279-82d1-749b67b99a133",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "t52f4e8e-7a86-4279-82d1-749b67b99a134",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "u52f4e8e-7a86-4279-82d1-749b67b99a135",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "v52f4e8e-7a86-4279-82d1-749b67b99a136", // Vilnius Tech MobileUser
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "w52f4e8e-7a86-4279-82d1-749b67b99a137",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "x52f4e8e-7a86-4279-82d1-749b67b99a138",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "y52f4e8e-7a86-4279-82d1-749b67b99a139",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "z52f4e8e-7a86-4279-82d1-749b67b99a140",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    }
                    );
                context.SaveChanges();
                */


                // Activites 

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
                        StopTime = new DateTime(2021, 07, 13, 15, 22, 05),     
                        SumTime = new TimeSpan(0, 20, 0, 0),                   // StopTime - StartTime // hours, minutes, seconds, milliseconds.
                        SumDistance = 5125                                     // Meters
                    });
                context.SaveChanges();
            }
        }
    }
}