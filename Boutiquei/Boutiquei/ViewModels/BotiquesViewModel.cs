using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Boutiquei.Models;
using System.Collections.Generic;
using Boutiquei.Services;
using Boutiquei.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;
using System.Windows.Input;
using Xamarin.Essentials;
using Plugin.Connectivity;

namespace Boutiquei.ViewModels
{
    public class BotiquesViewModel : BaseViewModel
    {
        readonly IGoogleAuth auth;

        public static string accessToken { get; set; }

        private async Task AccessToken()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                accessToken = oauthToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public ObservableCollection<Store> Boutique { get; set; }
        public AppServices Services { get; set; }

        public ICommand AccountCommand { get; }

        public BotiquesViewModel()
        {
           
            _ = AccessToken();
            auth = DependencyService.Get<IGoogleAuth>();
            Boutique = new ObservableCollection<Store>();
            Services = new AppServices();

            Load();

            AccountCommand = new Command(OnAccountTapped);

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
                    Boutique.Clear();
                }
                else
                {
                    Connection = "Nointernetconnection.png";
                    ContentIsVisible = false;
                    ImgIsVisible = true;

                    Boutique.Clear();

                }
            };
        }
        private async void OnAccountTapped()
        {

            await Shell.Current.Navigation.PushAsync(new AccountPage());
        }

        private Store previousSelected;
        Store selectedBoutique;
        public Store SelectedBoutique{
            get => selectedBoutique;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.Navigation.PushAsync(new SingleBoutiquePage(value));
                    previousSelected = value;

                    value = null;
                }
                selectedBoutique = value;
                OnPropertyChanged();
            }
        }
        void Load()
        {

           
            Boutique = Services.GetAllBoutiques();
           


        }
    }
}

