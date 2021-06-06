using AthenaApp.Models;
using AthenaApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace AthenaApp.Services
{
    public class LoginService
    {
        private const string MainWebServiceUrl = "https://10.0.2.2:5001/";
        private const string LoginWebServiceUrl = "https://10.0.2.2:5001/api/Users/";

        public async Task<bool> CheckLoginIfExists(string Email, string PasswordHash)
        {

            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };

            HttpClient client = client = new HttpClient(handler);

            Uri uri = new Uri(string.Format(LoginWebServiceUrl + Email + "/" + PasswordHash));
            
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
