
using AthenaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Repositories.PatternInterfaces
{
    public interface ICompanyRepository
    {

        Task<IEnumerable<Company>> GetCompanies();
        Task<Company> GetCompanyById(int Id);
        Task<Company> GetCompanyByName(string CompanyName);
    }
}