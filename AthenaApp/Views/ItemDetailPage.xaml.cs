using AthenaApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AthenaApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}