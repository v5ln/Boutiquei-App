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
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(FavoritePage)}");
        }
    }
}
