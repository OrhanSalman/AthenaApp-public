using AthenaApp.Services;
using AthenaApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using AthenaApp.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AthenaApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChangedNew = delegate { };



        private string userInputMail;
        public string UserInputMail
        {
            get { return userInputMail; }
            set
            {
                userInputMail = value;
                PropertyChangedNew(this, new PropertyChangedEventArgs("UserInputMail"));
            }
        }

        private string userInputPw;
        public string UserInputPw
        {
            get { return userInputPw; }
            set
            {
                userInputPw = value;
                PropertyChangedNew(this, new PropertyChangedEventArgs("UserInputPw"));
            }
        }
        public Command LoginComand { get; }
        public LoginViewModel()
        {
            LoginComand = new Command(OnLoginClicked);
        }


        private async void OnLoginClicked(object obj)
        {

            var content = await WebService.ServiceClientInstance.AuthenticateUserAsync(UserInputMail, UserInputPw);
            Console.WriteLine(content.ToString());
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            /*
            if (!string.IsNullOrEmpty(content.authenticationToken))
            {
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
//                await Navigation.PushAsync(new DashboardPage());

            }
            else
            {
//                DisplayInvalidLoginPrompt();
                await App.Current.MainPage.DisplayAlert("Alert", "Something Went Worng", "Ok");

            }
            */
        }
    }
}
