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
        public static bool IsException = false;
        public static string Where(string result)
        {
            CookieContainer cookies = new CookieContainer();
            string ResponseData = "";
            IsException = false;
            try
            {
                string webAddr = "http://192.168.0.34:5000/api/test-ocr";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "GET";
                httpWebRequest.CookieContainer = cookies;
                httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                httpWebRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    //cookies.Add(httpWebRequest.CookieContainer.GetCookies(httpWebRequest.RequestUri));
                    ResponseData = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                IsException = true;
                return ex.Message;

            }
            return ResponseData;
        }
        public async void ALERT()
        {
            //Navigation.PopAsync(new ResultPage(""));
        }
    }
}
