using Plugin.Geolocator;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading;
using Newtonsoft.Json;
using System.Net.Http;
using System.Timers;
using System.Diagnostics;
using AthenaApp.Models;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms.Maps;
using AthenaApp.Services;
using System.IO;

namespace AthenaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
       // IEnumerable<string> idOfActType;
        string idOfActType;
        

        bool isGettingLocation;
        double DistanceSum;
        double DistanceStart;
        double DistanceEnd;
        string FinalTime;
        double Velocity;
        string ActIdString;



        // From Login procedure
        //        string UserId = "sadafdsagfsfg";    // from LoginRegister
        //        string CompanyId = "Uni_Siegen";    // from LoginRegister 
        string action = "";


        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }



        Stopwatch AcvtivityTime = new Stopwatch();

        public MapPage()
        {
            InitializeComponent();
            myMap.Pins.Add(pinRunner);
        }

        public Pin pinRunner = new Pin
        {
            Label = "Runner's Position"
        };

        async void StartLocationTrackingButton_Clicked(System.Object sender, System.EventArgs e)
        {


            isGettingLocation = true;

            DistanceSum = 0;
            DistanceStart = 0;
            DistanceEnd = 0;
            bool Activity = false;



            // ToDo: Load from Database
            string Api = "https://10.0.2.2:5001/api/Activities/GetActivities";


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

            var dataAct = JsonConvert.DeserializeObject<Activity[]>(jsonString);

            var listOfAllActivities = new List<Activity>();

            foreach (var json in dataAct)
            {
                listOfAllActivities.Add(json);

                   /* new Activity
                    {
                        Id = json.Id,
                        ActivityType = json.ActivityType,
                        MaxSpeed = json.MaxSpeed,
                        Description = json.Description,
                        SetManualyByUser = json.SetManualyByUser
                    });*/
            }
            
            //            int actCount = listOfAllActivities.Count;
            var actTypes = listOfAllActivities.Select(c => c.ActivityType).ToArray();
            
            if (listOfAllActivities != null)
            {
                action = await DisplayActionSheet("Which Activity do you prefer today?", "Cancel", null, actTypes);
                var ActivityString = action.ToString();
                AcvtivityTime.Reset();
                if (action == "Cancel")
                {
                    AcvtivityTime.Reset();
                }
                else
                {
                    Activity = true;
                    idOfActType = listOfAllActivities.Where(c => c.ActivityType.ToString() == ActivityString).Select(d => d.Id.ToString()).FirstOrDefault();
                    
                    Debug.WriteLine(idOfActType);
                }
            }
            
            //Activity = true;  //für test später löschen

            if (Activity == true)
            {

                Activity_Id.Text = action;
                AcvtivityTime.Start();                              // Start Stopwatch
                StartAt = DateTime.Now;

                // While loop to calculate distance by tracking latitude and longitude
                while (isGettingLocation)
                {

                    // Definition of time units: hours:minutes:seconds:milliseconds
                    double elapsedHours = AcvtivityTime.Elapsed.Hours;
                    double elapsedMinutes = AcvtivityTime.Elapsed.Minutes;
                    double elapsedSeconds = AcvtivityTime.Elapsed.Seconds;
                    double elapsedMilliseconds = AcvtivityTime.Elapsed.Milliseconds;
                    // Converting of time units to string for displaying: hours:minutes:seconds:milliseconds

                    status_textTime.Text = elapsedHours.ToString() + ":" + elapsedMinutes.ToString() + ":" + elapsedSeconds.ToString() + ":" + elapsedMilliseconds.ToString();

                    FinalTime = elapsedHours.ToString() + ":" + elapsedMinutes.ToString() + ":" + elapsedSeconds.ToString() + ":" + elapsedMilliseconds.ToString();
                    DistanceStart = DistanceEnd;                    // Setting Start Distance to End Distance to sum Distance up afterwards, in first loop round DistanceStart == 0
                                                                    // Get Latitude and Longitude through Geolocation


                    var result1 = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(0)));
                    pinRunner.Position = new Position(result1.Latitude, result1.Longitude);
                    myMap.MoveToRegion(MapSpan.FromCenterAndRadius(pinRunner.Position, Distance.FromMeters(500)));

                    await Task.Delay(1000);
                    var result2 = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(5)));
                    pinRunner.Position = new Position(result2.Latitude, result2.Longitude);
                    myMap.MoveToRegion(MapSpan.FromCenterAndRadius(pinRunner.Position, Distance.FromMeters(500)));



                    // Calculate Distance between two measurement Points of Latitude and Longitude
                    double distanceCalc = Location.CalculateDistance(result1.Latitude, result1.Longitude, result2.Latitude, result2.Longitude, DistanceUnits.Kilometers);

                    DistanceEnd = distanceCalc;                     // setting DistanceEnd to the calculated variable distanceCalc
                    DistanceSum = DistanceStart + DistanceEnd;      // summing up Distances
                    DistanceEnd = DistanceSum;                      // setting Distance End as the Sum of Start and End Distance
                    DistanceInfo.Text = DistanceEnd.ToString("0.00") + " KM";
                    Velocity = DistanceSum / elapsedSeconds;


                }
            }
        }


        private async void StopLocationTrackingButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Debug.WriteLine(idOfActType);

            isGettingLocation = false;
            AcvtivityTime.Stop();                   // Stop Stopwatch
            EndAt = DateTime.Now;

            TimeSpan timeTaken = AcvtivityTime.Elapsed;

            bool answer = await DisplayAlert("Activity Tracker", "Would you like to send your Activity: " + "Distance: " + DistanceEnd + " " + "Your Time: " + FinalTime, "Yes", "No");
            Debug.WriteLine("Answer: " + answer);
            if (answer == true)
            {
                // ToDo: Send Data to Server

                
                XamarinManager manager = new XamarinManager();
                string jsonData = manager.Get_post_data();
                var jsonUser = JsonConvert.DeserializeObject<User>(jsonData);

                

                User user = new User()
                {
                    Id = jsonUser.Id,
                    UserName = jsonUser.UserName,
                    CompanyId = jsonUser.CompanyId,
                    SecurityStamp = jsonUser.SecurityStamp,
                    ProfilePicture = jsonUser.ProfilePicture
                };

                var UserCurrentId = user.Id;
                var CompanyId = user.CompanyId;
                



                 var rawData = new UserActivity
                {
                    UserId = UserCurrentId,
                    ActivityId = idOfActType,
                    CompanyId = CompanyId,
                    StartTime = StartAt,
                    StopTime = EndAt,
                    SumTime = timeTaken,
                    SumDistance = DistanceEnd
                };
                Debug.WriteLine("Inhalt: " + rawData.ToString());

                var serializedRawData = JsonConvert.SerializeObject(rawData);
                var requestContent = new StringContent(serializedRawData, Encoding.UTF8, "application/json");
                AcvtivityTime.Reset();
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
                Uri uri = new Uri(string.Format("https://10.0.2.2:5001/api/UserActivities/PostUserActivity"));


                HttpResponseMessage response = await client.PostAsync(uri, requestContent);
                var jsonString = await response.Content.ReadAsByteArrayAsync();


                if (response.IsSuccessStatusCode)
                {
                    if (jsonString is byte[])
                    {
                        await DisplayAlert("Congratulations!", "You recieved a new Badge", "Okay!");
                    }
                    await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    Debug.WriteLine("Error in MapPage.");
                }
                // ToDo: Some more handlers

            }
            AcvtivityTime.Reset();
        }
    }
}