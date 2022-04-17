using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Boutiquei.Models;
using Boutiquei.Services;
using Boutiquei.Views;
using MvvmHelpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Boutiquei.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        public string Email { get; set; }//
        public string Password { get; set; }//
        public string PhoneNumber { get; set; }//
        public string Name { get; set; }//
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
            if (PhoneNumber == "")
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "You should write the phone nummber", "Ok");
                return;
            }
            if (Name == "")
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "You should write the name", "Ok");
                return;
            }
            try
            {
                var token = await auth.SignUpWithEmailAndPassword(Email, Password);
                await SecureStorage.SetAsync("oauth_token", token);
                await services.AddNewUser(new AppUser {Name = Name, PhoneNumber = PhoneNumber});
                Application.Current.MainPage = new AppShell();
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "Something went wrong, Please Try Again", "Ok");
                Application.Current.MainPage = new SignUpPage();
            }
        }

        
    }
}
