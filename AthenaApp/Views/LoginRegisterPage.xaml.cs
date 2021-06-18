using AthenaApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthenaApp.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AthenaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginRegisterPage : TabbedPage
    {
        private const string Api = "https://10.0.2.2:5001/api/Users/AppLoginRequest";
        public LoginRegisterPage()
        {
            InitializeComponent();
        }


        private async void LoginButton(object sender, EventArgs e)
        {
//            await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");   // Testversion, später entfernen

            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            HttpClient client = client = new HttpClient(handler);

            Uri uri = new Uri(string.Format(Api + "?Email=" + UserInputMail.Text));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (UserInputMail.Text == "")
            {
                await DisplayAlert("Login failed", "Enter your Email before login", "Okay");
            }
            else if (response.IsSuccessStatusCode)
            {
                // ToDo: Check if user is blocked
                await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                await DisplayAlert("Login failed", "The E-Mail you entered was not found or not confirmed.", "Okay", "Resend ConfirmationMail");
            }
            else
            {
                await DisplayAlert("Error", "An error has occured. Please try again later.", "Okay");
            }
        }

        private async void RegisterButton (object sender, EventArgs e)
        {
//            RegisterService registerService = new RegisterService();
//            var postRegisterUser = await registerService.AppRegistration(UserInputName.Text, UserInputNewMail.Text);

            try
            {
                var rawData = new User
                {
                    UserName = UserInputName.Text,
                    Email = UserInputNewMail.Text,

                };
                var serializedRawData = JsonConvert.SerializeObject(rawData);
                var requestContent = new StringContent(serializedRawData, Encoding.UTF8, "application/json");

                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                    {
                        if (cert.Issuer.Equals("CN=localhost"))
                            return true;
                        return errors == System.Net.Security.SslPolicyErrors.None;
                    }
                };
                HttpClient client = new HttpClient(handler);
                Uri uri = new Uri(string.Format("https://10.0.2.2:5001/api/Users/AppRegisterRequest/"));


                HttpResponseMessage response = await client.PostAsync(uri, requestContent);


                if (UserInputMail.Text == "" || UserInputName.Text == "")
                {
                    await DisplayAlert("Credentials are missing", "Enter your Email and a Username before registration.", "Got it!");
                }
                else if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    //                    var validatedUser = JsonConvert.DeserializeObject(apiResponse);

                    await DisplayAlert("Check your Mail's", "Welcome! Just confirm your E-Mail to run for the Athenaton", "Great!");

                    // Lade-PopUp, dass auf Bestätigung wartet, um dann auf nächste Page weiterzuverlinken
                    await Shell.Current.GoToAsync($"//{nameof(LoginRegisterPage)}");
                    // ToDo: Save the user credentials and automatic login
//                    return apiResponse;
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    await DisplayAlert("Registration failed", "Username or Email is already forgiven.", "What a pity");
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    await DisplayAlert("Registration failed", "Registration failed. Please register with an accepted Company E-Mail", "Gotit");
                }
                else if(response.StatusCode == HttpStatusCode.GatewayTimeout)
                {
                    await DisplayAlert("Please try again later", "The Server did not respond. Maybe there are some maintainces. Please try again later", "I will do");
                }

                // Aufpassen, denn je nach dem welcher Controller, kann StatusCode 301 sein (wegen E-Mail Confirmation)
                //                response.EnsureSuccessStatusCode
                //            var createdCompany = JsonSerializer.Deserialize<CompanyDto>(content, _options);
                //            var responseOfNewUser = JsonSerializer.Deserialize(content);



            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}


