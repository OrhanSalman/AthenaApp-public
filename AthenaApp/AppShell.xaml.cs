using AthenaApp.ViewModels;
using AthenaApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AthenaApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Alle wartenden Pages, die nicht unmittelbar bevorstehen
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        }
/*
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
*/
    }
}
