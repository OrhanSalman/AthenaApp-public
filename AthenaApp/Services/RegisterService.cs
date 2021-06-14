using AthenaApp.Models;
using AthenaApp.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;



namespace AthenaApp.Services
{
    public class RegisterService
    {
        private const string MainWebServiceUrl = "https://10.0.2.2:5001/";
        private const string RegisterWebServiceUrl = "https://10.0.2.2:5001/api/Users/";

        /*
                public async Task RegisterUser (User user, bool isNewUser = false)
                {
                    // statt RegisterWebServiceUrl constants klasse erstellen
                    Uri uri = new Uri(string.Format(RegisterWebServiceUrl, string.Empty));

                    string json = JsonSerializer.Serialize<User>(user, SerializerOptions);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");


                }
        */






        public async Task<bool> CheckRegisterIfExists(string userName, string email)
        {
            try
            {
                

                User user = new User
                {
                    UserName = userName,
                    
                    Email = email,
                   
                };

/*
                string url = "http://192.168.0.5/api/Masters/SaveEmployee";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                
                if(response.StatusCode)
                {
                    return response.IsSuccessStatusCode;
                }
                el

                return response.EnsureSuccessStatusCode
*/
                
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    if (cert.Issuer.Equals("CN=localhost"))
                        return true;
                    return errors == System.Net.Security.SslPolicyErrors.None;
                };

                HttpClient client = new HttpClient(handler);

                Uri uri = new Uri(string.Format("https://10.0.2.2:5001/api/Users"));
                HttpResponseMessage response = new HttpResponseMessage();

                try
                {
                    var newUser = JsonSerializer.Serialize(user);
                    var requestContent = new StringContent(newUser, Encoding.UTF8, "application/json");
                    response = await client.PostAsync(uri, requestContent);

                    // Aufpassen, denn je nach dem welcher Controller, kann StatusCode 301 sein (wegen E-Mail Confirmation)
                    //                response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    //            var createdCompany = JsonSerializer.Deserialize<CompanyDto>(content, _options);
                    //            var responseOfNewUser = JsonSerializer.Deserialize(content);
                    Console.WriteLine(content);
                }
                catch (Exception)
                {

                    throw;
                }


                return response.IsSuccessStatusCode;
                
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
