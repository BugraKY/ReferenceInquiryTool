using System;
using System.Collections.Generic;
using System.Linq;
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
        Label labelMatches = new Label
        {
            Text = "Eşleşiyor. Kontrol et.",
            TextColor = Color.FromHex("#2AA377"),
            FontSize = 30,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Label labelNotMatches = new Label
        {
            Text = "Eşleşmiyor. Kontrol etme.",
            TextColor = Color.FromHex("#B6174B"),
            FontSize = 30,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Label labelResultGreen = new Label
        {
            Text = "",
            TextColor = Color.FromHex("#2AA377"),
            FontSize = 15,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Label labelResultRed = new Label
        {
            Text = "",
            TextColor = Color.FromHex("#B6174B"),
            FontSize = 15,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };

        public ResultPage(string _result, string _matches, bool exception)
        {
            InitializeComponent();
            ShowResult(_result, _matches, exception);
        }
        private async void Quit_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        public void ShowResult(string _result, string _matches, bool exception)
        {
            labelResultGreen.Text = _result;
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            if (exception)
                BarcodeException(_matches);
            layout.Children.Add(labelMatches);
            layout.Children.Add(labelResultGreen);
            this.Content = layout;
        }
        public void Quit()
        {
            this.Navigation.PopAsync();
        }
        public void BarcodeException(string Message)
        {
            labelMatches.TextColor = Color.FromHex("#B6174B");
            labelResultGreen.TextColor = Color.FromHex("#B6174B");
            labelMatches.Text = ExceptionTranslates(Message);
        }
        public string ExceptionTranslates(string Message)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
                Message = "Mobil cihazınızda aktif bir bağlantı bulunamadı. Lütfen internet bağlantınızı kontrol ediniz.";
            if (Message == "Error: ConnectFailure (Connection refused)")
                Message = "Bağlantı Hatası (Bağlantı reddedildi veya Sunucu bakımı olduğundan erişilemiyor. Lütfen bir kaç dakika sonra tekrar deneyiniz.)";
            return Message;
        }
    }
}