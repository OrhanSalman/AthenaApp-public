using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace AthenaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        bool isGettingLocation;
        double DistanceSum;
        double DistanceStart;
        double DistanceEnd;

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

                                                                // While loop to calculate distance by tracking latitude and longitude
            while (isGettingLocation)    
            {
                DistanceStart = DistanceEnd;                    // Setting Start Distance to End Distance to sum Distance up afterwards, in first loop round DistanceStart == 0
                                                                // Get Latitude and Longitude through Geolocation
                var result1 = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10)));
                var result2 = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10)));
                                                                // Visualization of Latitude and Longitude -> delete in final version
                result1Location.Text += $"lat: {result1.Latitude}. lng: {result1.Longitude} {Environment.NewLine}";
                result2Location.Text += $"lat: {result2.Latitude}. lng: {result2.Longitude} {Environment.NewLine}";

                                                                // Calculate Distance between two measurement Points of Latitude and Longitude
                double distanceCalc = Location.CalculateDistance(result1.Latitude, result1.Longitude, result2.Latitude, result2.Longitude, DistanceUnits.Kilometers );
                                                
                DistanceEnd = distanceCalc;                     // setting DistanceEnd to the calculated variable distanceCalc
                DistanceSum = DistanceStart + DistanceEnd;      // summing up Distances
                DistanceEnd = DistanceSum;                      // setting Distance End as the Sum of Start and End Distance
                                                                // Visualization of Distance -> delete in final version
                DistanceInfo.Text += $"Distance: {DistanceEnd} {Environment.NewLine}";

            }
        }

 

        void StopLocationTrackingButton_Clicked(System.Object sender, System.EventArgs e)
        {
            isGettingLocation = false;
        }
        
    }
}
