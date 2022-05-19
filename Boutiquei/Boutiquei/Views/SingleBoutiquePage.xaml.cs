using System;
using System.Collections.Generic;
using Boutiquei.Models;
using Boutiquei.ViewModels;
using Xamarin.Forms;

namespace Boutiquei.Views
{

    public partial class SingleBoutiquePage : ContentPage
    {
        public SingleBoutiquePage(Store boutique)
        {
            InitializeComponent();
            SingleBoutiqueViewModel singleBoutiqueViewModel = new SingleBoutiqueViewModel(boutique);
            BindingContext = singleBoutiqueViewModel;

            
        }

       
    }
}
