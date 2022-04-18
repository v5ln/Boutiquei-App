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
    public class AccountViewModel : BaseViewModel
    {
        private AppServices services;

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
