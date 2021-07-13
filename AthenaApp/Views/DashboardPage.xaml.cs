using Microcharts;
using SkiaSharp;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.ChartEntry;


namespace AthenaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // Raphael: Class that sits behind the HTML and delivers the data
    public partial class DashboardPage : ContentPage
    {
        // Raphael: Creates a Entry List with entries for Chart2, a Bar Chart with monthly kilometers
        List<Entry> entries = new List<Entry>
        {
            new Entry (200)
            {
                Color = SKColor.Parse("#081764"),
                Label = "January",
                ValueLabel = "200"
            },
            new Entry (400)
            {
                Color = SKColor.Parse("#081764"),
                Label = "February",
                ValueLabel = "400"
            },
            new Entry (100)
            {
                Color = SKColor.Parse("#081764"),
                Label = "March",
                ValueLabel = "100"
            },
            new Entry (200)
            {
                Color = SKColor.Parse("#081764"),
                Label = "April",
                ValueLabel = "200"
            },
             new Entry (200)
            {
                Color = SKColor.Parse("#081764"),
                Label = "May",
                ValueLabel = "200"
            },
            new Entry (200)
            {
                Color = SKColor.Parse("#081764"),
                Label = "June",
                ValueLabel = "200"
            },
            new Entry (200)
            {
                Color = SKColor.Parse("#081764"),
                Label = "August",
                ValueLabel = "200"
            },
            new Entry (200)
            {
                Color = SKColor.Parse("#081764"),
                Label = "September",
                ValueLabel = "200"
            },
             new Entry (100)
            {
                Color = SKColor.Parse("#081764"),
                Label = "October",
                ValueLabel = "100"
            },
             new Entry (200)
            {
                Color = SKColor.Parse("#081764"),
                Label = "November",
                ValueLabel = "200"
            },
             new Entry (200)
            {
                Color = SKColor.Parse("#081764"),
                Label = "December",
                ValueLabel = "200"
            },
        };
        // Raphael: Creates a Entry List with entries for Chart1, a Line Chart with Accumulated kilometers
        // Raphael: Every entry consists of a value, Bar/Line color, Month Name
        List<Entry> entriesAccumulated = new List<Entry>
        {
            new Entry (200)
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "January",
                ValueLabel = "200"
            },
            new Entry (400)
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "February",
                ValueLabel = "400"
            },
            new Entry (300)
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "March",
                ValueLabel = "300"
            },
            new Entry (500)
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "April",
                ValueLabel = "500"
            },
             new Entry (700)
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "May",
                ValueLabel = "700"
            },
            new Entry (800)
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "June",
                ValueLabel = "800"
            },
            new Entry (800)
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "August",
                ValueLabel = "800"

            },
            new Entry (1000)
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "September",
                ValueLabel = "1000"
            },
             new Entry (1100)
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "October",
                ValueLabel = "1100"
            },
             new Entry (1200)
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "November",
                ValueLabel = "1200"
            },
             new Entry (1500)
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "December",
                ValueLabel = "1500"
            },
        };
        public DashboardPage()
        {

            InitializeComponent();
            // Raphael: With start of app Charts are created with the implemented Lists. The Background is transparent for the coloured frame -> Additional Options for the appearance are made
            Chart1.Chart = new LineChart() { Entries = entriesAccumulated, BackgroundColor = SKColors.Transparent, LineSize = 20, PointSize = 20, LabelOrientation = Orientation.Horizontal, IsAnimated = true };
            Chart2.Chart = new BarChart() { Entries = entries, BackgroundColor = SKColors.Transparent, BarAreaAlpha = 30, IsAnimated = true, LabelOrientation = Orientation.Horizontal };


        }
    }
}