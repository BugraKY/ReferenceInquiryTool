using ReferenceInquiryTool.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ReferenceInquiryTool.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}