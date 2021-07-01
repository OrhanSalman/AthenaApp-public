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

namespace AthenaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        IEnumerable<string> idOfActType = Enumerable.Empty<string>();

        bool isGettingLocation;
        double DistanceSum;
        double DistanceStart;
        double DistanceEnd;
        string FinalTime;

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
        }
               
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

            var data = JsonConvert.DeserializeObject<Activity[]>(jsonString);

            List<Activity> listOfAllActivities = new List<Activity>();

            foreach (var json in data)
            {
                listOfAllActivities.Add(
                    new Activity
                    {
                        Id = json.Id,
                        ActivityType = json.ActivityType,
                        MaxSpeed = json.MaxSpeed,
                        Description = json.Description,
                        SetManualyByUser = json.SetManualyByUser
                    });
            }

//            int actCount = listOfAllActivities.Count;
            var actTypes = listOfAllActivities.Select(c => c.ActivityType).ToArray();
            
            if (listOfAllActivities != null) {
                action = await DisplayActionSheet("Which Activity do you prefer today?", "Cancel", null, actTypes);

                AcvtivityTime.Reset();
                if (action == "Cancel")
                {
                    AcvtivityTime.Reset();
                }
                else
                {
                    Activity = true;
                    idOfActType = listOfAllActivities.Where(c => c.ActivityType == action).Select(d => d.Id);
                    Debug.WriteLine(idOfActType);
                }
            }


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
                    var result1 = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(2)));
                    var result2 = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10)));
                    // Visualization of Latitude and Longitude -> delete in final version

                    // Calculate Distance between two measurement Points of Latitude and Longitude
                    double distanceCalc = Location.CalculateDistance(result1.Latitude, result1.Longitude, result2.Latitude, result2.Longitude, DistanceUnits.Kilometers);
                    
                    DistanceEnd = distanceCalc;                     // setting DistanceEnd to the calculated variable distanceCalc
                    DistanceSum = DistanceStart + DistanceEnd;      // summing up Distances
                    DistanceEnd = DistanceSum;                      // setting Distance End as the Sum of Start and End Distance
                    DistanceInfo.Text = DistanceEnd.ToString("0.00") + " KM";
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
            if(answer == true)
            {
                // ToDo: Send Data to Server
                

                var rawData = new UserActivity
                {
                    UserId = "d52f4e8e-7a86-4279-82d1-749b67b99a92",
                    ActivityId = "49f7fee1-01e1-4911-b2a0-ad5fdc0b4ab4",
                    CompanyId = "42ed7e99-7941-4c17-9b3e-1afbb5bd65fe",
                    StartTime = StartAt,
                    StopTime = EndAt,
                    SumTime = timeTaken,
                    SumDistance = DistanceEnd
                };
                Debug.WriteLine("Inhalt: " + rawData.ToString());
                
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
                Uri uri = new Uri(string.Format("https://10.0.2.2:5001/api/UserActivities/PostUserActivity"));


                HttpResponseMessage response = await client.PostAsync(uri, requestContent);
                               
                if(response.IsSuccessStatusCode)
                {
                    await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    Debug.WriteLine("nöö");
                }
                // ToDo: Some more handlers

            }
            AcvtivityTime.Reset();


        }

    }
}





