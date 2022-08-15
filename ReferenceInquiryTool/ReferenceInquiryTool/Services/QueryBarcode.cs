using ReferenceInquiryTool.Models;
using ReferenceInquiryTool.Models.Statics;
using ReferenceInquiryTool.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;


namespace ReferenceInquiryTool.Services
{
    public class QueryBarcode
    {
        public static bool IsException = false;
        public static Verifications Where(string result)
        {
            Verifications _verification=null;
            CookieContainer cookies = new CookieContainer();
            
            try
            {
                string webAddr = IpDefinition.Local+"/api/rv/query-reference/" + result+"/user/"+UserStatic.Id;
                //string webAddr =  IpDefinition.Dedicated + "/api/rv/query-reference/" + result + "/user/" + UserStatic.Id;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "GET";
                httpWebRequest.CookieContainer = cookies;
                httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream(),Encoding.UTF8))
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
                        CompanyReference = result,
                        CustomerReference = result
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
                return _verification;
            }
            return _verification;
        }
    }
}
