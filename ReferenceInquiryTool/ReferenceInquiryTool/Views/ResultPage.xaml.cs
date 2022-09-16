using ReferenceInquiryTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReferenceInquiryTool.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        const string red = "#B6174B";
        const string yellow = "#FFA701";
        const string green = "#2AA377";
        Label label_OK = new Label
        {
            Text = "/!\\ OK /!\\",
            //TextColor = Color.FromHex(green),
            FontSize = 30,
            Padding = new Thickness(0, 100, 0, 0),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
        };
        Label label_NOK = new Label
        {
            Text = "/!\\ DİKKAT /!\\",
            //TextColor = Color.FromHex(yellow),
            FontSize = 30,
            Padding = new Thickness(0, 100, 0, 0),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
        };
        Label labelReference_OK = new Label
        {
            Text = "· Referans ✓",
            //TextColor = Color.FromHex(green),
            FontSize = 30,
            FontAttributes = FontAttributes.Bold,
            Padding = new Thickness(115, 50, 0, 0),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.StartAndExpand
        };
        Label labelReference_NOK = new Label
        {
            Text = "· Referans X",
            //TextColor = Color.FromHex(green),
            FontSize = 30,
            FontAttributes = FontAttributes.Bold,
            Padding = new Thickness(115, 50, 0, 0),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.StartAndExpand
        };
        Label labelTraining_OK = new Label
        {
            Text = "· Eğitim ✓",
            //TextColor = Color.FromHex(green),
            FontSize = 30,
            FontAttributes = FontAttributes.Bold,
            Padding = new Thickness(115, 15, 0, 0),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.StartAndExpand
        };
        Label labelTraining_NOK = new Label
        {
            Text = "· Eğitim X",
            //TextColor = Color.FromHex(green),
            FontSize = 30,
            FontAttributes = FontAttributes.Bold,
            Padding = new Thickness(115, 15, 0, 0),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.StartAndExpand
        };
        Label labelMatches_NOK = new Label
        {
            Text = "Kontrol Etme",
            //TextColor = Color.FromHex(yellow),
            FontSize = 30,
            Padding = new Thickness(0, 50, 0, 0),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
        };
        Label labelMatches_UnTrained = new Label
        {
            Text = "     Bu Referansta\n Eğitim Kaydın Yok",
            //TextColor = Color.FromHex(red),
            FontSize = 30,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Label labelMatches_UnTrained2 = new Label
        {
            Text = "Eğitim Kaydın Yok",
            //TextColor = Color.FromHex(red),
            FontSize = 30,
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        
        Label labelCheck_OK = new Label
        {
            Text = "Referansı Kontrol \n       Edebilirsin",
            //TextColor = Color.FromHex(green),
            FontSize = 30,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Label labelCheck_NOK = new Label
        {
            Text = "Referans Geçerli\n         Değil",
            //TextColor = Color.FromHex(yellow),
            FontSize = 30,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Label labelCheck_UnTrained = new Label
        {
            Text = "Referansı Kontrol\n          Etme",
            //TextColor = Color.FromHex(red),
            FontSize = 30,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Label labelResult_OK = new Label
        {
            Text = "",
            //TextColor = Color.FromHex(green),
            FontSize = 30,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Label labelResult_NOK = new Label
        {
            Text = "",
            //TextColor = Color.FromHex(yellow),
            FontSize = 30,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Label labelResult_UnTrained = new Label
        {
            Text = "",
            //TextColor = Color.FromHex(red),
            FontSize = 30,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        StackLayout layout;
        public ResultPage(Verifications _verification)
        {
            InitializeComponent();
            ShowResult(_verification);
        }
        private async void Quit_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        public void ShowResult(Verifications _verification)
        {
            labelResult_OK.Text = _verification.Reference;
            labelResult_NOK.Text = _verification.CompanyReference;
            labelResult_UnTrained.Text = _verification.Reference;
            layout = new StackLayout { Padding = new Thickness(5, 10) };
            if (_verification.IsException)
            {
                BarcodeException(_verification.Exception.Message);
            }
            else
            {
                if (_verification.Valid == false)
                {
                    layout.Children.Add(label_NOK);
                    layout.Children.Add(labelReference_NOK);
                    layout.Children.Add(labelCheck_NOK);
                    layout.Children.Add(labelMatches_NOK);
                    layout.Children.Add(labelResult_NOK);
                    layout.BackgroundColor = Color.FromHex(red);

                }
                else if (_verification.Active && _verification.Valid)
                {
                    layout.Children.Add(label_OK);
                    layout.Children.Add(labelReference_OK);
                    layout.Children.Add(labelTraining_OK);
                    layout.Children.Add(labelCheck_OK);
                    layout.Children.Add(labelResult_OK);
                    layout.BackgroundColor = Color.FromHex(green);
                }
                else
                {
                    //layout.Children.Add(label_UnTrained);
                    layout.Children.Add(label_NOK);
                    layout.Children.Add(labelReference_OK);
                    layout.Children.Add(labelTraining_NOK);
                    layout.Children.Add(labelMatches_UnTrained);
                    //layout.Children.Add(labelMatches_UnTrained2);
                    layout.Children.Add(labelCheck_UnTrained);
                    layout.Children.Add(labelResult_UnTrained);
                    layout.BackgroundColor = Color.FromHex(red);
                }
            }
            this.Content = layout;
        }
        public void Quit()
        {
            this.Navigation.PopAsync();
        }
        public void BarcodeException(string Message)
        {
            labelResult_NOK.Text = ExceptionTranslates(Message);
            layout.Children.Add(labelResult_NOK);
        }
        public string ExceptionTranslates(string Message)
        {
            var IpAddress = Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault();
            var IPAddressSTR = "";
            if (IpAddress != null)
            {
                IPAddressSTR = IpAddress.ToString();
            }

            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                return "Mobil cihazınızda aktif bir bağlantı bulunamadı. Lütfen internet bağlantınızı kontrol ediniz.";
            }
            if (Message == "Error: ConnectFailure (Connection refused)")
                Message = "Bağlantı Hatası (Bağlantı reddedildi veya Sunucu bakımı olduğundan erişilemiyor. Lütfen bir kaç dakika sonra tekrar deneyiniz.) Local IP: " + IPAddressSTR;
            return Message;
        }
    }
}