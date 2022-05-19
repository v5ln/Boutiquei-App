using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Boutiquei.Views
{
    public partial class BrandsPage : ContentPage
    {
        public BrandsPage()
        {
            InitializeComponent();
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
