using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Boutiquei.Views
{
    public partial class BoutiquesPage : ContentPage
    {
        public BoutiquesPage()
        {
            InitializeComponent();
            StartTimer();
        }


        private void StartTimer()
        {
            Device.StartTimer(System.TimeSpan.FromSeconds(10), () =>
            {
               
                return true;
            });
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new FavoritePage());
        }

       
    }
}
