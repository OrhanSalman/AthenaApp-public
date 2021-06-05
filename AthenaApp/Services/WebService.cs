/*
using AthenaApp.Models.Login;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using AthenaApp.ViewModels;
using AthenaApp.Views;

namespace AthenaApp.Services
{
    class WebService
    {
        private static WebService _ServiceClientInstance;
        public static WebService ServiceClientInstance
        {
            get
            {
                if (_ServiceClientInstance == null)
                    _ServiceClientInstance = new WebService();
                return _ServiceClientInstance;
            }
        }

        private JsonSerializer _serializer = new JsonSerializer();
        private HttpClient client;


        public WebService()
        {
            client = new HttpClient();
            //Change your base address here
            client.BaseAddress = new Uri("https://localhost:44361/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<LoginApiRequestModel> LoginProcess(string email, string password)
        {
            LoginApiRequestModel loginRequestModel = new LoginApiRequestModel()
            {
                Email = email,
                PasswordHash = password

            };
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(loginRequestModel), Encoding.UTF8, "application/json");
                //Change your base address tail part here and post it. 




                var response = await client.PostAsync("Users", content);
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<List<TodoItem>>(content, serializerOptions);
                }

                response.EnsureSuccessStatusCode();
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                {
                    return HttpResponseMessage();
                }
                
/*
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<LoginApiResponseModel>(json);
                    Preferences.Set("authToken", jsoncontent.authenticationToken);
                    return jsoncontent;
                }
*/
/*
            }
            catch (Exception ex)
            {
                return null;
            }
        }
*/
/*
        public async Task<LoginApiResponseModel> AuthenticateUserAsync(string email, string password)
        {
            try
            {
                LoginApiRequestModel loginRequestModel = new LoginApiRequestModel()
                {
                    Email = email,
                    PasswordHash = password

                };
                var content = new StringContent(JsonConvert.SerializeObject(loginRequestModel), Encoding.UTF8, "application/json");
                //Change your base address tail part here and post it. 
                var response = await client.PostAsync("Users", content);
                response.EnsureSuccessStatusCode();
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<LoginApiResponseModel>(json);
                    Preferences.Set("authToken", jsoncontent.authenticationToken);
                    return jsoncontent;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
*/
/*
    }
}
*/