using AthenaApp.Models;
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
            var viewModel = new LoginViewModel();
            this.BindingContext = viewModel;
            viewModel.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");
            InitializeComponent();

            UserInputMail.Completed += (object sender, EventArgs e) =>
            {
                UserInputPw.Focus();
            };

            UserInputPw.Completed += (object sender, EventArgs e) =>
            {
                viewModel.LoginComand.Execute(null);
            };
        }
    }
}