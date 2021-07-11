using AthenaApp.Services;
using Xamarin.Forms;


namespace AthenaApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();

        }

        protected override void OnStart()
        {
            // ToDo: Load everything necesarry from Server
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
