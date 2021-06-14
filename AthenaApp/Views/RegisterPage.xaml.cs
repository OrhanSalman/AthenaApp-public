using AthenaApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AthenaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }



        private async void RegisterButton(object sender, EventArgs e)
        {
            
            RegisterService registerService = new RegisterService();
            var postRegisterUser = await registerService.CheckRegisterIfExists(UserInputName.Text, UserInputUniversity.Text, UserInputMail.Text);
            
            if (postRegisterUser)
            {
                await DisplayAlert("Registration succeeded", "Welcome new user!", "Okay", "Cancel");
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else if (UserInputMail.Text == null && UserInputPw.Text == null)
            {
                await DisplayAlert("Registration failed", "Enter your Email and Password before login", "Okay", "Cancel");
            }
            else
            {
                await DisplayAlert("Registration failed", "Username or Password is incorrect or not exists", "Okay", "Cancel");
            }
            
        }
    }
}
