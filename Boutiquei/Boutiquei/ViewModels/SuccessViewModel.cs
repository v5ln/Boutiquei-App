using System;
using MvvmHelpers;

namespace Boutiquei.ViewModels
{
    public class SuccessViewModel : BaseViewModel
    {
        public string ImageSource { get; set; }
        public SuccessViewModel()
        {
            ImageSource = "https://firebasestorage.googleapis.com/v0/b/boutiquei-54faf.appspot.com/o/Imgs%2FSuccess.png?alt=media&token=30273a94-17bf-4a18-95fd-f67257d4d1d7";
        }
    }
}
