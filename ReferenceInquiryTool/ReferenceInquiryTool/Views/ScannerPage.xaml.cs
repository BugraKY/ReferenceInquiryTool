using ReferenceInquiryTool.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;


namespace ReferenceInquiryTool.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : ContentPage
    {
        public ZXingScannerView zxing;
        ZXingDefaultOverlay defaultOverlay = null;
        Grid grid;
        string mathes = "";
        public ScannerPage() : base()
        {
            MobileBarcodeScanningOptions options = null;
            View customOverlay = null;
            QueryBarcode _query = null;

            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Options = options
            };

            zxing.OnScanResult += (result) =>
            {
                var eh = this.OnScanResult;
                if (eh != null)
                    eh(result);
                if (result != null)
                    mathes = QueryBarcode.Where(result.Text);
                /*
                if (mathes != "false" || mathes != "true")
                {
                    DisplayAlert("ERROR", mathes, "Cancel");
                }*/

                //Device.BeginInvokeOnMainThread (() => eh (result));
                Device.BeginInvokeOnMainThread(async () => await Navigation.PushModalAsync(new ResultPage(mathes, true)));
                
            };

            if (customOverlay == null)
            {
                defaultOverlay = new ZXingDefaultOverlay
                {
                    TopText = "Barkod Kare alanına tamamen sığmalı ve Görüntü net olmalı.",
                    BottomText = "Tarama otomatik olarak gerçekleşir.",
                    ShowFlashButton = zxing.HasTorch,
                };
                defaultOverlay.FlashButtonClicked += (sender, e) =>
                {
                    zxing.IsTorchOn = !zxing.IsTorchOn;
                };
                Overlay = defaultOverlay;
            }
            else
            {
                Overlay = customOverlay;
            }

            grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(zxing);
            grid.Children.Add(Overlay);

            // The root page of your application
            Content = grid;
        }

        public string DefaultOverlayTopText
        {
            get
            {
                return defaultOverlay == null ? string.Empty : defaultOverlay.TopText;
            }
            set
            {
                if (defaultOverlay != null)
                    defaultOverlay.TopText = value;
            }
        }
        public string DefaultOverlayBottomText
        {
            get
            {
                return defaultOverlay == null ? string.Empty : defaultOverlay.BottomText;
            }
            set
            {
                if (defaultOverlay != null)
                    defaultOverlay.BottomText = value;
            }
        }
        public bool DefaultOverlayShowFlashButton
        {
            get
            {
                return defaultOverlay == null ? false : defaultOverlay.ShowFlashButton;
            }
            set
            {
                if (defaultOverlay != null)
                    defaultOverlay.ShowFlashButton = value;
            }
        }

        public delegate void ScanResultDelegate(ZXing.Result result);
        public event ScanResultDelegate OnScanResult;

        public View Overlay
        {
            get;
            private set;
        }

        public void ToggleTorch()
        {
            if (zxing != null)
                zxing.ToggleTorch();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {/*
            zxing.IsScanning = false;
            base.OnDisappearing();*/
        }
        public void PauseAnalysis()
        {
            if (zxing != null)
                zxing.IsAnalyzing = false;
        }

        public void ResumeAnalysis()
        {
            if (zxing != null)
                zxing.IsAnalyzing = true;
        }

        public void AutoFocus()
        {
            if (zxing != null)
                zxing.AutoFocus();
        }

        public void AutoFocus(int x, int y)
        {
            if (zxing != null)
                zxing.AutoFocus(x, y);
            DisplayAlert("","","");
        }

        public bool IsTorchOn
        {
            get
            {
                return zxing == null ? false : zxing.IsTorchOn;
            }
            set
            {
                if (zxing != null)
                    zxing.IsTorchOn = value;
            }
        }

        public bool IsAnalyzing
        {
            get
            {
                return zxing == null ? false : zxing.IsAnalyzing;
            }
            set
            {
                if (zxing != null)
                    zxing.IsAnalyzing = value;
            }
        }

        public bool IsScanning
        {
            get
            {
                return zxing == null ? false : zxing.IsScanning;
            }
            set
            {
                if (zxing != null)
                    zxing.IsScanning = value;
            }
        }

        public bool HasTorch
        {
            get
            {
                return zxing == null ? false : zxing.HasTorch;
            }
        }
    }
}