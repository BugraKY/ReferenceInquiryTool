using ReferenceInquiryTool.ViewModels;
using ReferenceInquiryTool.Views;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace ReferenceInquiryTool
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            //Routing.RegisterRoute(nameof(ScannerPage), typeof(ScannerPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
        private void ScannerPageClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new ScannerPage());
            ScanBarcode_WORKING();
        }
        private async void ScanBarcode_WORKING()
        {
            var _scan = new ZXingScannerPage();
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

        private void ShellContent_Appearing(object sender, EventArgs e)
        {
            ScanBarcode_WORKING();
        }
    }
}
