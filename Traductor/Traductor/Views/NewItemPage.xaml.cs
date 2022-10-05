using System;
using System.Collections.Generic;
using System.ComponentModel;
using Traductor.Models;
using Traductor.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Traductor.Views
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