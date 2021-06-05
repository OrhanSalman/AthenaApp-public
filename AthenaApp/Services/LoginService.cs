using AthenaApp.Models;
using AthenaApp.Models.Login;
using AthenaApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AthenaApp.Services
{
    public class LoginService
    {
        RestClient<User> _restClient = new RestClient<User>();

        public async Task<bool> CheckLoginIfExists(string Email, string PasswordHash)
        {
            var check = await _restClient.checkLogin(Email, PasswordHash);

            return check;
        }
    }
}
