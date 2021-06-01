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
            if (userInputMail != "athena@uni.de" || userInputPw != "athena")
            {
                DisplayInvalidLoginPrompt();
            }
            else
            {

//                await Shell.Current.GoToAsync("//AboutPage");
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
        }
    }
}
