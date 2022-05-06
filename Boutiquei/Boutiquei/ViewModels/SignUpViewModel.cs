using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Boutiquei.Models;
using Boutiquei.Services;
using Boutiquei.Views;
using MvvmHelpers;
using Plugin.Connectivity;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Boutiquei.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        public string Email { get; set; }//
        public string Password { get; set; }//
        public string Name { get; set; }//
        private readonly IGoogleAuth auth;//

        public ICommand SignUpCommad { get; }//
        public ICommand LoginCommand { get; }
        private readonly AppServices services;

        public SignUpViewModel()
        {
            IsBusy_ = false;
            services = new AppServices();
            auth = DependencyService.Get<IGoogleAuth>();
            SignUpCommad = new Command(OnSignUpTapped);
            LoginCommand = new Command(OnLoginTapped);

            ChickWifiOnStart();
            ChickWifiContinuously();
        }
        private bool _imgIsVisible;

        public bool ImgIsVisible
        {
            get => _imgIsVisible;
            set
            {
                _imgIsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _contentIsVisible;

        public bool ContentIsVisible
        {
            get => _contentIsVisible;
            set
            {
                _contentIsVisible = value;
                OnPropertyChanged();
            }
        }
        private bool isBusy;

        public bool IsBusy_
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }
     
    private string _connection;

        public string Connection
        {
            get => _connection;
            set
            {
                _connection = value;
                OnPropertyChanged();
            }
        }

        public void ChickWifiOnStart()
        {

            if (CrossConnectivity.Current.IsConnected)
            {

                ContentIsVisible = true;
                ImgIsVisible = false;
            }
            else
            {
                Connection = "Nointernetconnection.png";
                ContentIsVisible = false;
                ImgIsVisible = true;
            }
        }
        public void ChickWifiContinuously()
        {
            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {

                if (args.IsConnected)
                {

                    ContentIsVisible = true;
                    ImgIsVisible = false;
                }
                else
                {
                    Connection = "Nointernetconnection.png";
                    ContentIsVisible = false;
                    ImgIsVisible = true;
                }
            };
        }
        private void OnLoginTapped(object obj)
        {
            Application.Current.MainPage = new LoginPage();
        }

        private async void OnSignUpTapped(object obj)
        {
            if (Name == null)
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "You should write the name", "Ok");
                return;
            }
            if (Email == null)
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "You should write the email", "Ok");
                return;
            }
            if (Password == null)
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "You should write the password", "Ok");
                return;
            }
            if (Password.Length<8)
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "You'r password length must be grate or equle to 8", "Ok");
                return;
            }
 
            try
            {
                var token = await auth.SignUpWithEmailAndPassword(Email, Password);
                await SecureStorage.SetAsync("oauth_token", token);
                await services.AddNewUser(new AppUser {Name = Name, Email = Email});
                Application.Current.MainPage = new AppShell();
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "Something went wrong, Please Try Again", "Ok");
                Application.Current.MainPage = new SignUpPage();
            }
            IsBusy_ = true;
        }

        
    }
}
