using AthenaApp.Models;
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
using AthenaWebApp.Models;

namespace AthenaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // Raphael: Class that sits behind the HTML and delivers the data
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();
            DashboardData();
        }

        
        
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
        
        



        async void DashboardData()
        {

            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();

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

            double JanUserDistanceTotal = 0;
            double FebUserDistanceTotal = 0;
            double MarUserDistanceTotal = 0;
            double AprUserDistanceTotal = 0;
            double MayUserDistanceTotal = 0;
            double JunUserDistanceTotal = 0;
            double JulUserDistanceTotal = 0;
            double AugUserDistanceTotal = 0;
            double SepUserDistanceTotal = 0;
            double OctUserDistanceTotal = 0;
            double NovUserDistanceTotal = 0;
            double DecUserDistanceTotal = 0;

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

            //Useractivities data request

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

            // Universities data request
            string ApiCom = "https://10.0.2.2:5001/api/Companies/GetCompanies";

            HttpClientHandler handlerCom = new HttpClientHandler();
            handlerCom.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            HttpClient clientCom = clientCom = new HttpClient(handlerCom);

            Uri uriCom = new Uri(string.Format(ApiCom));
            HttpResponseMessage responseCom = await client.GetAsync(uriCom);


            var jsonStringCom = await responseCom.Content.ReadAsStringAsync();

            var dataCom = JsonConvert.DeserializeObject<Company[]>(jsonStringCom);
            List<Company> listOfCompanies = new List<Company>();

            foreach (var company in dataCom)
            {
                listOfCompanies.Add(
                    new Company
                    {
                        Id = company.Id,
                        CompanyName = company.CompanyName,
                        Country = company.Country,
                        EmailContext = company.EmailContext
                    });
            }

            


            //            int actCount = listOfAllActivities.Count;
            // ToDo: filter Month var StopTimeList = (listOfAllUserActivities.Select(c => c.StopTime));







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

            var UserUniversity = listOfCompanies.Where(company => company.Id == user.CompanyId).Select(company => company.CompanyName).FirstOrDefault();
            UniversityInfo.Text = UserUniversity;

            var EnumMonthlyUniKm = listOfAllUserActivities.Where(useract => (useract.CompanyId == user.CompanyId) && (useract.StopTime.Month.ToString() == month)).Select(useract => useract.SumDistance);
            var CurrentMonthUniDistance = EnumMonthlyUniKm.Sum();

            var EnumDailyUniKm = listOfAllUserActivities.Where(useract => (useract.CompanyId == user.CompanyId) && (useract.StopTime.Day.ToString() == day)).Select(useract => useract.SumDistance);
            var CurrentDayUniDistance = EnumDailyUniKm.Sum();

            var EnumUserKm = listOfAllUserActivities.Where(useract => useract.UserId == user.Id).Select(useract => useract.SumDistance);
            var totalUserKm = EnumUserKm.Sum();

            MonthlyKmInfo.Text = CurrentMonthUniDistance.ToString("0.00") + " KM";
            DailyKmInfo.Text = CurrentDayUniDistance.ToString("0.00") + " KM";
            //MembersInfo.Text = 
            PersonalKmInfo.Text = totalUserKm.ToString("0.00") + " KM";


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


            var distanceListJan = (listOfAllUserActivities.Where(c => c.StopTime.Month.ToString() == January).Select(c => c.SumDistance));
            var totalDistanceJan = distanceListJan.Sum(item => (float)item);

            var distanceListFeb = (listOfAllUserActivities.Where(c => c.StopTime.Month.ToString() == February).Select(c => c.SumDistance));
            var totalDistanceFeb = distanceListFeb.Sum(item => (float)item);

            var distanceListMar = (listOfAllUserActivities.Where(c => c.StopTime.Month.ToString() == March).Select(c => c.SumDistance));
            var totalDistanceMar = distanceListMar.Sum(item => (float)item);

            var distanceListApr = (listOfAllUserActivities.Where(c => c.StopTime.Month.ToString() == April).Select(c => c.SumDistance));
            var totalDistanceApr = distanceListApr.Sum(item => (float)item);

            var distanceListMay = (listOfAllUserActivities.Where(c => c.StopTime.Month.ToString() == May).Select(c => c.SumDistance));
            var totalDistanceMay = distanceListMay.Sum(item => (float)item);

            var distanceListJun = (listOfAllUserActivities.Where(c => c.StopTime.Month.ToString() == June).Select(c => c.SumDistance));
            var totalDistanceJun = distanceListJun.Sum(item => (float)item);

            var distanceListJul = (listOfAllUserActivities.Where(c => c.StopTime.Month.ToString() == July).Select(c => c.SumDistance));
            var totalDistanceJul = distanceListJul.Sum(item => (float)item);

            var distanceListAug = (listOfAllUserActivities.Where(c => c.StopTime.Month.ToString() == August).Select(c => c.SumDistance));
            var totalDistanceAug = distanceListAug.Sum(item => (float)item);

            var distanceListSep = (listOfAllUserActivities.Where(c => c.StopTime.Month.ToString() == September).Select(c => c.SumDistance));
            var totalDistanceSep = distanceListSep.Sum(item => (float)item);

            var distanceListOct = (listOfAllUserActivities.Where(c => c.StopTime.Month.ToString() == October).Select(c => c.SumDistance));
            var totalDistanceOct = distanceListOct.Sum(item => (float)item);

            var distanceListNov = (listOfAllUserActivities.Where(c => c.StopTime.Month.ToString() == November).Select(c => c.SumDistance));
            var totalDistanceNov = distanceListNov.Sum(item => (float)item);

            var distanceListDec = (listOfAllUserActivities.Where(c => c.StopTime.Month.ToString() == December).Select(c => c.SumDistance));
            var totalDistanceDec = distanceListDec.Sum(item => (float)item);

            FebUserDistanceAccumulated = totalDistanceJan + totalDistanceFeb;
            MarUserDistanceAccumulated = FebUserDistanceAccumulated + totalDistanceMar;
            AprUserDistanceAccumulated = MarUserDistanceAccumulated + totalDistanceApr;
            MayUserDistanceAccumulated = AprUserDistanceAccumulated + totalDistanceMay;
            JunUserDistanceAccumulated = MayUserDistanceAccumulated + totalDistanceJun;
            JulUserDistanceAccumulated = JunUserDistanceAccumulated + totalDistanceJul;
            AugUserDistanceAccumulated = JulUserDistanceAccumulated + totalDistanceAug;
            SepUserDistanceAccumulated = AugUserDistanceAccumulated + totalDistanceSep;
            OctUserDistanceAccumulated = SepUserDistanceAccumulated + totalDistanceOct;
            NovUserDistanceAccumulated = OctUserDistanceAccumulated + totalDistanceNov;
            DecUserDistanceAccumulated = NovUserDistanceAccumulated + totalDistanceDec;

            

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
            
            new Entry (float.Parse(totalDistanceJan.ToString()))
            {
                Color = SKColor.Parse("#FFFFFF"),
                Label = "January",
                ValueLabel = totalDistanceJan.ToString("0.00")
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