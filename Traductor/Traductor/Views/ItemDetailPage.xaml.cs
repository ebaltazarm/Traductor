using System.ComponentModel;
using Traductor.ViewModels;
using Xamarin.Forms;

namespace Traductor.Views
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