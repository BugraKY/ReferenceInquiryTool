using ReferenceInquiryTool.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace ReferenceInquiryTool
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppTabbed : TabbedPage
    {
        public AppTabbed()
        {
            InitializeComponent();
        }
        ZXingScannerPage _scan;
        private void ContentPage_LayoutChanged(object sender, EventArgs e)
        {
            ScanBarcode_WORKING();
        }
        private async void ScanBarcode_WORKING()
        {
            _scan = new ZXingScannerPage();
            await Navigation.PushModalAsync(_scan);
            _scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    Models.Barcode.Result = result;
                    await Application.Current.MainPage.Navigation.PopAsync();
                    await QueryExample();
                });
            };
        }
        private async void ScanBarcode_TEST()
        {
            _scan = new ZXingScannerPage();
            await Navigation.PushModalAsync(_scan);
            _scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    Models.Barcode.Result = result;
                    await Application.Current.MainPage.Navigation.PopAsync();
                    await QueryExample();
                });
            };
        }
        private async Task QueryExample()
        {
            if (Models.Barcode.Result != null)
            {
                //await Navigation.PushAsync(new ResultPage(Models.Barcode.Result.Text));
            }
        }
        private void ContentPage_BindingContextChanged(object sender, EventArgs e)
        {
            ScanBarcode_WORKING();
        }

        private void ContentPage_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            ScanBarcode_WORKING();
        }
        /*
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScannerPage());
        }*/
    }
}