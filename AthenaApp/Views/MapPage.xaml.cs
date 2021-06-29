using Plugin.Geolocator;
using System;
using System.Collections.Generic;
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

namespace AthenaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        bool isGettingLocation;
        double DistanceSum;
        double DistanceStart;
        double DistanceEnd;
        string FinalTime;
        double FinalTimeData;
        


        Stopwatch AcvtivityTime = new Stopwatch();

        public MapPage()
        {
            InitializeComponent();
        }

        async void StartLocationTrackingButton_Clicked(System.Object sender, System.EventArgs e)
        {
            AcvtivityTime.Start();                              // Start Stopwatch
            isGettingLocation = true;
            DistanceSum = 0;
            DistanceStart = 0;
            DistanceEnd = 0;

                                                                // While loop to calculate distance by tracking latitude and longitude
            while (isGettingLocation)    
            {
                // Definition of time units: hours:minutes:seconds:milliseconds
                double elapsedHours = AcvtivityTime.Elapsed.Hours;          
                double elapsedMinutes = AcvtivityTime.Elapsed.Minutes;
                double elapsedSeconds = AcvtivityTime.Elapsed.Seconds;
                double elapsedMilliseconds = AcvtivityTime.Elapsed.Milliseconds;
                // Converting of time units to string for displaying: hours:minutes:seconds:milliseconds
               
                /* status_textH.Text = elapsedHours.ToString();                   delete
                status_textM.Text = elapsedMinutes.ToString();                      
                status_textS.Text = elapsedSeconds.ToString();
                status_textMS.Text = elapsedMilliseconds.ToString();*/             
                
                status_textTime.Text = elapsedHours.ToString() + ":" + elapsedMinutes.ToString() + ":" + elapsedSeconds.ToString() + ":" + elapsedMilliseconds.ToString();
                FinalTimeData = elapsedHours + elapsedMinutes + elapsedSeconds + elapsedMilliseconds;
                FinalTime = elapsedHours.ToString() + ":" + elapsedMinutes.ToString() + ":" + elapsedSeconds.ToString() + ":" + elapsedMilliseconds.ToString();
                DistanceStart = DistanceEnd;                    // Setting Start Distance to End Distance to sum Distance up afterwards, in first loop round DistanceStart == 0
                                                                // Get Latitude and Longitude through Geolocation
                var result1 = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(2)));
                var result2 = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(5)));
                                                                // Visualization of Latitude and Longitude -> delete in final version
//                result1Location.Text += $"lat: {result1.Latitude}. lng: {result1.Longitude} {Environment.NewLine}";
//                result2Location.Text += $"lat: {result2.Latitude}. lng: {result2.Longitude} {Environment.NewLine}";

                                                                // Calculate Distance between two measurement Points of Latitude and Longitude
                double distanceCalc = Location.CalculateDistance(result1.Latitude, result1.Longitude, result2.Latitude, result2.Longitude, DistanceUnits.Kilometers );
                                                
                DistanceEnd = distanceCalc;                     // setting DistanceEnd to the calculated variable distanceCalc
                DistanceSum = DistanceStart + DistanceEnd;      // summing up Distances
                DistanceEnd = DistanceSum;                      // setting Distance End as the Sum of Start and End Distance
                                                                // Visualization of Distance -> delete in final version
                DistanceInfo.Text = DistanceEnd.ToString();
                // DistanceInfo.Text += $"Distance: {DistanceEnd} {Environment.NewLine}";
                
                

                //                Thread.Sleep(3000);
            }
            
        }


        private async void StopLocationTrackingButton_Clicked(System.Object sender, System.EventArgs e)
        {
            isGettingLocation = false;
            AcvtivityTime.Stop();                   // Stop Stopwatch


            bool answer = await DisplayAlert("Activity Tracker", "Would you like to send your Activity: " + "Distance: " + DistanceEnd + " " + "Your Time: " + FinalTime, "Yes", "No");
            Debug.WriteLine("Answer: " + answer);

            // await DisplayAlert("Your Avtivity: ", DistanceEnd.ToString(), "Your Time: ", FinalTime);
            // ToDo: Send Data to Database
            // Data == double FinalTimeData(check for correctness), double DistanceEnd
            


            AcvtivityTime.Reset();                  // Reset Stopwatch to 0



            // ToDo:
            // PopUp, "X-Meter gelaufen, XX:XX, Senden, Cancel"
            //await DisplayAlert("", "", "", "");
            

            var serializedRawData = JsonConvert.SerializeObject("");
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
            Uri uri = new Uri(string.Format("https://10.0.2.2:5001/api/Users/AppRegisterRequestiohio/"));


            HttpResponseMessage response = await client.PostAsync(uri, requestContent);

        }

    }
}
