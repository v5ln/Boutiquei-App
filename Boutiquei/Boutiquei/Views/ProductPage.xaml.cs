using Boutiquei.Models;
using Boutiquei.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Boutiquei.Views
{
    public partial class ProductPage : ContentPage
    {
        public ProductPage(Product product)
        {
            InitializeComponent();
            ProductViewModel productViewModel = new ProductViewModel(product);
            BindingContext = productViewModel;
        }

        //private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Picker picker = sender as Picker;
        //    var selectedItem = picker.SelectedItem; // This is the model selected in the picker
        //}
    }
}
