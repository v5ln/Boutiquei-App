using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Boutiquei.Services;
using Boutiquei.Views;

namespace Boutiquei
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new SplashScreenPage();
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
