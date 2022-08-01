using ReferenceInquiryTool.Models;
using ReferenceInquiryTool.Models.Statics;
using ReferenceInquiryTool.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReferenceInquiryTool.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            LoginSuccess();

        }
        public void LoginSuccess()
        {
            //this.BindingContext = new AppTabbed();
            this.BindingContext = new AppTabbed();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            LoginPostAsync();
        }
        private async void LoginPostAsync()
        {
            Username.Text = Username.Text.Replace(" ", "");
            User _user = new User();
            UserStatic.UserName = Username.Text;

            string result = null;


            try
            {
                string webAddr = IpDefinition.Local + "/api/rv/login-post/";
                //string webAddr = IpDefinition.Dedicated+"/api/rv/login-post/";

                var request = (HttpWebRequest)WebRequest.Create(webAddr);

                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = "{\"userName\":\"" + Username.Text + "\"," +
                                  "\"password\":\"" + Password.Text + "\"}";

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();

                }
                if (result == null || result == "")
                    await DisplayAlert("Kimlik doğrulama", "Kullanıcı adı veya şifre geçerli değil.", "Tamam");
                else
                {
                    using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(result)))
                    {
                        DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(User));
                        _user = (User)deserializer.ReadObject(ms);
                    }
                    UserStatic.Id = _user.Id.ToString();
                    UserStatic.UserName = _user.UserName;
                    UserStatic.FullName = _user.FullName;
                    UserStatic.Admin = _user.Admin;
                    UserStatic.Active = _user.Active;

                    if (UserStatic.Active)
                        await Navigation.PushModalAsync(new AppTabbed());
                    else
                        await DisplayAlert("Kimlik doğrulama", "Kullanıcı dondurulmuş. Bunun doğru olmadığını düşünüyor ve aktif personelseniz lütfen IK ile iletişime geçiniz.", "Tamam");
                    //WriteXML(UserStatic.UserName);
                    ReadXML();
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", ex.Message, "tamam");
            }

            //await Navigation.PushModalAsync(new AppTabbed());
        }

        private void WriteXML(string username)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement el = (XmlElement)doc.AppendChild(doc.CreateElement("User"));
            el.SetAttribute("username", username);

            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, "expert_config.xml");

            File.WriteAllText(filePath, doc.ToString());

            //username için txt yapılabilir.


        }
        private void ReadXML()
        {
            XmlDocument doc = new XmlDocument();
            //XmlTextReader docReader;
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, "expert_config.xml");

            XmlTextReader reader = new XmlTextReader(filePath);
            var username = "";
            doc.LoadXml(documentsPath);/*
            while (reader.Read())
            {
                username = reader.Name;
            }*/

            //string readed = File.ReadAllText(filePath);

            //return null;
        }
    }
}