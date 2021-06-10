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
        public MapPage()
        {
            InitializeComponent();
        }

        async void StartLocationTrackingButton_Clicked(System.Object sender, System.EventArgs e)
        {
            isGettingLocation = true;

            while (isGettingLocation) {
                var result = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1)));

                resultLocation.Text += $"lat: {result.Latitude}. lng: {result.Longitude} {Environment.NewLine}";

                
            }           // Test
        }
        void StopLocationTrackingButton_Clicked(System.Object sender, System.EventArgs e)
        {
            isGettingLocation = false;
        }
        
    }
}



/*var locator = CrossGeolocator.Current;
locator.DesiredAccuracy = 5;
var position = await locator.GetPositionAsync(TimeSpan.FromMilliseconds(100));
LongitudeLabel.Text = position.Longitude.ToString();        // Test
LatitudeLabel.Text = position.Latitude.ToString();*/