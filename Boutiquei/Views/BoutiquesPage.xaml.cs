using System;
using System.Collections.Generic;
using Boutiquei.Services;
using Xamarin.Forms;

namespace Boutiquei.Views
{
    public partial class BoutiquesPage : ContentPage
    {
        public BoutiquesPage()
        {
            InitializeComponent();
        }

        //protected override async void OnAppearing()
        //{
        //    var boutiques = await boutiquesServices.GetAllBoutiques();
        //    //boutiquesServices.ItemsSource = boutiques;
        //}

        //private async void OnTapped(object sender, EventArgs e)
        //{

        //    var param = ((TappedEventArgs)e).Parameter.ToString();
        //    await Navigation.PushAsync(new ProductListView(ref param));
        //}
    }
}
