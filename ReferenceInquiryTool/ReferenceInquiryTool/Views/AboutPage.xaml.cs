using Opus.Models.DbModels.ReferenceVerifDb;
using ReferenceInquiryTool.Models;
using ReferenceInquiryTool.Models.Statics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReferenceInquiryTool.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        ListView _listView = new ListView();
        public AboutPage()
        {
            InitializeComponent();
            //GetReferences();
            pers_FullName_lbl.Text = Models.Statics.UserStatic.FullName;
            var listView = new ListView();
            /*listView.ItemsSource = new string[]
            {
                "123456",
                "54321",
                "554564",
                "6456456",
                "64564534",
                "645645645"
            };*/
            //reference_array
            //reference_array
        }
        public void GetReferences()
        {
            IEnumerable<ReferenceDefinitions> _verificationEnum = null;
            List<string> _verificationCode = new List<string>();
            
            try
            {
                CookieContainer cookies = new CookieContainer();
                //string webAddr = IpDefinition.Local + "/api/rv/get-refs/user/" +UserStatic.Id;
                string webAddr = IpDefinition.Dedicated + "/api/rv/get-refs/user/" +UserStatic.Id;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "GET";
                httpWebRequest.CookieContainer = cookies;
                httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                {
                    cookies.Add(httpWebRequest.CookieContainer.GetCookies(httpWebRequest.RequestUri));
                    string _readedEnumChar = streamReader.ReadToEnd();
                    using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(_readedEnumChar)))
                    {
                        DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(IEnumerable<ReferenceDefinitions>));
                        _verificationEnum = (IEnumerable<ReferenceDefinitions>)deserializer.ReadObject(ms);
                    }
                }
                if (_verificationEnum == null)
                {/*
                    _verification = new Verifications()
                    {
                        ReferenceCode = referenceCode.Text,
                        ReferenceNum = referenceNum.Text
                    };*/
                }
                else
                {
                    foreach (var item in _verificationEnum)
                    {
                        _verificationCode.Add(item.Verifications.ReferenceNum+" - "+item.Verifications.ReferenceCode);
                    }
                    //references.ItemsSource = _verificationCode;
                }
            }
            catch (WebException ex)
            {/*
                _verification = new Verifications()
                {
                    IsException = true,
                    Exception = ex
                };*/
            }
        }
    }
}