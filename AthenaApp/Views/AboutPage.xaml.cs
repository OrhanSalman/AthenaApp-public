using System;
using Xamarin.Forms;

namespace AthenaApp.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            // Alle wartenden Pages, die nicht unmittelbar bevorstehen
        }

        private async void GoToSignIn(object sender, EventArgs e)
        {
            // AppShell laden für Login/Register
            // await Navigation.PushAsync(new LoginRegisterPage());
            // await Shell.Current.GoToAsync($"//{nameof(LoginRegisterPage)}");
        }

        /*
        private async void GoToSignUp(object sender, EventArgs e)
        {
            // AppShell laden für Login/Register

            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }*/
    }
}