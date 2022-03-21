using System;
using System.Collections.Generic;
using Boutiquei.ViewModels;
using Xamarin.Forms;

namespace Boutiquei.Views
{
    [QueryProperty(nameof(boutiqueID), nameof(boutiqueID))]
    public partial class SingleBoutiquePage : ContentPage
    {
        public string boutiqueID { get; set; }
        public SingleBoutiquePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            SingleBoutiqueViewModel s = new SingleBoutiqueViewModel();
            base.OnAppearing();
            BindingContext = s.GetStore(boutiqueID);
        }
    }
}
