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
            TextColor = Color.FromHex(green),
            FontSize = 30,
            Padding = new Thickness(0, 100, 0, 0),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
        };
        Label label_NOK = new Label
        {
            Text = "/!\\ DİKKAT /!\\",
            TextColor = Color.FromHex(yellow),
            FontSize = 30,
            Padding = new Thickness(0, 100, 0, 0),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
        };
        Label label_UnTrained = new Label
        {
            Text = "/!\\ DİKKAT /!\\",
            TextColor = Color.FromHex(red),
            FontSize = 30,
            Padding = new Thickness(0, 100, 0, 0),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
        };
        Label labelMatches_OK = new Label
        {
            Text = "Eşleşiyor.",
            TextColor = Color.FromHex(green),
            FontSize = 30,
            Padding = new Thickness(0, 50, 0, 0),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
        };
        Label labelMatches_NOK = new Label
        {
            Text = "Bu referans listenizde yok.",
            TextColor = Color.FromHex(yellow),
            FontSize = 30,
            Padding = new Thickness(0, 50, 0, 0),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
        };
        Label labelMatches_UnTrained = new Label
        {
            Text = "Bu referansta eğitiminiz yok",
            TextColor = Color.FromHex(red),
            FontSize = 30,
            Padding = new Thickness(0, 50, 0, 0),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
        };
        Label labelCheck_OK = new Label
        {
            Text = "Kontrol et.",
            TextColor = Color.FromHex(green),
            FontSize = 30,
            VerticalOptions = LayoutOptions.StartAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Label labelCheck_NOK = new Label
        {
            Text = "Kontrol etme.",
            TextColor = Color.FromHex(yellow),
            FontSize = 30,
            VerticalOptions = LayoutOptions.StartAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Label labelCheck_UnTrained = new Label
        {
            Text = "Kontrol etme.",
            TextColor = Color.FromHex(red),
            FontSize = 30,
            VerticalOptions = LayoutOptions.StartAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Label labelResult_OK = new Label
        {
            Text = "",
            TextColor = Color.FromHex(green),
            FontSize = 20,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Label labelResult_NOK = new Label
        {
            Text = "",
            TextColor = Color.FromHex(yellow),
            FontSize = 20,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Label labelResult_UnTrained = new Label
        {
            Text = "",
            TextColor = Color.FromHex(red),
            FontSize = 20,
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
                    layout.Children.Add(labelMatches_NOK);
                    layout.Children.Add(labelCheck_NOK);
                    layout.Children.Add(labelResult_NOK);

                }
                else if (_verification.Active && _verification.Valid)
                {
                    layout.Children.Add(label_OK);
                    layout.Children.Add(labelMatches_OK);
                    layout.Children.Add(labelCheck_OK);
                    layout.Children.Add(labelResult_OK);
                }
                else
                {
                    layout.Children.Add(label_UnTrained);
                    layout.Children.Add(labelMatches_UnTrained);
                    layout.Children.Add(labelCheck_UnTrained);
                    layout.Children.Add(labelResult_UnTrained);
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