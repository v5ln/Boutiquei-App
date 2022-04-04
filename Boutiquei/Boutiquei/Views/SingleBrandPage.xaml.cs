using System;
using System.Collections.Generic;
using Boutiquei.Models;
using Xamarin.Forms;
using Boutiquei.ViewModels;

namespace Boutiquei.Views
{
    public partial class SingleBrandPage : ContentPage
    {
        public SingleBrandPage(Store brand)
        {
            InitializeComponent();
            SingleBrandViewModel singleBrandViewModel = new SingleBrandViewModel(brand);
            BindingContext = singleBrandViewModel;
        }
    }
}
