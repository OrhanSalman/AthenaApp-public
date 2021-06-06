using AthenaApp.Models;
using AthenaApp.Services;
using AthenaApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AthenaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void ButtonLoginClicked(object sender, EventArgs e)
        {
            LoginService services = new LoginService();
            var getLoginDetails = await services.CheckLoginIfExists(UserInputMail.Text, UserInputPw.Text);

            if (getLoginDetails)
            {
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else if (UserInputMail.Text == null && UserInputPw.Text == null)
            {
                await DisplayAlert("Login failed", "Enter your Email and Password before login", "Okay", "Cancel");
            }
            else
            {
                await DisplayAlert("Login failed", "Username or Password is incorrect or not exists", "Okay", "Cancel");
            }
        }
    }
}