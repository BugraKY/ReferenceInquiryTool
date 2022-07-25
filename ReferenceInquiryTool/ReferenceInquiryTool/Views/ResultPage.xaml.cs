using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReferenceInquiryTool.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        Label labelMatches = new Label { 
            Text = "Eşleşiyor. Kontrol et.", 
            TextColor = Color.FromHex("#2AA377"), 
            FontSize = 20,
            VerticalOptions=LayoutOptions.CenterAndExpand,
            HorizontalOptions= LayoutOptions.CenterAndExpand 
        };
        Label labelNotMatches = new Label { 
            Text = "Eşleşmiyor. Kontrol etme.", 
            TextColor = Color.FromHex("#B6174B"), 
            FontSize = 20,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Label labelResultGreen = new Label { 
            Text = "", 
            TextColor = Color.FromHex("#2AA377"), 
            FontSize = 20,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Label labelResultRed = new Label
        {
            Text = "",
            TextColor = Color.FromHex("#B6174B"),
            FontSize = 20,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };

        public ResultPage(string _result,bool exception)
        {
            InitializeComponent();
            ShowResult(_result);
        }
        private async void Quit_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        public void ShowResult(string _result)
        {
            labelResultGreen.Text = _result;
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            layout.Children.Add(labelMatches);
            layout.Children.Add(labelResultGreen);
            this.Content = layout;

        }/*
        public string ToastLabel
        {
            get { return toastLabel; }
            set
            {
                toastLabel = value;
                OnPropertyChanged(nameof(ToastLabel)); // Notify that there was a change on this property
            }
        }*/
        public void Quit()
        {
            this.Navigation.PopAsync();
        }
    }
}