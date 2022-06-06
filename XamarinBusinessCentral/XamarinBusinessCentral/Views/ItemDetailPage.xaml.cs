using XamarinBusinessCentral.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace XamarinBusinessCentral.Views
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