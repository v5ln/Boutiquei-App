using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Boutiquei.Services;
using Boutiquei.Views;

namespace Boutiquei
{
    public partial class App : Application
    {

        IGoogleAuth auth;
        public App()
        {
            InitializeComponent();
            auth = DependencyService.Get<IGoogleAuth>();

            if (auth.IsSigIn())
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new LoginPage();
            }
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
