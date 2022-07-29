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
            User _user = new User();
            UserStatic.UserName = Username.Text;

            string result = null;


            try
            {
                string webAddr = "http://192.168.0.34:5000/api/rv/login-post/";
                //string webAddr = "http://213.238.181.203/api/rv/login-post/";

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
                    UserStatic.UserName = _user.UserName;
                    UserStatic.FullName = _user.FullName;
                    UserStatic.Admin = _user.Admin;
                    UserStatic.Active = _user.Active;

                    if (UserStatic.Active)
                        await Navigation.PushModalAsync(new AppTabbed());
                    else
                        await DisplayAlert("Kimlik doğrulama", "Kullanıcı dondurulmuş. Bunun doğru olmadığını düşünüyor ve aktif personelseniz lütfen IK ile iletişime geçiniz.", "Tamam");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", ex.Message, "tamam");
            }

            //await Navigation.PushModalAsync(new AppTabbed());
        }
    }
}