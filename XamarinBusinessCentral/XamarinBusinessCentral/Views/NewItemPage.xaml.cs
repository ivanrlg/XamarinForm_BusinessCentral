using XamarinBusinessCentral.ViewModels;
using Shared.Models;
using Xamarin.Forms;

namespace XamarinBusinessCentral.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}