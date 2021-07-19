﻿using AthenaApp.Models;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.ChartEntry;
using AthenaApp.Services;



namespace AthenaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // Raphael: Class that sits behind the HTML and delivers the data
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();
        }

        List<Entry> entries = new List<Entry>();

        IEnumerable<double> JanUserDistanceEnum;
        IEnumerable<double> FebUserDistanceEnum;
        IEnumerable<double> MarUserDistanceEnum;
        IEnumerable<double> AprUserDistanceEnum;
        IEnumerable<double> MayUserDistanceEnum;
        IEnumerable<double> JunUserDistanceEnum;
        IEnumerable<double> JulUserDistanceEnum;
        IEnumerable<double> AugUserDistanceEnum;
        IEnumerable<double> SepUserDistanceEnum;
        IEnumerable<double> OctUserDistanceEnum;
        IEnumerable<double> NovUserDistanceEnum;
        IEnumerable<double> DecUserDistanceEnum;

        double JanUserDistanceTotal;
        double FebUserDistanceTotal;
        double MarUserDistanceTotal;
        double AprUserDistanceTotal;
        double MayUserDistanceTotal;
        double JunUserDistanceTotal;
        double JulUserDistanceTotal;
        double AugUserDistanceTotal;
        double SepUserDistanceTotal;
        double OctUserDistanceTotal;
        double NovUserDistanceTotal;
        double DecUserDistanceTotal;

        double FebUserDistanceAccumulated;
        double MarUserDistanceAccumulated;
        double AprUserDistanceAccumulated;
        double MayUserDistanceAccumulated;
        double JunUserDistanceAccumulated;
        double JulUserDistanceAccumulated;
        double AugUserDistanceAccumulated;
        double SepUserDistanceAccumulated;
        double OctUserDistanceAccumulated;
        double NovUserDistanceAccumulated;
        double DecUserDistanceAccumulated;

        readonly string January = "1";
        readonly string February = "2";
        readonly string March = "3";
        readonly string April = "4";
        readonly string May = "5";
        readonly string June = "6";
        readonly string July = "7";
        readonly string August = "8";
        readonly string September = "9";
        readonly string October = "10";
        readonly string November = "11";
        readonly string December = "12";
        
        string year = DateTime.Now.Year.ToString();

        async void DataRefreshButton_Clicked(System.Object sender, System.EventArgs e)
        {
            string Api = "https://10.0.2.2:5001/api/UserActivities/GetUserActivitiesOfUserId";

            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            HttpClient client = client = new HttpClient(handler);

            Uri uri = new Uri(string.Format(Api));
            HttpResponseMessage response = await client.GetAsync(uri);


             var jsonString = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<UserActivity[]>(jsonString);
            List<UserActivity> listOfAllUserActivities = new List<UserActivity>();

            foreach (var json in data)
            {
                listOfAllUserActivities.Add(
                    new UserActivity
                    {
                        Id = json.Id,
                        UserId = json.UserId,
                        ActivityId = json.ActivityId,
                        CompanyId = json.CompanyId,
                        StartTime = json.StartTime,             
                        StopTime = json.StopTime,               
                        SumTime = json.SumTime,                
                        SumDistance = json.SumDistance          


                    });
            }


            //            int actCount = listOfAllActivities.Count;
            // ToDo: filter Month var StopTimeList = (listOfAllUserActivities.Select(c => c.StopTime));



            var distanceList = (listOfAllUserActivities.Select(c => c.SumDistance));
            var totalDistance = distanceList.Sum(item => (float)item);

            FebUserDistanceAccumulated = JanUserDistanceTotal + FebUserDistanceTotal;
            MarUserDistanceAccumulated = FebUserDistanceAccumulated + MarUserDistanceTotal;
            AprUserDistanceAccumulated = MarUserDistanceAccumulated + AprUserDistanceTotal;
            MayUserDistanceAccumulated = AprUserDistanceAccumulated + MayUserDistanceTotal;
            JunUserDistanceAccumulated = MayUserDistanceAccumulated + JunUserDistanceTotal;
            JulUserDistanceAccumulated = JunUserDistanceAccumulated + JulUserDistanceTotal;
            AugUserDistanceAccumulated = JulUserDistanceAccumulated + AugUserDistanceTotal;
            SepUserDistanceAccumulated = AugUserDistanceAccumulated + SepUserDistanceTotal;
            OctUserDistanceAccumulated = SepUserDistanceAccumulated + OctUserDistanceTotal;
            NovUserDistanceAccumulated = OctUserDistanceAccumulated + NovUserDistanceTotal;
            DecUserDistanceAccumulated = NovUserDistanceAccumulated + DecUserDistanceTotal;

            XamarinManager manager = new XamarinManager();
            string jsonData = manager.Get_post_data();
            var jsonUser = JsonConvert.DeserializeObject<User>(jsonData);

            

            User user = new User()
            {
                Id = jsonUser.Id,
                UserName = jsonUser.UserName,
                CompanyId = jsonUser.CompanyId,
                SecurityStamp = jsonUser.SecurityStamp,
                ProfilePicture = jsonUser.ProfilePicture
            };

            var UserCurrentId = user.Id;

            foreach (var json in listOfAllUserActivities) 
            {
                JanUserDistanceEnum = listOfAllUserActivities.Where(c => (c.StopTime.Month.ToString() == January) && (c.UserId == UserCurrentId )).Select(d => d.SumDistance);
                JanUserDistanceTotal = JanUserDistanceEnum.Sum((item => (float)item));    
            }
            foreach (var json in listOfAllUserActivities)
            {
                FebUserDistanceEnum = listOfAllUserActivities.Where(c => (c.StopTime.Month.ToString() == February) && (c.UserId == UserCurrentId)).Select(d => d.SumDistance);
                FebUserDistanceTotal = FebUserDistanceEnum.Sum((item => (float)item));
            }
            foreach (var json in listOfAllUserActivities)
            {
                MarUserDistanceEnum = listOfAllUserActivities.Where(c => (c.StopTime.Month.ToString() == March) && (c.UserId == UserCurrentId)).Select(d => d.SumDistance);
                MarUserDistanceTotal = MarUserDistanceEnum.Sum((item => (float)item));
            }
            foreach (var json in listOfAllUserActivities)
            {
                AprUserDistanceEnum = listOfAllUserActivities.Where(c => (c.StopTime.Month.ToString() == April) && (c.UserId == UserCurrentId)).Select(d => d.SumDistance);
                AprUserDistanceTotal = AprUserDistanceEnum.Sum((item => (float)item));            
            }
            foreach (var json in listOfAllUserActivities)
            {
                MayUserDistanceEnum = listOfAllUserActivities.Where(c => (c.StopTime.Month.ToString() == May) && (c.UserId == UserCurrentId)).Select(d => d.SumDistance);
                MayUserDistanceTotal = MayUserDistanceEnum.Sum((item => (float)item));
            }
            foreach (var json in listOfAllUserActivities)
            {
                JunUserDistanceEnum = listOfAllUserActivities.Where(c => (c.StopTime.Month.ToString() == June) && (c.UserId == UserCurrentId)).Select(d => d.SumDistance);
                JunUserDistanceTotal = JunUserDistanceEnum.Sum((item => (float)item));
            }
            foreach (var json in listOfAllUserActivities)
            {
                JulUserDistanceEnum = listOfAllUserActivities.Where(c => (c.StopTime.Month.ToString() == July) && (c.UserId == UserCurrentId)).Select(d => d.SumDistance);
                JulUserDistanceTotal = JulUserDistanceEnum.Sum((item => (float)item));
            }
            foreach (var json in listOfAllUserActivities)
            {
                AugUserDistanceEnum = listOfAllUserActivities.Where(c => (c.StopTime.Month.ToString() == August) && (c.UserId == UserCurrentId)).Select(d => d.SumDistance);
                AugUserDistanceTotal = AugUserDistanceEnum.Sum((item => (float)item));
            }
            foreach (var json in listOfAllUserActivities)
            {
                SepUserDistanceEnum = listOfAllUserActivities.Where(c => (c.StopTime.Month.ToString() == September) && (c.UserId == UserCurrentId)).Select(d => d.SumDistance);
                SepUserDistanceTotal = SepUserDistanceEnum.Sum((item => (float)item));
            }
            foreach (var json in listOfAllUserActivities)
            {
                OctUserDistanceEnum = listOfAllUserActivities.Where(c => (c.StopTime.Month.ToString() == October) && (c.UserId == UserCurrentId)).Select(d => d.SumDistance);
                OctUserDistanceTotal = OctUserDistanceEnum.Sum((item => (float)item));
            }
            foreach (var json in listOfAllUserActivities)
            {
                NovUserDistanceEnum = listOfAllUserActivities.Where(c => (c.StopTime.Month.ToString() == November) && (c.UserId == UserCurrentId)).Select(d => d.SumDistance);
                NovUserDistanceTotal = NovUserDistanceEnum.Sum((item => (float)item));
            }
            foreach (var json in listOfAllUserActivities)
            {
                DecUserDistanceEnum = listOfAllUserActivities.Where(c => (c.StopTime.Month.ToString() == December) && (c.UserId == UserCurrentId)).Select(d => d.SumDistance);
                DecUserDistanceTotal = DecUserDistanceEnum.Sum((item => (float)item));
            }

            List<Entry> entries = new List<Entry>()
            {
            new Entry(float.Parse(JanUserDistanceTotal.ToString())) //float.Parse(total.ToString())
            {
                 Color = SKColor.Parse("#081764"),
                 Label = "January",
                 ValueLabel = JanUserDistanceTotal.ToString("0.00")
            },

            new Entry(float.Parse(FebUserDistanceTotal.ToString()))
            {
                Color = SKColor.Parse("#081764"),
                Label = "February",
                ValueLabel = FebUserDistanceTotal.ToString("0.00")
                },
            new Entry(float.Parse(MarUserDistanceTotal.ToString()))
            {
                Color = SKColor.Parse("#081764"),
                Label = "March",
                ValueLabel = MarUserDistanceTotal.ToString("0.00")
            },
            new Entry(float.Parse(AprUserDistanceTotal.ToString()))
            {
                Color = SKColor.Parse("#081764"),
                Label = "April",
                ValueLabel = AprUserDistanceTotal.ToString("0.00")
            },
             new Entry(float.Parse(MayUserDistanceTotal.ToString()))
             {
                 Color = SKColor.Parse("#081764"),
                 Label = "May",
                 ValueLabel = MayUserDistanceTotal.ToString("0.00")
             },
            new Entry(float.Parse(JunUserDistanceTotal.ToString()))
            {
                Color = SKColor.Parse("#081764"),
                Label = "June",
                ValueLabel = JunUserDistanceTotal.ToString("0.00")
            },
            new Entry(float.Parse(JulUserDistanceTotal.ToString()))
            {
                Color = SKColor.Parse("#081764"),
                Label = "July",
                ValueLabel = JulUserDistanceTotal.ToString("0.00")
            },
            new Entry(float.Parse(AugUserDistanceTotal.ToString()))
            {
                Color = SKColor.Parse("#081764"),
                Label = "August",
                ValueLabel = AugUserDistanceTotal.ToString("0.00")
            },
            new Entry(float.Parse(SepUserDistanceTotal.ToString()))
            {
                Color = SKColor.Parse("#081764"),
                Label = "September",
                ValueLabel = SepUserDistanceTotal.ToString("0.00")
            },
             new Entry(float.Parse(OctUserDistanceTotal.ToString()))
             {
                 Color = SKColor.Parse("#081764"),
                 Label = "October",
                 ValueLabel = OctUserDistanceTotal.ToString("0.00")
             },
             new Entry(float.Parse(NovUserDistanceTotal.ToString()))
             {
                 Color = SKColor.Parse("#081764"),
                 Label = "November",
                 ValueLabel = NovUserDistanceTotal.ToString("0.00")
             },
             new Entry(float.Parse(DecUserDistanceTotal.ToString()))
             {
                 Color = SKColor.Parse("#081764"),
                 Label = "December",
                 ValueLabel = DecUserDistanceTotal.ToString("0.00")
             },
            };
            Chart2.Chart = new BarChart() { Entries = (IEnumerable<Entry>)entries, BackgroundColor = SKColors.Transparent, BarAreaAlpha = 30, IsAnimated = true, LabelOrientation = Orientation.Horizontal };



            List<Entry> entriesAccumulated = new List<Entry>
        {
            
            new Entry (float.Parse(JanUserDistanceTotal.ToString()))
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "January",
                ValueLabel = JanUserDistanceTotal.ToString("0.00")
            },
            new Entry (float.Parse(FebUserDistanceAccumulated.ToString()))
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "February",
                ValueLabel = FebUserDistanceAccumulated.ToString("0.00")
            },
            new Entry (float.Parse(MarUserDistanceAccumulated.ToString()))
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "March",
                ValueLabel = MarUserDistanceAccumulated.ToString("0.00")
            },
            new Entry (float.Parse(AprUserDistanceAccumulated.ToString()))
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "April",
                ValueLabel = AprUserDistanceAccumulated.ToString("0.00")
            },
             new Entry (float.Parse(MayUserDistanceAccumulated.ToString()))
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "May",
                ValueLabel = MayUserDistanceAccumulated.ToString("0.00")
            },
            new Entry (float.Parse(JunUserDistanceAccumulated.ToString()))
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "June",
                ValueLabel = JunUserDistanceAccumulated.ToString("0.00")
            },
            new Entry (float.Parse(JulUserDistanceAccumulated.ToString()))
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "July",
                ValueLabel = JulUserDistanceAccumulated.ToString("0.00")
            },
            new Entry (float.Parse(AugUserDistanceAccumulated.ToString()))
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "August",
                ValueLabel = AugUserDistanceAccumulated.ToString("0.00")

            },
            new Entry (float.Parse(SepUserDistanceAccumulated.ToString()))
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "September",
                ValueLabel = SepUserDistanceAccumulated.ToString("0.00")
            },
             new Entry (float.Parse(OctUserDistanceAccumulated.ToString()))
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "October",
                ValueLabel = OctUserDistanceAccumulated.ToString("0.00")
            },
             new Entry (float.Parse(NovUserDistanceAccumulated.ToString()))
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "November",
                ValueLabel = NovUserDistanceAccumulated.ToString("0.00")
            },
             new Entry (float.Parse(DecUserDistanceAccumulated.ToString()))
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "December",
                ValueLabel = DecUserDistanceAccumulated.ToString("0.00")
            },
        };
            Chart1.Chart = new LineChart() { Entries = entriesAccumulated, BackgroundColor = SKColors.Transparent, LineSize = 20, PointSize = 20, LabelOrientation = Orientation.Horizontal, IsAnimated = true };
        }
    }
}