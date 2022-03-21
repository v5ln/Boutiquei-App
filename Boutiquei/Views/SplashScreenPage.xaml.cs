using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Boutiquei.Views
{
    public partial class SplashScreenPage : ContentPage
    {
        public SplashScreenPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await SplashImage.ScaleTo(0.9, 1500, Easing.Linear);
            //await SplashImage.ScaleTo(3, 250, Easing.Linear);
            await SplashImage.FadeTo(0, 300);

            Application.Current.MainPage = new AppShell();
        }
    }
}
