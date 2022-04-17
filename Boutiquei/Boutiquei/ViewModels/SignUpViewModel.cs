using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Boutiquei.Services;
using Boutiquei.Views;
using MvvmHelpers;
using Xamarin.Forms;

namespace Boutiquei.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        public string Email { get; set; }//
        public string Password { get; set; }//
        private readonly IGoogleAuth auth;//

        public ICommand SignUpCommad { get; }//
        public ICommand LoginCommand { get; }
        private AppServices services;

        public SignUpViewModel()
        {
            services = new AppServices();
            auth = DependencyService.Get<IGoogleAuth>();
            SignUpCommad = new Command(OnSignUpTapped);
            LoginCommand = new Command(OnLoginTapped);
        }

        private void OnLoginTapped(object obj)
        {
            Application.Current.MainPage = new LoginPage();
        }

        private async void OnSignUpTapped(object obj)
        {
            if (Email == "")
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "You should write the email", "Ok");
                return;
            }
            if (Password == "")
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "You should write the email", "Ok");
                return;
            }
            try
            {
                await auth.SignUpWithEmailAndPassword(Email, Password);
                Application.Current.MainPage = new LoginPage();
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "Something went Wrong, Please Try Again", "Ok");
                Application.Current.MainPage = new SignUpPage();
            }
        }

        
    }
}
