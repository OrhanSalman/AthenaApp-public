using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AthenaWebApp.Models;

namespace AthenaWebApp.Data
{
    public class AthenaWebAppContext : DbContext
    {
        public AthenaWebAppContext (DbContextOptions<AthenaWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<AthenaWebApp.Models.UserView> UserView { get; set; }
    }
}
