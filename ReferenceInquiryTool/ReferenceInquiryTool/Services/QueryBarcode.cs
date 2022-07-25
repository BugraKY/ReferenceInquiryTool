using ReferenceInquiryTool.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;


namespace ReferenceInquiryTool.Services
{
    public class QueryBarcode
    {
        public static string Where(string result)
        {
            CookieContainer cookies = new CookieContainer();
            try
            {
                string webAddr = "https://localhost:5001/api/test-ocr";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "GET";
                httpWebRequest.CookieContainer = cookies;
                httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                httpWebRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    cookies.Add(httpWebRequest.CookieContainer.GetCookies(httpWebRequest.RequestUri));
                }
            }
            catch (WebException ex)
            {
                //ContentPage.DisplayAlert(ex.Message);
                return ex.Message;

            }
            return "false";
        }
        public async void ALERT()
        {
            //Navigation.PopAsync(new ResultPage(""));
        }
    }
}
