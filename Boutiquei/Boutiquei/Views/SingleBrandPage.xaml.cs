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

            StartTimer();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new FavoritePage());
        }

        private void StartTimer()
        {
            Device.StartTimer(System.TimeSpan.FromSeconds(10), () =>
            {

                return true;
            });
        }
    }
}
