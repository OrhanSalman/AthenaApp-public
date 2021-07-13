using AthenaApp.Models;
using AthenaApp.ViewModels;
using Xamarin.Forms;

namespace AthenaApp.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}