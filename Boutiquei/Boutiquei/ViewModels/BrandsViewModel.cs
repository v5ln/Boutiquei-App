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
using Plugin.Connectivity;

namespace Boutiquei.ViewModels
{
    public class BrandsViewModel : BaseViewModel
    {

        public ObservableCollection<Store> Brand { get; set; }
        private AppServices services { get; set; }

        public ICommand AccountCommand { get; }

        public BrandsViewModel()
        {
            Brand = new ObservableCollection<Store>();
            services = new AppServices();

            LoadMore();

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
                    Brand.Clear();
                }
                else
                {
                    Connection = "Nointernetconnection.png";
                    ContentIsVisible = false;
                    ImgIsVisible = true;
                    Brand.Clear();
                }
            };
        }

        private async void OnAccountTapped()
        {
            await Shell.Current.Navigation.PushAsync(new AccountPage());
        }


        private Store previousSelected;
        Store selectedBrand;
        public Store SelectedBrand
        {
            get => selectedBrand;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.Navigation.PushAsync(new SingleBrandPage(value));
                    previousSelected = value;

                    value = null;
                }

                selectedBrand = value;
                OnPropertyChanged();

            }
        }

        void LoadMore()
        {

          
            Brand = services.GetAllPrands();

        }
    }
}
