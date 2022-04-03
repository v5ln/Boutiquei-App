using System;
using MvvmHelpers;
using Xamarin.Forms;

namespace Boutiquei.ViewModels
{
    public class SplashScreenViewModel : BaseViewModel
    {
        public string SplashImage { get; set; }

        public SplashScreenViewModel()
        {
            SplashImage = "https://cdn.discordapp.com/attachments/924024471755567124/955273190999986236/logo.png";
        }
    }
}

