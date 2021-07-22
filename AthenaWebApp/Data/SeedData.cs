using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AthenaWebApp.Data;
using System;
using System.Linq;
using AthenaWebApp.Areas.Identity.IdentityModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.IO;

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
                    },
                    new UserExtension
                    {
                        Id = "e52f4e8e-7a86-4279-82d1-749b67b99a93",
                        Email = "supervisor@um.si",
                        NormalizedEmail = "SUPERVISOR@UM.SI",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "Supervisor_Maribor",
                        NormalizedUserName = "SUPERVISOR_MARIBOR",
                        CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78"
                    },
                    new UserExtension
                    {
                        Id = "f52f4e8e-7a86-4279-82d1-749b67b99a94",
                        Email = "supervisor@uni-siegen.de",
                        NormalizedEmail = "SUPERVISOR@UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "Supervisor_Siegen",
                        NormalizedUserName = "SUPERVISOR_SIEGEN",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "g52f4e8e-7a86-4279-82d1-749b67b99a95",
                        Email = "supervisor@ipp.pt",
                        NormalizedEmail = "SUPERVISOR@IPP.PT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "Supervisor_Porto",
                        NormalizedUserName = "SUPERVISOR_PORTO",
                        CompanyId = "hca9a2af-1ed2-430c-b9ae-e5476d36ca79"
                    },
                    new UserExtension
                    {
                        Id = "h52f4e8e-7a86-4279-82d1-749b67b99a96",
                        Email = "supervisor@hmu.gr",
                        NormalizedEmail = "SUPERVISOR@HMU.GR",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "Supervisor_Hellenic",
                        NormalizedUserName = "SUPERVISOR_HELLENIC",
                        CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80"
                    },
                    new UserExtension
                    {
                        Id = "i52f4e8e-7a86-4279-82d1-749b67b99a97",
                        Email = "supervisor@pec.it",
                        NormalizedEmail = "SUPERVISOR@PEC.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "Supervisor_Unicusano",
                        NormalizedUserName = "SUPERVISOR_UNICUSANO",
                        CompanyId = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81"
                    },
                    new UserExtension
                    {
                        Id = "j52f4e8e-7a86-4279-82d1-749b67b99a98",
                        Email = "supervisor@univ-orleans.fr",
                        NormalizedEmail = "SUPERVISOR@UNIV-ORLEANS.FR",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "Supervisor_Orleans",
                        NormalizedUserName = "SUPERVISOR_ORLEANS",
                        CompanyId = "kca9a2af-1ed2-430c-b9ae-e5476d36ca82"
                    },
                    new UserExtension
                    {
                        Id = "k52f4e8e-7a86-4279-82d1-749b67b99a99",
                        Email = "supervisor@vilniustech.it",
                        NormalizedEmail = "SUPERVISOR@VILNIUSTECH.IT",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAENf84y15l279kb1SlN0OMFgdR6qf3ne15Ny4y2rQP+RtnLSBu1OYngVDaIvHcYdCBg==",
                        UserName = "Supervisor_Vilnius",
                        NormalizedUserName = "SUPERVISOR_VILNIUS",
                        CompanyId = "lca9a2af-1ed2-430c-b9ae-e5476d36ca83"
                    },
                    new UserExtension
                    {
                        Id = "l52f4e8e-7a86-4279-82d1-749b67b99a100",
                        Email = "hans.peter@um.si",
                        NormalizedEmail = "HANS.PETER@UM.SI",
                        EmailConfirmed = true,
                        UserName = "HansPeter",
                        NormalizedUserName = "HANSPETER",
                        CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78"
                    },
                    new UserExtension
                    {
                        Id = "m52f4e8e-7a86-4279-82d1-749b67b99a101",
                        Email = "Klaus.Hein@um.si",
                        NormalizedEmail = "KLAUS.HEIN@UM.SI",
                        EmailConfirmed = true,
                        UserName = "KlausHein",
                        NormalizedUserName = "KLAUSHEIN",
                        CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78"
                    },
                    new UserExtension
                    {
                        Id = "n52f4e8e-7a86-4279-82d1-749b67b99a102",
                        Email = "Hans@um.si",
                        NormalizedEmail = "HANS@UM.SI",
                        EmailConfirmed = true,
                        UserName = "Hans12",
                        NormalizedUserName = "HANS12",
                        CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78"
                    },
                    new UserExtension
                    {
                        Id = "o52f4e8e-7a86-4279-82d1-749b67b99a103",
                        Email = "Bernd.K@um.si",
                        NormalizedEmail = "BERND.K@UM.SI",
                        EmailConfirmed = true,
                        UserName = "Bernd45",
                        NormalizedUserName = "BERND45",
                        CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78"
                    },
                    new UserExtension
                    {
                        Id = "p52f4e8e-7a86-4279-82d1-749b67b99a104",
                        Email = "MarkusKlum@um.si",
                        NormalizedEmail = "MARKUSKLUM@UM.SI",
                        EmailConfirmed = true,
                        UserName = "MarkusK",
                        NormalizedUserName = "MARKUSK",
                        CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78"
                    },
                    new UserExtension
                    {
                        Id = "q52f4e8e-7a86-4279-82d1-749b67b99a105",
                        Email = "MohammedBeirut@um.si",
                        NormalizedEmail = "MOHAMMEDBEIRUT@UM.SI",
                        EmailConfirmed = true,
                        UserName = "Mohi23",
                        NormalizedUserName = "MOHI23",
                        CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78"
                    },
                    new UserExtension
                    {
                        Id = "r52f4e8e-7a86-4279-82d1-749b67b99a106",
                        Email = "FranzMü@uni-siegen.de",
                        NormalizedEmail = "FRANZMÜ@UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        UserName = "FranzM",
                        NormalizedUserName = "FRANZM",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "s52f4e8e-7a86-4279-82d1-749b67b99a107",
                        Email = "Herbert@uni-siegen.de",
                        NormalizedEmail = "HERBERT@UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        UserName = "Herbi",
                        NormalizedUserName = "HERBI",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "t52f4e8e-7a86-4279-82d1-749b67b99a108",
                        Email = "orhan.salman@student.uni-siegen.de",
                        NormalizedEmail = "ORHAN.SALMAN@STUDENT.UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        UserName = "OrhanS",
                        NormalizedUserName = "ORHANS",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "u52f4e8e-7a86-4279-82d1-749b67b99a109",
                        Email = "alexander.eickelmann@student.uni-siegen.de",
                        NormalizedEmail = "ALEXANDER.EICKELMANN@STUDENT.UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        UserName = "Alex",
                        NormalizedUserName = "ALEX",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "v52f4e8e-7a86-4279-82d1-749b67b99a110",
                        Email = "lena.falkenberg@student.uni-siegen.de",
                        NormalizedEmail = "LENA.FALKENBERG@STUDENT.UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        UserName = "LenaF",
                        NormalizedUserName = "LENAF",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "w52f4e8e-7a86-4279-82d1-749b67b99a111",
                        Email = "raphael.palombo@student.uni-siegen.de",
                        NormalizedEmail = "RAPHAEL.PALOMBO@STUDENT.UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        UserName = "RaphaelP",
                        NormalizedUserName = "RAPHAELP",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "x52f4e8e-7a86-4279-82d1-749b67b99a112",
                        Email = "manuel.neuer@torwart.uni-siegen.de",
                        NormalizedEmail = "MANUEL.NEUER@TORWART.UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        UserName = "Manu",
                        NormalizedUserName = "MANU",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "y52f4e8e-7a86-4279-82d1-749b67b99a113",
                        Email = "Jasmin.Freudenberg@student.uni-siegen.de",
                        NormalizedEmail = "JASMIN.FREUDENBERG@STUDENT.UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        UserName = "Mini",
                        NormalizedUserName = "MINI",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "z52f4e8e-7a86-4279-82d1-749b67b99a114",
                        Email = "CyraK@uni-siegen.de",
                        NormalizedEmail = "CYRAK@UNI-SIEGEN.DE",
                        EmailConfirmed = true,
                        UserName = "Cyra",
                        NormalizedUserName = "CYRA",
                        CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76"
                    },
                    new UserExtension
                    {
                        Id = "a52f4e8e-7a86-4279-82d1-749b67b99a115",
                        Email = "Estephanio@ipp.it",
                        NormalizedEmail = "ESTEPHANIO@IPP.IT",
                        EmailConfirmed = true,
                        UserName = "Estee",
                        NormalizedUserName = "ESTEE",
                        CompanyId = "hca9a2af-1ed2-430c-b9ae-e5476d36ca79"
                    },
                    new UserExtension
                    {
                        Id = "b52f4e8e-7a86-4279-82d1-749b67b99a116",
                        Email = "MariaH@ipp.it",
                        NormalizedEmail = "MARIAH@IPP.IT",
                        EmailConfirmed = true,
                        UserName = "Marii",
                        NormalizedUserName = "MARII",
                        CompanyId = "hca9a2af-1ed2-430c-b9ae-e5476d36ca79"
                    },
                    new UserExtension
                    {
                        Id = "c52f4e8e-7a86-4279-82d1-749b67b99a117",
                        Email = "Rosee@ipp.it",
                        NormalizedEmail = "ROSEE@IPP.IT",
                        EmailConfirmed = true,
                        UserName = "Rosi",
                        NormalizedUserName = "ROSI",
                        CompanyId = "hca9a2af-1ed2-430c-b9ae-e5476d36ca79"
                    },
                    new UserExtension
                    {
                        Id = "d52f4e8e-7a86-4279-82d1-749b67b99a118",
                        Email = "Irena@ipp.it",
                        NormalizedEmail = "IRENA@IPP.IT",
                        EmailConfirmed = true,
                        UserName = "Iri75",
                        NormalizedUserName = "IRI75",
                        CompanyId = "hca9a2af-1ed2-430c-b9ae-e5476d36ca79"
                    },
                    new UserExtension
                    {
                        Id = "e52f4e8e-7a86-4279-82d1-749b67b99a25",
                        Email = "TomMustermann@hmu.gr",
                        NormalizedEmail = "TOMMUSTERMANN@HMU.GR",
                        EmailConfirmed = true,
                        UserName = "Tommy",
                        NormalizedUserName = "TOMMY",
                        CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80"
                    },
                    new UserExtension
                    {
                        Id = "f52f4e8e-7a86-4279-82d1-749b67b99a20",
                        Email = "TinaTurner@hmu.gr",
                        NormalizedEmail = "TINATURNER@HMU.GR",
                        EmailConfirmed = true,
                        UserName = "Tini",
                        NormalizedUserName = "TINI",
                        CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80"
                    },
                    new UserExtension
                    {
                        Id = "g52f4e8e-7a86-4279-82d1-749b67b99a11",
                        Email = "MichelK@hmu.gr",
                        NormalizedEmail = "MICHELK@HMU.GR",
                        EmailConfirmed = true,
                        UserName = "Mimi",
                        NormalizedUserName = "MIMI",
                        CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80"
                    },
                    new UserExtension
                    {
                        Id = "h52f4e8e-7a86-4279-82d1-749b67b99a22",
                        Email = "KurtWalter@hmu.gr",
                        NormalizedEmail = "KURTWALTER@HMU.GR",
                        EmailConfirmed = true,
                        UserName = "KurtW",
                        NormalizedUserName = "KURTW",
                        CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80"
                    },
                    new UserExtension
                    {
                        Id = "i52f4e8e-7a86-4279-82d1-749b67b99a23",
                        Email = "ElisabethNewton@hmu.gr",
                        NormalizedEmail = "ELISABETHNEWTON@HMU.GR",
                        EmailConfirmed = true,
                        UserName = "Elli",
                        NormalizedUserName = "ELLI",
                        CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80"
                    },
                    new UserExtension
                    {
                        Id = "j52f4e8e-7a86-4279-82d1-749b67b99a24",
                        Email = "AlbertEinstein@hmu.gr",
                        NormalizedEmail = "ALBERTEINSTEIN@HMU.GR",
                        EmailConfirmed = true,
                        UserName = "AlbertE",
                        NormalizedUserName = "ALBERTE",
                        CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80"
                    },
                    new UserExtension
                    {
                        Id = "k52f4e8e-7a86-4279-82d1-749b67b99a25",
                        Email = "WilhelmRöntgen@pec.it",
                        NormalizedEmail = "WILHELMRÖNTGEN@PEC.IT",
                        EmailConfirmed = true,
                        UserName = "Willi",
                        NormalizedUserName = "WILLI",
                        CompanyId = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81"
                    },
                    new UserExtension
                    {
                        Id = "l52f4e8e-7a86-4279-82d1-749b67b99a26",
                        Email = "FridaMozart@pec.it",
                        NormalizedEmail = "FRIDAMOZART@PEC.IT",
                        EmailConfirmed = true,
                        UserName = "Frida23",
                        NormalizedUserName = "FRIDA23",
                        CompanyId = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81"
                    },
                    new UserExtension
                    {
                        Id = "m52f4e8e-7a86-4279-82d1-749b67b99a27",
                        Email = "AntoniaS@pec.it",
                        NormalizedEmail = "ANTONIAS@PEC.IT",
                        EmailConfirmed = true,
                        UserName = "Anto",
                        NormalizedUserName = "ANTO",
                        CompanyId = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81"
                    },
                    new UserExtension
                    {
                        Id = "n52f4e8e-7a86-4279-82d1-749b67b99a28",
                        Email = "HeinrichHeine@pec.it",
                        NormalizedEmail = "HEINRICHHEINE@PEC.IT",
                        EmailConfirmed = true,
                        UserName = "HeinrichH",
                        NormalizedUserName = "HEINRICHH",
                        CompanyId = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81"
                    },
                    new UserExtension
                    {
                        Id = "o52f4e8e-7a86-4279-82d1-749b67b99a29",
                        Email = "WolfgangGoethe@pec.it",
                        NormalizedEmail = "WOLFGANGGOETHE@PEC.IT",
                        EmailConfirmed = true,
                        UserName = "Wolli",
                        NormalizedUserName = "WOLLI",
                        CompanyId = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81"
                    },
                    new UserExtension
                    {
                        Id = "p52f4e8e-7a86-4279-82d1-749b67b99a30",
                        Email = "HectorG@pec.it",
                        NormalizedEmail = "HECTORG@PEC.IT",
                        EmailConfirmed = true,
                        UserName = "Hector12",
                        NormalizedUserName = "HECTOR12",
                        CompanyId = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81"
                    },
                    new UserExtension
                    {
                        Id = "q52f4e8e-7a86-4279-82d1-749b67b99a31",
                        Email = "KaiPflaume@univ-orleans.fr",
                        NormalizedEmail = "KAIPFLAUME@UNIV-ORLEANS.FR",
                        EmailConfirmed = true,
                        UserName = "KaiP",
                        NormalizedUserName = "KAIP",
                        CompanyId = "kca9a2af-1ed2-430c-b9ae-e5476d36ca82"
                    },
                    new UserExtension
                    {
                        Id = "r52f4e8e-7a86-4279-82d1-749b67b99a32",
                        Email = "Viki@univ-orleans.fr",
                        NormalizedEmail = "VIKI@UNIV-ORLEANS.FR",
                        EmailConfirmed = true,
                        UserName = "Vika",
                        NormalizedUserName = "VIKA",
                        CompanyId = "kca9a2af-1ed2-430c-b9ae-e5476d36ca82"
                    },
                    new UserExtension
                    {
                        Id = "s52f4e8e-7a86-4279-82d1-749b67b99a33",
                        Email = "Sero@univ-orleans.fr",
                        NormalizedEmail = "SERO@UNIV-ORLEANS.FR",
                        EmailConfirmed = true,
                        UserName = "Sero12",
                        NormalizedUserName = "SERO12",
                        CompanyId = "kca9a2af-1ed2-430c-b9ae-e5476d36ca82"
                    },
                    new UserExtension
                    {
                        Id = "t52f4e8e-7a86-4279-82d1-749b67b99a34",
                        Email = "HeikeU@univ-orleans.fr",
                        NormalizedEmail = "HEIKEU@UNIV-ORLEANS.FR",
                        EmailConfirmed = true,
                        UserName = "Heike78",
                        NormalizedUserName = "HEIKE78",
                        CompanyId = "kca9a2af-1ed2-430c-b9ae-e5476d36ca82"
                    },
                    new UserExtension
                    {
                        Id = "u52f4e8e-7a86-4279-82d1-749b67b99a35",
                        Email = "KatieG@univ-orleans.fr",
                        NormalizedEmail = "KATIEG@UNIV-ORLEANS.FR",
                        EmailConfirmed = true,
                        UserName = "Katie",
                        NormalizedUserName = "KATIE",
                        CompanyId = "kca9a2af-1ed2-430c-b9ae-e5476d36ca82"
                    },
                    new UserExtension
                    {
                        Id = "u52f4e8e-7a86-4279-82d1-749b67b99a13",
                        Email = "AlperR@vilniustech.it",
                        NormalizedEmail = "ALPERR@VILNIUSTECH.IT",
                        EmailConfirmed = true,
                        UserName = "Alper34",
                        NormalizedUserName = "ALPER34",
                        CompanyId = "lca9a2af-1ed2-430c-b9ae-e5476d36ca83"
                    },
                    new UserExtension
                    {
                        Id = "v52f4e8e-7a86-4279-82d1-749b67b99a136",
                        Email = "MertBeirut@vilniustech.it",
                        NormalizedEmail = "MERTBEIRUT@VILNIUSTECH.IT",
                        EmailConfirmed = true,
                        UserName = "Mert",
                        NormalizedUserName = "MERT",
                        CompanyId = "lca9a2af-1ed2-430c-b9ae-e5476d36ca83"
                    },
                    new UserExtension
                    {
                        Id = "w52f4e8e-7a86-4279-82d1-749b67b99a37",
                        Email = "MarcelJ@vilniustech.it",
                        NormalizedEmail = "MARCELJ@VILNIUSTECH.IT",
                        EmailConfirmed = true,
                        UserName = "Marci",
                        NormalizedUserName = "MARCI",
                        CompanyId = "lca9a2af-1ed2-430c-b9ae-e5476d36ca83"
                    },
                    new UserExtension
                    {
                        Id = "x52f4e8e-7a86-4279-82d1-749b67b99a38",
                        Email = "Angi@vilniustech.it",
                        NormalizedEmail = "ANGI@VILNIUSTECH.IT",
                        EmailConfirmed = true,
                        UserName = "Angie",
                        NormalizedUserName = "ANGIE",
                        CompanyId = "lca9a2af-1ed2-430c-b9ae-e5476d36ca83"
                    },
                    new UserExtension
                    {
                        Id = "y52f4e8e-7a86-4279-82d1-749b67b99a39",
                        Email = "PaulPeter@vilniustech.it",
                        NormalizedEmail = "PAULPETER@VILNIUSTECH.IT",
                        EmailConfirmed = true,
                        UserName = "Pauli",
                        NormalizedUserName = "PAULI",
                        CompanyId = "lca9a2af-1ed2-430c-b9ae-e5476d36ca83"
                    },
                    new UserExtension
                    {
                        Id = "z52f4e8e-7a86-4279-82d1-749b67b99a40",
                        Email = "NewtonRoman@vilniustech.it",
                        NormalizedEmail = "NEWTONROMAN@VILNIUSTECH.IT",
                        EmailConfirmed = true,
                        UserName = "Roman45",
                        NormalizedUserName = "ROMAN45",
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
                    new IdentityUserRole<string>                            // @Jasmin: Ab hier bitte die UserId oben bei User und unten bei UserRole vergleichen
                    {
                        UserId = "e52f4e8e-7a86-4279-82d1-749b67b99a25", // MobileUser Hellenic Mediterranean University
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "f52f4e8e-7a86-4279-82d1-749b67b99a20",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "g52f4e8e-7a86-4279-82d1-749b67b99a11",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "h52f4e8e-7a86-4279-82d1-749b67b99a22",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "i52f4e8e-7a86-4279-82d1-749b67b99a23",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "j52f4e8e-7a86-4279-82d1-749b67b99a24", // MobileUser Unicusano
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "k52f4e8e-7a86-4279-82d1-749b67b99a25",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "l52f4e8e-7a86-4279-82d1-749b67b99a26",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "m52f4e8e-7a86-4279-82d1-749b67b99a27", // Nur 2 mal
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "n52f4e8e-7a86-4279-82d1-749b67b99a28",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "o52f4e8e-7a86-4279-82d1-749b67b99a29",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "p52f4e8e-7a86-4279-82d1-749b67b99a30",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "q52f4e8e-7a86-4279-82d1-749b67b99a31", // Mobile User University of Orléans
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "r52f4e8e-7a86-4279-82d1-749b67b99a32",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "s52f4e8e-7a86-4279-82d1-749b67b99a33",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "t52f4e8e-7a86-4279-82d1-749b67b99a34",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "u52f4e8e-7a86-4279-82d1-749b67b99a35",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "u52f4e8e-7a86-4279-82d1-749b67b99a13", // Vilnius Tech MobileUser
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "v52f4e8e-7a86-4279-82d1-749b67b99a136",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "w52f4e8e-7a86-4279-82d1-749b67b99a37",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "x52f4e8e-7a86-4279-82d1-749b67b99a38",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "y52f4e8e-7a86-4279-82d1-749b67b99a39",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "z52f4e8e-7a86-4279-82d1-749b67b99a40",
                        RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2"
                    }
                    );
                context.SaveChanges();



                // Activity 

                context.Activity.AddRange(
                    new Activity
                    {
                        Id = "t33df349-1c29-42e3-bf45-6d7e6c990f1h",
                        ActivityType = "Swimming",
                        Description = "Go swimming",
                        MaxSpeed = 10,
                        SetManualyByUser = true
                    },
                    new Activity
                    {
                        Id = "u33df349-1c29-42e3-bf45-6d7e6c990f1i",
                        ActivityType = "Biking",
                        Description = "Ride a bike",
                        MaxSpeed = 40,
                        SetManualyByUser = false
                    },
                    new Activity
                    {
                        Id = "v33df349-1c29-42e3-bf45-6d7e6c990f1j",
                        ActivityType = "Hiking",
                        Description = "Go hiking.",
                        MaxSpeed = 10,
                        SetManualyByUser = false
                    },
                    new Activity
                    {
                        Id = "w33df349-1c29-42e3-bf45-6d7e6c990f1k",
                        ActivityType = "Jogging",
                        Description = "Go jogging",
                        MaxSpeed = 25,
                        SetManualyByUser = false
                    },
                    new Activity
                    {
                        Id = "x33df349-1c29-42e3-bf45-6d7e6c990f1l",
                        ActivityType = "Walking",
                        Description = "Go for a walk",
                        MaxSpeed = 15,
                        SetManualyByUser = false
                    });
                context.SaveChanges();


                context.UserActivity.AddRange(
                     new UserActivity
                     {
                         Id = "ä51f4e8e-5b86-4279-82d1-749b67b99a98",
                         UserId = "m52f4e8e-7a86-4279-82d1-749b67b99a101",
                         ActivityId = "t33df349-1c29-42e3-bf45-6d7e6c990f1h",
                         CompanyId = "d33df339-1c29-42e3-bb45-6d7e6c990f1e",
                         StartTime = new DateTime(2021, 07, 13, 15, 02, 05),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 13, 15, 22, 05),
                         SumTime = new TimeSpan(hours: 0, minutes: 20, seconds: 0),
                         SumDistance = 5125                                     // Meters
                     },
                     new UserActivity
                     {
                         Id = "ö51f4e8e-5b86-4279-82d1-749b67b99a99",
                         UserId = "p52f4e8e-7a86-4279-82d1-749b67b99a104", // Zwei mal eingetragen
                         ActivityId = "u33df349-1c29-42e3-bf45-6d7e6c990f1i",
                         CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78",
                         StartTime = new DateTime(2021, 07, 14, 15, 00, 05),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 14, 15, 30, 05),
                         SumTime = new TimeSpan(hours: 0, minutes: 30, seconds: 0),
                         SumDistance = 1000                                    // Meters
                     },
                     new UserActivity
                     {
                         Id = "p51f4e8e-5b86-4279-82d1-749b67b99a112",
                         UserId = "x52f4e8e-7a86-4279-82d1-749b67b99a112", // Richtig
                         ActivityId = "u33df349-1c29-42e3-bf45-6d7e6c990f1i",
                         CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76",
                         StartTime = new DateTime(2021, 07, 14, 15, 00, 05),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 14, 15, 30, 05),
                         SumTime = new TimeSpan(hours: 0, minutes: 30, seconds: 0),
                         SumDistance = 1000                                    // Meters
                     },
                     new UserActivity
                     {
                         Id = "q51f4e8e-5b86-4279-82d1-749b67b99a113",
                         UserId = "y52f4e8e-7a86-4279-82d1-749b67b99a113",
                         ActivityId = "w33df349-1c29-42e3-bf45-6d7e6c990f1k",
                         CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76",
                         StartTime = new DateTime(2021, 07, 14, 16, 00, 05),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 14, 16, 25, 05),
                         SumTime = new TimeSpan(hours: 0, minutes: 25, seconds: 0),
                         SumDistance = 4500                                    // Meters
                     },
                     new UserActivity
                     {
                         Id = "r51f4e8e-5b86-4279-82d1-749b67b99a114",
                         UserId = "z52f4e8e-7a86-4279-82d1-749b67b99a114",
                         ActivityId = "x33df349-1c29-42e3-bf45-6d7e6c990f1l",
                         CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76",
                         StartTime = new DateTime(2021, 07, 14, 16, 00, 05),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 14, 17, 00, 05),
                         SumTime = new TimeSpan(hours: 1, minutes: 0, seconds: 0),
                         SumDistance = 5025                                    // Meters
                     },
                     new UserActivity
                     {
                         Id = "s51f4e8e-5b86-4279-82d1-749b67b99a115",
                         UserId = "a52f4e8e-7a86-4279-82d1-749b67b99a115",
                         ActivityId = "x33df349-1c29-42e3-bf45-6d7e6c990f1l",
                         CompanyId = "hca9a2af-1ed2-430c-b9ae-e5476d36ca79",
                         StartTime = new DateTime(2021, 07, 14, 16, 00, 05),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 14, 17, 00, 05),
                         SumTime = new TimeSpan(hours: 1, minutes: 0, seconds: 0),
                         SumDistance = 5005                                    // Meters
                     },
                     new UserActivity
                     {
                         Id = "t51f4e8e-5b86-4279-82d1-749b67b99a116",
                         UserId = "b52f4e8e-7a86-4279-82d1-749b67b99a116",
                         ActivityId = "v33df349-1c29-42e3-bf45-6d7e6c990f1j",
                         CompanyId = "hca9a2af-1ed2-430c-b9ae-e5476d36ca79",
                         StartTime = new DateTime(2021, 07, 14, 16, 00, 05),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 14, 17, 00, 05),
                         SumTime = new TimeSpan(hours: 1, minutes: 0, seconds: 0),
                         SumDistance = 3000                                    // Meters
                     },
                     new UserActivity
                     {
                         Id = "u51f4e8e-5b86-4279-82d1-749b67b99a117",
                         UserId = "c52f4e8e-7a86-4279-82d1-749b67b99a117",
                         ActivityId = "v33df349-1c29-42e3-bf45-6d7e6c990f1j",
                         CompanyId = "hca9a2af-1ed2-430c-b9ae-e5476d36ca79",
                         StartTime = new DateTime(2021, 07, 14, 16, 00, 05),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 14, 17, 00, 05),
                         SumTime = new TimeSpan(hours: 1, minutes: 0, seconds: 0),
                         SumDistance = 3000                                    // Meters
                     },
                     new UserActivity
                     {
                         Id = "v51f4e8e-5b86-4279-82d1-749b67b99a118",
                         UserId = "d52f4e8e-7a86-4279-82d1-749b67b99a118",
                         ActivityId = "v33df349-1c29-42e3-bf45-6d7e6c990f1j",
                         CompanyId = "hca9a2af-1ed2-430c-b9ae-e5476d36ca79",
                         StartTime = new DateTime(2021, 07, 14, 15, 00, 05),
                         StopTime = new DateTime(2021, 07, 14, 17, 00, 05),
                         SumTime = new TimeSpan(hours: 2, minutes: 0, seconds: 0),
                         SumDistance = 6000
                     },
                     new UserActivity
                     {
                         Id = "w51f4e8e-5b86-4279-82d1-749b67b99a119",
                         UserId = "e52f4e8e-7a86-4279-82d1-749b67b99a25",
                         ActivityId = "t33df349-1c29-42e3-bf45-6d7e6c990f1h",
                         CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80",
                         StartTime = new DateTime(2021, 07, 14, 15, 00, 05),
                         StopTime = new DateTime(2021, 07, 14, 16, 00, 05),
                         SumTime = new TimeSpan(hours: 1, minutes: 0, seconds: 0),
                         SumDistance = 3500
                     },
                     new UserActivity
                     {
                         Id = "x51f4e8e-5b86-4279-82d1-749b67b99a120",
                         UserId = "f52f4e8e-7a86-4279-82d1-749b67b99a20",
                         ActivityId = "t33df349-1c29-42e3-bf45-6d7e6c990f1h",
                         CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80",
                         StartTime = new DateTime(2021, 07, 14, 15, 00, 05),
                         StopTime = new DateTime(2021, 07, 14, 15, 30, 05),
                         SumTime = new TimeSpan(hours: 0, minutes: 30, seconds: 0),
                         SumDistance = 1500
                     },
                     new UserActivity
                     {
                         Id = "y51f4e8e-5b86-4279-82d1-749b67b99a121",
                         UserId = "g52f4e8e-7a86-4279-82d1-749b67b99a11",
                         ActivityId = "u33df349-1c29-42e3-bf45-6d7e6c990f1i",
                         CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80",
                         StartTime = new DateTime(2021, 07, 14, 15, 00, 05),
                         StopTime = new DateTime(2021, 07, 14, 15, 30, 05),
                         SumTime = new TimeSpan(hours: 0, minutes: 30, seconds: 0),
                         SumDistance = 1000
                     },
                     new UserActivity
                     {
                         Id = "z51f4e8e-5b86-4279-82d1-749b67b99a122",
                         UserId = "h52f4e8e-7a86-4279-82d1-749b67b99a22",
                         ActivityId = "u33df349-1c29-42e3-bf45-6d7e6c990f1i",
                         CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80",
                         StartTime = new DateTime(2021, 07, 14, 15, 00, 05),
                         StopTime = new DateTime(2021, 07, 14, 15, 40, 05),
                         SumTime = new TimeSpan(hours: 0, minutes: 40, seconds: 0),
                         SumDistance = 1010
                     },
                     new UserActivity
                     {
                         Id = "a51f4e8e-5b86-4279-82d1-749b67b99a123",
                         UserId = "i52f4e8e-7a86-4279-82d1-749b67b99a23",
                         ActivityId = "w33df349-1c29-42e3-bf45-6d7e6c990f1k",
                         CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80",
                         StartTime = new DateTime(2021, 07, 14, 15, 00, 05),
                         StopTime = new DateTime(2021, 07, 14, 17, 00, 05),
                         SumTime = new TimeSpan(hours: 2, minutes: 0, seconds: 0),
                         SumDistance = 20000
                     },
                     new UserActivity
                     {
                         Id = "b51f4e8e-5b86-4279-82d1-749b67b99a124",
                         UserId = "j52f4e8e-7a86-4279-82d1-749b67b99a24",
                         ActivityId = "w33df349-1c29-42e3-bf45-6d7e6c990f1k",
                         CompanyId = "ica9a2af-1ed2-430c-b9ae-e5476d36ca80",
                         StartTime = new DateTime(2021, 07, 14, 15, 00, 05),
                         StopTime = new DateTime(2021, 07, 14, 16, 00, 05),
                         SumTime = new TimeSpan(hours: 1, minutes: 0, seconds: 0),
                         SumDistance = 10000
                     },
                     new UserActivity
                     {
                         Id = "c51f4e8e-5b86-4279-82d1-749b67b99a125",
                         UserId = "k52f4e8e-7a86-4279-82d1-749b67b99a25",
                         ActivityId = "x33df349-1c29-42e3-bf45-6d7e6c990f1l",
                         CompanyId = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81",
                         StartTime = new DateTime(2021, 07, 14, 15, 00, 05),
                         StopTime = new DateTime(2021, 07, 14, 17, 00, 05),
                         SumTime = new TimeSpan(hours: 2, minutes: 0, seconds: 0),
                         SumDistance = 10000
                     },
                     new UserActivity
                     {
                         Id = "d51f4e8e-5b86-4279-82d1-749b67b99a126",
                         UserId = "l52f4e8e-7a86-4279-82d1-749b67b99a26",
                         ActivityId = "x33df349-1c29-42e3-bf45-6d7e6c990f1l",
                         CompanyId = "jca9a2af-1ed2-430c-b9ae-e5476d36ca81",
                         StartTime = new DateTime(2021, 07, 14, 15, 00, 05),
                         StopTime = new DateTime(2021, 07, 14, 15, 40, 05),
                         SumTime = new TimeSpan(hours: 0, minutes: 40, seconds: 0),
                         SumDistance = 5000
                     },
                     new UserActivity
                     {
                         Id = "b51f4e8e-5b86-4279-82d1-749b67b99a98",
                         UserId = "d52f4e8e-7a86-4279-82d1-749b67b99a92", // Zwei mal eingetragen
                         ActivityId = "t33df349-1c29-42e3-bf45-6d7e6c990f1h",
                         CompanyId = "d33df339-1c29-42e3-bb45-6d7e6c990f1e",
                         StartTime = new DateTime(2021, 07, 14, 15, 15, 05),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 14, 15, 30, 05),
                         SumTime = new TimeSpan(hours: 0, minutes: 15, seconds: 0),
                         SumDistance = 512                                     // Meters
                     },
                     new UserActivity
                     {
                         Id = "c51f4e8e-5b86-4279-82d1-749b67b99a99",
                         UserId = "l52f4e8e-7a86-4279-82d1-749b67b99a100",
                         ActivityId = "u33df349-1c29-42e3-bf45-6d7e6c990f1i",
                         CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78",
                         StartTime = new DateTime(2021, 07, 13, 16, 10, 15),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 13, 17, 22, 25),
                         SumTime = new TimeSpan(hours: 1, minutes: 12, seconds: 10),
                         SumDistance = 15215                                     // Meters
                     },
                     new UserActivity
                     {
                         Id = "d51f4e8e-5b86-4279-82d1-749b67b99a100",
                         UserId = "m52f4e8e-7a86-4279-82d1-749b67b99a101",
                         ActivityId = "v33df349-1c29-42e3-bf45-6d7e6c990f1j",
                         CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78",
                         StartTime = new DateTime(2021, 07, 15, 12, 32, 38),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 15, 14, 37, 39),
                         SumTime = new TimeSpan(hours: 2, minutes: 5, seconds: 1),
                         SumDistance = 920                                     // Meters
                     },
                     new UserActivity
                     {
                         Id = "e51f4e8e-5b86-4279-82d1-749b67b99a101",
                         UserId = "n52f4e8e-7a86-4279-82d1-749b67b99a102",
                         ActivityId = "w33df349-1c29-42e3-bf45-6d7e6c990f1k",
                         CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78",
                         StartTime = new DateTime(2021, 07, 15, 20, 07, 21),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 15, 20, 37, 21),
                         SumTime = new TimeSpan(hours: 0, minutes: 30, seconds: 0),
                         SumDistance = 5232                                     // Meters
                     },
                     new UserActivity
                     {
                         Id = "f51f4e8e-5b86-4279-82d1-749b67b99a102",
                         UserId = "o52f4e8e-7a86-4279-82d1-749b67b99a103",
                         ActivityId = "x33df349-1c29-42e3-bf45-6d7e6c990f1l",
                         CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78",
                         StartTime = new DateTime(2021, 07, 15, 14, 10, 05),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 15, 16, 22, 05),
                         SumTime = new TimeSpan(hours: 2, minutes: 12, seconds: 0),
                         SumDistance = 9321                                     // Meters
                     },
                     new UserActivity
                     {
                         Id = "g51f4e8e-5b86-4279-82d1-749b67b99a103",
                         UserId = "p52f4e8e-7a86-4279-82d1-749b67b99a104",
                         ActivityId = "t33df349-1c29-42e3-bf45-6d7e6c990f1h",
                         CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78",
                         StartTime = new DateTime(2021, 07, 15, 15, 02, 05),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 15, 15, 22, 05),
                         SumTime = new TimeSpan(hours: 0, minutes: 20, seconds: 0),
                         SumDistance = 5125                                     // Meters
                     },
                     new UserActivity
                     {
                         Id = "h51f4e8e-5b86-4279-82d1-749b67b99a104",
                         UserId = "q52f4e8e-7a86-4279-82d1-749b67b99a105",
                         ActivityId = "w33df349-1c29-42e3-bf45-6d7e6c990f1k",
                         CompanyId = "gca9a2af-1ed2-430c-b9ae-e5476d36ca78",
                         StartTime = new DateTime(2021, 07, 15, 11, 02, 05),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 15, 12, 22, 05),
                         SumTime = new TimeSpan(hours: 1, minutes: 20, seconds: 0),
                         SumDistance = 1300                                     // Meters
                     },
                     new UserActivity
                     {
                         Id = "x33df349-1c29-42e3-bf45-6d7e6c990f1l",
                         UserId = "r52f4e8e-7a86-4279-82d1-749b67b99a106",
                         ActivityId = "t33df349-1c29-42e3-bf45-6d7e6c990f1h",
                         CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76",
                         StartTime = new DateTime(2021, 07, 15, 19, 00, 00),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 15, 20, 00, 00),
                         SumTime = new TimeSpan(hours: 1, minutes: 0, seconds: 0),
                         SumDistance = 5005                                     // Meters
                     },
                     new UserActivity
                     {
                         Id = "j51f4e8e-5b86-4279-82d1-749b67b99a106",
                         UserId = "s52f4e8e-7a86-4279-82d1-749b67b99a107",
                         ActivityId = "t33df349-1c29-42e3-bf45-6d7e6c990f1h",
                         CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76",
                         StartTime = new DateTime(2021, 07, 15, 13, 12, 00),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 15, 13, 32, 00),
                         SumTime = new TimeSpan(hours: 0, minutes: 20, seconds: 0),
                         SumDistance = 510                                     // Meters
                     },
                     new UserActivity
                     {
                         Id = "k51f4e8e-5b86-4279-82d1-749b67b99a107",
                         UserId = "t52f4e8e-7a86-4279-82d1-749b67b99a108",
                         ActivityId = "u33df349-1c29-42e3-bf45-6d7e6c990f1i",
                         CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76",
                         StartTime = new DateTime(2021, 07, 15, 15, 47, 00),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 16, 17, 47, 00),
                         SumTime = new TimeSpan(hours: 2, minutes: 0, seconds: 0),
                         SumDistance = 30726                                     // Meters
                     },
                     new UserActivity
                     {
                         Id = "l51f4e8e-5b86-4279-82d1-749b67b99a108",
                         UserId = "u52f4e8e-7a86-4279-82d1-749b67b99a109",
                         ActivityId = "w33df349-1c29-42e3-bf45-6d7e6c990f1k",
                         CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76",
                         StartTime = new DateTime(2021, 07, 15, 14, 02, 01),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 15, 14, 22, 01),
                         SumTime = new TimeSpan(hours: 0, minutes: 20, seconds: 0),
                         SumDistance = 3825                                     // Meters
                     },
                     new UserActivity
                     {
                         Id = "m51f4e8e-5b86-4279-82d1-749b67b99a109",
                         UserId = "v52f4e8e-7a86-4279-82d1-749b67b99a110",
                         ActivityId = "x33df349-1c29-42e3-bf45-6d7e6c990f1l",
                         CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76",
                         StartTime = new DateTime(2021, 07, 15, 15, 05, 35),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 15, 16, 05, 35),
                         SumTime = new TimeSpan(hours: 1, minutes: 0, seconds: 0),
                         SumDistance = 5025                                     // Meters
                     },
                     new UserActivity
                     {
                         Id = "n51f4e8e-5b86-4279-82d1-749b67b99a110",
                         UserId = "w52f4e8e-7a86-4279-82d1-749b67b99a111",
                         ActivityId = "u33df349-1c29-42e3-bf45-6d7e6c990f1i",
                         CompanyId = "eca9a2af-1ed2-430c-b9ae-e5476d36ca76",
                         StartTime = new DateTime(2021, 07, 15, 12, 14, 32),    //year, month, day, hour, min, seconds
                         StopTime = new DateTime(2021, 07, 15, 15, 32, 32),
                         SumTime = new TimeSpan(hours: 3, minutes: 18, seconds: 0),                   // StopTime - StartTime // hours, minutes, seconds, milliseconds.
                         SumDistance = 45269                                     // Meters
                     }

                    );
                context.SaveChanges();


                // Claims

                context.RoleClaims.AddRange(
                // For Admin ClaimValue = true
                new IdentityRoleClaim<string>
                {
                    //                    Id = 1,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Create User",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 2,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Edit User",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 3,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Delete User",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                   Id = 4,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Create Company",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 5,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Edit Company",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 6,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Delete Company",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 7,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Create Template",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 8,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Edit Template",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 9,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Delete Template",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 10,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Create Activity",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 11,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Edit Activity",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 12,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Delete Activity",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 13,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Create UserActivity",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 14,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Edit UserActivity",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 15,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Delete UserActivity",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 16,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Create Role",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 17,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Edit Role",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 18,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Delete Role",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 19,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Create Claim",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 20,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Edit Claim",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                    Id = 21,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Delete Claim",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 22,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Create User",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 23,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Edit User",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 24,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Delete User",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 25,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Create Company",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 26,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Edit Company",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 27,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Delete Company",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 28,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Create Template",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 29,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Edit Template",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 30,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Delete Template",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 31,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Create Activity",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 32,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Edit Activity",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 33,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Delete Activity",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 34,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Create UserActivity",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 35,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Edit UserActivity",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 36,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Delete UserActivity",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 37,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Create Role",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 38,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Edit Role",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 39,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Delete Role",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 40,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Create Claim",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 41,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Edit Claim",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 42,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Delete Claim",
                    ClaimValue = "false"
                },
                // For MobileUser ClaimValue = false
                // For Supervisor ClaimValue vorerst freilassen
                new IdentityRoleClaim<string>
                {
                    //                         Id = 43,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Create User",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 44,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Edit User",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 45,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Delete User",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 46,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Create Company",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 47,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Edit Company",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 48,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Delete Company",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 49,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Create Template",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                        Id = 50,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Edit Template",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 51,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Delete Template",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                        Id = 52,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Create Activity",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 53,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Edit Activity",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 54,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Delete Activity",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 55,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Create UserActivity",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 56,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Edit UserActivity",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 57,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Delete UserActivity",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 58,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Create Role",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 59,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Edit Role",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                        Id = 60,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Delete Role",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 61,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Create Claim",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 62,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Edit Claim",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 63,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Delete Claim",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 64,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Send Template",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 65,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Send Template",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 66,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Send Template",
                    ClaimValue = "true"
                },

                // Badge for Admin
                new IdentityRoleClaim<string>
                {
                    //                         Id = 67,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Create Badge",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 68,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Edit Badge",
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 69,
                    RoleId = "9d5107d5-5edd-49ae-adee-240acf8c9af1",
                    ClaimType = "Delete Badge",
                    ClaimValue = "true"
                },

                // Badge for SuperVisor
                new IdentityRoleClaim<string>
                {
                    //                         Id = 67,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Create Badge",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 68,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Edit Badge",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 69,
                    RoleId = "8d5107d5-5edd-49ae-adee-240acf8c9af0",
                    ClaimType = "Delete Badge",
                    ClaimValue = "false"
                },
                // Badge for MobileUser
                new IdentityRoleClaim<string>
                {
                    //                         Id = 67,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Create Badge",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 68,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Edit Badge",
                    ClaimValue = "false"
                },
                new IdentityRoleClaim<string>
                {
                    //                         Id = 69,
                    RoleId = "7d5107d5-5edd-49ae-adee-240acf8c9af2",
                    ClaimType = "Delete Badge",
                    ClaimValue = "false"
                });
                context.SaveChanges();
            }
        }
    }
}