using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AthenaApp.Services
{
    public class RestClient<T>
    {
        private const string MainWebServiceUrl = "https://192.168.178.196:44361/";
        private const string LoginWebServiceUrl = "https://192.168.178.196:44361/UserViews/";

        public async Task<bool> checkLogin(string Email, string PasswordHash)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(LoginWebServiceUrl + "Email=" + Email + "/" + "PasswordHash=" + PasswordHash);

            return response.IsSuccessStatusCode;
        }
    }
}
