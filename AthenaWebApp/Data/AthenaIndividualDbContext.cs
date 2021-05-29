/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AthenaWebApp.Models;

namespace AthenaWebApp.Data
{
    public class AthenaIndividualDbContext : DbContext
    {
        public AthenaIndividualDbContext (DbContextOptions<AthenaIndividualDbContext> options)
            : base(options)
        {
        }

        public DbSet<AthenaWebApp.Models.Company> Company { get; set; }
    }
}
*/