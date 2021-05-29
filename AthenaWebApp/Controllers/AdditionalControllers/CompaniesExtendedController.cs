/*
using AthenaWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthenaWebApp.Repositories.PatternInterfaces;
using AthenaWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace AthenaWebApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesExtendedController : ControllerBase
    {
        private readonly ICompanyRepository companyRepository;

        public CompaniesExtendedController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Company>>> Search(string CompanyName)
        {
            try
            {
                var result = await companyRepository.Search(CompanyName);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
*/