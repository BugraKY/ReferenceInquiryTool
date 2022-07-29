using ReferenceInquiryTool.Services;
using ReferenceInquiryTool.Views;
using System;
using System.Globalization;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReferenceInquiryTool
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            // DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();



            //MainPage = new AppTabbed();

            MainPage = new LoginPage();

            //MainPage = new NavigationPage(new AppTabbed());
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("tr-tr");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
