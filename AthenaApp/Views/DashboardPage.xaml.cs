using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts.Forms;
using Entry = Microcharts.ChartEntry;
using Microcharts;

namespace AthenaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        
        List<Entry> entries = new List<Entry>
        {
            new Entry (200)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "January",
                ValueLabel = "200"
            },
            new Entry (400)
            {
                Color = SKColor.Parse("#00BFFF"),
                Label = "February",
                ValueLabel = "400"
            },
            new Entry (-100)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "March",
                ValueLabel = "-100"
            },
            new Entry (200)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "April",
                ValueLabel = "200"
            },
             new Entry (200)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "May",
                ValueLabel = "200"
            },
            new Entry (200)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "June",
                ValueLabel = "200"
            },
            new Entry (200)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "August",
                ValueLabel = "200"
            },
            new Entry (200)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "September",
                ValueLabel = "200"
            },
             new Entry (100)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "October",
                ValueLabel = "100"
            },
             new Entry (200)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "November",
                ValueLabel = "200"
            },
             new Entry (200)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "December",
                ValueLabel = "200"
            },
        };

        List<Entry> entriesAccumulated = new List<Entry>
        {
            new Entry (200)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "January",
                ValueLabel = "200"
            },
            new Entry (400)
            {
                Color = SKColor.Parse("#00BFFF"),
                Label = "February",
                ValueLabel = "400"
            },
            new Entry (300)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "March",
                ValueLabel = "300"
            },
            new Entry (500)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "April",
                ValueLabel = "500"
            },
             new Entry (700)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "May",
                ValueLabel = "700"
            },
            new Entry (800)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "June",
                ValueLabel = "800"
            },
            new Entry (800)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "August",
                ValueLabel = "800"
            },
            new Entry (1000)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "September",
                ValueLabel = "1000"
            },
             new Entry (1100)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "October",
                ValueLabel = "1100"
            },
             new Entry (1200)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "November",
                ValueLabel = "1200"
            },
             new Entry (1500)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "December",
                ValueLabel = "1500"
            },
        };
        public Dashboard()
        {
            InitializeComponent();
            Chart1.Chart = new LineChart()  {Entries = entriesAccumulated};
            Chart2.Chart = new BarChart() { Entries = entries };
            
        }
    }
}