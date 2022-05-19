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
    public class AccountViewModel : BaseViewModel
    {
        private readonly AppServices services;

        private AppUser user;
        public AppUser User
        {
            set
            {
                user = value;
                OnPropertyChanged();
            }
            get
            {
                return user;
            }
        }

        private readonly IGoogleAuth auth;

        public ICommand LogOutCommand { get; set; }
        public ICommand OrdersCommand { get; set; }
        public ICommand AddressesCommand { get; set; }



        public AccountViewModel()
        {
            auth = DependencyService.Get<IGoogleAuth>();
            services = new AppServices();
            User = new AppUser();
            
            _ = LoadData();

            LogOutCommand = new Command(OnLogoutTapped);
            OrdersCommand = new Command(OnOrdersTapped);
            AddressesCommand = new Command(OnAddressesTapped);
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
        private async void OnAddressesTapped(object obj)
        {
            await Shell.Current.Navigation.PushAsync(new AddressListPage());
        }

        private async void OnOrdersTapped(object obj)
        {
            await Shell.Current.Navigation.PushAsync(new OrdersPage());
        }

        private void OnLogoutTapped(object obj)
        {
            SecureStorage.RemoveAll();
            auth.SignOut();
            Application.Current.MainPage = new LoginPage();
        }

        private async Task LoadData()
        {
            User = await services.GetUserDetails();
        }

    }
}
