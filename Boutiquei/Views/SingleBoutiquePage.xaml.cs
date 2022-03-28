using System;
using System.Collections.Generic;
using Boutiquei.Models;
using Boutiquei.ViewModels;
using Xamarin.Forms;

namespace Boutiquei.Views
{

    public partial class SingleBoutiquePage : ContentPage
    {

        /*
        protected override void OnAppearing()
        {
            SingleBoutiqueViewModel s = new SingleBoutiqueViewModel();
            base.OnAppearing();
            BindingContext = s.GetStore(boutiqueID);
        }*/


        public SingleBoutiquePage(Store boutique)
        {
            InitializeComponent();
            SingleBoutiqueViewModel singleBoutiqueViewModel = new SingleBoutiqueViewModel();
            singleBoutiqueViewModel.Boutique = boutique;
        }

    }
}
