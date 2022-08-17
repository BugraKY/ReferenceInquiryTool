using ReferenceInquiryTool.Models;
using ReferenceInquiryTool.Models.Statics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReferenceInquiryTool.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputPage : ContentPage
    {
        public InputPage()
        {
            InitializeComponent();
            /*
			Device.BeginInvokeOnMainThread(() =>
			{
				if (pickerCode.IsFocused)
					pickerCode.Unfocus();

				pickerCode.Focus();
			});*/
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {/*
            if (referenceCode.Text == "" && referenceNum.Text == "")
            {
                await DisplayAlert("", "Referans kodu veya referans numaralarından biri girilmeli.", "Tamam");
            }*/
            if (referenceNum.Text == "")
            {
                await DisplayAlert("", "Referans numarası girilmeli.", "Tamam");
            }
            else
            {
                Verifications _verification = new Verifications()
                {
                    //CompanyReference = referenceCode.Text,
                    CustomerReference = referenceNum.Text
                };
                if (_verification.CompanyReference == "")
                    _verification.CompanyReference = 0.ToString();
                if (_verification.CustomerReference == "")
                    _verification.CustomerReference = 0.ToString();



                CookieContainer cookies = new CookieContainer();

                try
                {
                    //string webAddr = IpDefinition.Local + "/api/rv/query-manual/code/" + _verification.ReferenceCode + "/num/" + _verification.ReferenceNum + "/user/" + UserStatic.Id;
                    string webAddr = IpDefinition._IP + "/api/rv/query-manual/code/" + _verification.CompanyReference + "/num/" + _verification.CustomerReference + "/user/" + UserStatic.Id;
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
                            DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Verifications));
                            _verification = (Verifications)deserializer.ReadObject(ms);
                        }
                    }
                    if (_verification == null)
                    {
                        _verification = new Verifications()
                        {
                            //CompanyReference = referenceCode.Text,
                            CustomerReference = referenceNum.Text
                        };
                    }
                }
                catch (WebException ex)
                {
                    _verification = new Verifications()
                    {
                        IsException = true,
                        Exception = ex
                    };
                }
                await Navigation.PushModalAsync(new ResultPage(_verification));
            }

        }
    }
}