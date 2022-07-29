using ReferenceInquiryTool.Models;
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
        {
            if (referenceCode.Text == "" && referenceNum.Text == "")
            {
                await DisplayAlert("", "Referans kodu veya referans numaralarından biri girilmeli.", "Tamam");
            }
            else
            {
                Verifications _verification = new Verifications()
                {
                    ReferenceCode = referenceCode.Text,
                    ReferenceNum = referenceNum.Text
                };
                if (_verification.ReferenceCode == "")
                    _verification.ReferenceCode = 0.ToString();
                if (_verification.ReferenceNum == "")
                    _verification.ReferenceNum = 0.ToString();



                CookieContainer cookies = new CookieContainer();

                try
                {
                    //string webAddr = "http://192.168.0.34:5000/api/rv/query-manual/code/" + _verification.ReferenceCode + "/num/" + _verification.ReferenceNum;
                    string webAddr = "http://213.238.181.203/api/rv/query-manual/code/" + _verification.ReferenceCode+"/num/"+_verification.ReferenceNum;
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
                            ReferenceCode = referenceCode.Text,
                            ReferenceNum = referenceNum.Text
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