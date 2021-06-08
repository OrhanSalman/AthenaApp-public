
using AthenaWebApp.Areas.Identity.IdentityModels;
using AthenaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthenaWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace AthenaWebApp.Repositories.PatternInterfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AthenaIdentityUser>> GetUsers();
        Task<AthenaIdentityUser> GetUserLogin(string Email, string PasswordHash);
        Task<AthenaIdentityUser> GetUserByEmail(string Email);
        Task<AthenaIdentityUser> PostRegisterUser(AthenaIdentityUser user);
//        Task<AthenaIdentityUser> UpdateUser(AthenaIdentityUser employee);
        void DeleteUser(string Id);
    }
}
