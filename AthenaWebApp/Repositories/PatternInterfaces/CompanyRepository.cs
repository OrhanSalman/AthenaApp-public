/*
using AthenaWebApp.Data;
using AthenaWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Repositories.PatternInterfaces
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AthenaIdentityUserDbContext _context;

        public CompanyRepository(AthenaIdentityUserDbContext _context)
        {
            this._context = _context;
        }



        public async Task<IEnumerable<Company>> Search(string CompanyName)
        {
            IQueryable<Company> query = _context.Company;

            if (!string.IsNullOrEmpty(CompanyName))
            {
                query = query.Where(e => e.CompanyName.Contains(CompanyName));
            }

            return await query.ToListAsync();
        }
    }
}
*/