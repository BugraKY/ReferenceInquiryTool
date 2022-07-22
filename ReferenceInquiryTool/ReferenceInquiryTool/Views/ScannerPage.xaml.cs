using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;


namespace ReferenceInquiryTool.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : ContentPage
    {
        public ZXing.Result _result;
        private string toastLabel;
        ZXingScannerPage scanPage = new ZXingScannerPage();
        public ScannerPage()
        {
            InitializeComponent();
            //ScanBarcode_WORKING();
            //TestScan();
            CHECKSCAN();
        }

        private async void ScanBarcode_WORKING()
        {
            var _scan = new ZXingScannerPage();
            await Navigation.PushModalAsync(_scan);
            _scan.OnScanResult += (result) =>
            {
                _result = result;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _result = result;
                    await Navigation.PopModalAsync();
                    await DisplayAlert("Referans No: ", result.Text, "OK");
                });
            };
        }
        private async void TestScan()
        {
            try
            {
                scanPage = new ZXingScannerPage();
                scanPage.IsScanning = true;

                scanPage.OnScanResult += (result) =>
                {
                    // Stop scanning
                    scanPage.IsScanning = false;

                    // Pop the page and show the result
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopAsync(true);

                        await DisplayAlert("Scanned Code", result.Text, "OK");
                    });
                };

                await Navigation.PushAsync(scanPage);
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERROR", ex.Message, "OK");
            }
        }
        public string ToastLabel
        {
            get { return toastLabel; }
            set
            {
                toastLabel = value;
                OnPropertyChanged(nameof(ToastLabel)); // Notify that there was a change on this property
            }
        }
        private async void CHECKSCAN()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(5000);
                await Task.Run(() =>
                {
                    if (scanPage.IsScanning)
                    {
                        ToastLabel = "is scaning";
                    }
                    else
                    {
                        ToastLabel = "no scan";
                    }
                });
            }
        }
    }
}