﻿using AthenaApp.Models;
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
            this.BindingContext = this;

        }

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");   // Testversion, später entfernen
            
/*
            LoginService services = new LoginService();
            var getLoginDetails = await services.CheckLoginIfExists(UserInputMail.Text, UserInputPw.Text);

            if (getLoginDetails)
            {
                await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
            }
            else if (UserInputMail.Text == null && UserInputPw.Text == null)
            {
                await DisplayAlert("Login failed", "Enter your Email and Password before login", "Okay", "Cancel");
            }
            else
            {
                await DisplayAlert("Login failed", "Username or Password is incorrect or not exists", "Okay", "Cancel");
            }
*/            

        }
    }
}