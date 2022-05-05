using Boutiquei.Models;
using Boutiquei.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Boutiquei.Views
{
    public partial class ProductPage : ContentPage
    {
        public ProductPage(Product product,string TYPE_OF_STORE)
        {
            InitializeComponent();
            if (TYPE_OF_STORE == "Boutique")
            {
                ProductViewModel productViewModel = new ProductViewModel(product);
                BindingContext = productViewModel;
            }
            else
            {
                BrandProductViewModel brandProductViewModel = new BrandProductViewModel(product);
                BindingContext = brandProductViewModel;
            }

          
        }

        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var previous = e.PreviousSelection;
            var current = e.CurrentSelection;
            Colors colors = current as Colors;
            Console.WriteLine("ggggggggggggggggghhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhjjjjjjjffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff"+ colors.PColor);
        }
        //private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Picker picker = sender as Picker;
        //    var selectedItem = picker.SelectedItem; // This is the model selected in the picker
        //}
    }
}
