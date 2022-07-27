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
    }
}