using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Boutiquei.Views
{
    public partial class FavoritePage : ContentPage
    {
        public FavoritePage()
        {
            InitializeComponent();
        }

        private async  void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new CartPage());
        }
    }
}
