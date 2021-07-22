using AthenaApp.Models;
using AthenaApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;
using AthenaApp.Models;
using AthenaWebApp.Models;

namespace AthenaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        Array Email; 
        public async void BadgeButton_Clicked(object sender, EventArgs e)
        {
            
            string Api = "https://10.0.2.2:5001/api/UserActivities/GetUserActivitiesOfUserId";

            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            HttpClient client = client = new HttpClient(handler);

            Uri uri = new Uri(string.Format(Api));
            HttpResponseMessage response = await client.GetAsync(uri);


            var jsonString = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<UserActivity[]>(jsonString);
            List<UserActivity> listOfAllUserActivities = new List<UserActivity>();

            foreach (var json in data)
            {
                listOfAllUserActivities.Add(
                    new UserActivity
                    {
                        Id = json.Id,
                        UserId = json.UserId,
                        ActivityId = json.ActivityId,
                        CompanyId = json.CompanyId,
                        StartTime = json.StartTime,
                        StopTime = json.StopTime,
                        SumTime = json.SumTime,
                        SumDistance = json.SumDistance


                    });
            }



            string ApiCom = "https://10.0.2.2:5001/api/Companies/GetCompanies";

            HttpClientHandler handlerCom = new HttpClientHandler();
            handlerCom.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            HttpClient clientCom = clientCom = new HttpClient(handlerCom);

            Uri uriCom = new Uri(string.Format(ApiCom));
            HttpResponseMessage responseCom = await client.GetAsync(uriCom);


            var jsonStringCom = await responseCom.Content.ReadAsStringAsync();

            var dataCom = JsonConvert.DeserializeObject<Company[]>(jsonStringCom);
            List<Company> listOfCompanies = new List<Company>();

            foreach (var company in dataCom)
            {
                listOfCompanies.Add(
                    new Company
                    {
                        Id = company.Id,
                        CompanyName = company.CompanyName,
                        Country =company.Country,
                        EmailContext = company.EmailContext
                    });
            }








            XamarinManager manager = new XamarinManager();
            string jsonData = manager.Get_post_data();
            var jsonUser = JsonConvert.DeserializeObject<User>(jsonData);

            User user = new User()
            {
                Id = jsonUser.Id,
                UserName = jsonUser.UserName,
                CompanyId = jsonUser.CompanyId,
                SecurityStamp = jsonUser.SecurityStamp,
                ProfilePicture = jsonUser.ProfilePicture,
                Email = jsonUser.Email,
                CompanyName = jsonUser.CompanyName
               
            };









            var UserCompany = listOfCompanies.Where(c => c.Id == user.CompanyId).Select(c => c.CompanyName.ToString()).FirstOrDefault();
            ProfileUserCompanyInfo.Text = UserCompany;  

            var ProfileUserName = user.UserName;
            ProfileUserNameInfo.Text = ProfileUserName;
            
            var ProfileUserEmail = user.Email;
            ProfileUserEmailInfo.Text = ProfileUserEmail;
            var ProfileUserPicture = user.ProfilePicture;


            
            string ApiAct = "https://10.0.2.2:5001/api/Activities/GetActivities";


            HttpClientHandler handlerAct = new HttpClientHandler();
            handlerAct.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            HttpClient clientAct = clientAct = new HttpClient(handlerAct);

            Uri uriAct = new Uri(string.Format(ApiAct));
            HttpResponseMessage responseAct = await client.GetAsync(uriAct);

        var jsonStringAct = await responseAct.Content.ReadAsStringAsync();

            var dataAct = JsonConvert.DeserializeObject<Activity[]>(jsonStringAct);

            var listOfAllActivities = new List<Activity>();

            foreach (var json in dataAct)
            {
                listOfAllActivities.Add(json);

                 new Activity
                 {
                     Id = json.Id,
                     ActivityType = json.ActivityType,
                     MaxSpeed = json.MaxSpeed,
                     Description = json.Description,
                     SetManualyByUser = json.SetManualyByUser
                 };
            }

            var actTypes = listOfAllActivities.Select(c => c.ActivityType).ToArray();

            // Change
            var action = await DisplayActionSheet("For which Activity would you like to see your Badges?", "Cancel", null, actTypes);

            bool ActivityStatus = false;
            var idOfActType = listOfAllActivities.Where(d => d.ActivityType == action ).Select(d => d.Id.ToString()).FirstOrDefault();
            var idOfUserActivity = listOfAllUserActivities.Where(c => c.ActivityId == idOfActType).Select(c => c.SumDistance);
            var EachActTotalDistance = idOfUserActivity.Sum();

            
            var ActivityString = action.ToString();
            if (action == "Cancel")
            {
               
            }
            else
            {
                ActivityStatus = true;
                idOfActType = listOfAllActivities.Where(c => c.ActivityType.ToString() == ActivityString).Select(d => d.Id.ToString()).FirstOrDefault();

                Debug.WriteLine(idOfActType);
            }



            

            double DistanceIntervallStart = 0;          //Echte Intervalle abfragen API
            double DistanceIntervallEnd = 100;
            if (EachActTotalDistance >= DistanceIntervallEnd)
            {
                // give Badge, show pic etc.
            }


        }


    }
}