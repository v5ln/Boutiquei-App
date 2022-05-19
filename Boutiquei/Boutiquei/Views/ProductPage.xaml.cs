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

       
    }
}
