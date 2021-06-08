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
    public class RegisterService
    {
        private const string MainWebServiceUrl = "https://10.0.2.2:5001/";
        private const string RegisterWebServiceUrl = "https://10.0.2.2:5001/api/Users/";

        public async Task<bool> CheckRegisterIfExists(string UserName, string UniversityId, string Email, string PasswordHash)
        {
            int intUniversityId = Int32.Parse(UniversityId);

            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };

            HttpClient client = client = new HttpClient(handler);

            Uri uri = new Uri(string.Format(RegisterWebServiceUrl + "/" + UserName + "/" + intUniversityId + "/" + Email + "/" + PasswordHash));
            
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
