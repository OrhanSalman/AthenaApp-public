using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AthenaApp.Services
{
    public class RestClient<T>
    {
        /*
        private const string MainWebServiceUrl = "https://10.0.2.2:44361/";
        private const string LoginWebServiceUrl = "https://10.0.2.2:44361/api/Users/";
        */

        public static string MainWebServiceUrl =
        DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:44361" : "https://localhost:44361";
        public static string LoginWebServiceUrl = $"{MainWebServiceUrl}/api/Users/";


        public async Task<bool> checkLogin(string Email, string PasswordHash)
        {


            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(handler);

            var response = await client.GetAsync(LoginWebServiceUrl + Email + "/" +  PasswordHash);

            return response.IsSuccessStatusCode;


        }
    }
}
