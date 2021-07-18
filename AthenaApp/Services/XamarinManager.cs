using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http;
using Xamarin.Forms;
using AthenaApp.Models;
using Newtonsoft.Json;

namespace AthenaApp.Services
{
    class XamarinManager
    {
       

        public String Get_post_data()
        {

            String jsonString = Application.Current.Properties["post_data"].ToString();
            Console.WriteLine("Das JSON aus der APP" + jsonString);

            return jsonString;

        }

        public void Set_post_data(string data)
        {
            Console.WriteLine("User als String" + data);
            //assigning Application.Current.Properties data 
            Application.Current.Properties["post_data"] = data;



        }


    }
}
