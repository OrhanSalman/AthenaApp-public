
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
        private readonly AthenaDbContext _context;

        public CompanyRepository(AthenaDbContext _context)
        {
            this._context = _context;
        }

        public Task<IEnumerable<Company>> GetCompanies()
        {
            throw new NotImplementedException();
        }

        public Task<Company> GetCompanyById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Company> GetCompanyByName(string CompanyName)
        {
            throw new NotImplementedException();
        }


        /*
                public async Task<IEnumerable<Company>> Search(string CompanyName)
                {
                    IQueryable<Company> query = _context.Company;

                    if (!string.IsNullOrEmpty(CompanyName))
                    {
                        query = query.Where(e => e.CompanyName.Contains(CompanyName));
                    }

                    return await query.ToListAsync();
                }
        */
    }
}