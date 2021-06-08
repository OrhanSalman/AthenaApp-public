using AthenaWebApp.Areas.Identity.IdentityModels;
using AthenaWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Repositories.PatternInterfaces
{
    public class UserRepository : IUserRepository
    {
        private readonly AthenaDbContext _context;

        public UserRepository(AthenaDbContext _context)
        {
            this._context = _context;
        }

        public async Task<IEnumerable<AthenaIdentityUser>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AthenaIdentityUser> GetUserLogin(string Email, string PasswordHash)
        {
            return await _context.Users
                .FirstOrDefaultAsync(e => e.Email == Email && e.PasswordHash == PasswordHash);
        }

        public async Task<AthenaIdentityUser> GetUserByEmail(string Email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(e => e.Email == Email);
        }

        public async Task<AthenaIdentityUser> PostNewUser(AthenaIdentityUser user)
        {
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        /*      Für Profiländerungen wichtig
                public async Task<AthenaIdentityUser> UpdateEmployee(AthenaIdentityUser user)
                {
                    var result = await _context.Users
                        .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

                    if (result != null)
                    {
                        result.FirstName = user.Email;
                        result.LastName = user.NormalizedEmail;
                        result.Email = user.UserName;
                        result.DateOfBrith = user.User;
                        result.Gender = user.Gender;
                        result.DepartmentId = user.DepartmentId;
                        result.PhotoPath = user.PhotoPath;

                        await _context.SaveChangesAsync();

                        return result;
                    }

                    return null;
                }
        */
        public async void DeleteUser(string Id)
        {
            var result = await _context.Users
                .FirstOrDefaultAsync(e => e.Id == Id);
            if (result != null)
            {
                _context.Users.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
    }
}