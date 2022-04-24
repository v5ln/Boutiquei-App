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
    public class AddressListViewModel : BaseViewModel
    {
        private AppServices services;
        private ObservableCollection<Address> _addresses;
        public ObservableCollection<Address> Addresses
        {
            get
            {
                return _addresses;
            }
            set
            {
                _addresses = value;
                OnPropertyChanged();
            }

        }
        private ObservableCollection<Address> addresses;

        public ICommand AddAddressCommand { get; set; }

        public AddressListViewModel()
        {
            services = new AppServices();
            Addresses = new ObservableCollection<Address>();
            addresses = new ObservableCollection<Address>();
            addresses = services.GetAllAdressesByUserID();
            addresses.CollectionChanged += Addresses_CollectionChanged;

            AddAddressCommand = new Command(OnAddTapped);

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
                    addresses.Clear();
                }
                else
                {
                    Connection = "Nointernetconnection.png";
                    ContentIsVisible = false;
                    ImgIsVisible = true;
                    addresses.Clear();
                }
            };
        }

        private async void OnAddTapped(object obj)
        {
            await Shell.Current.Navigation.PushAsync(new AddNewAddressPage());
        }

        private void Addresses_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if(e.NewItems[0] != null)
                {
                    Addresses.Add((Address)e.NewItems[0]);
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                Addresses.Remove((Address)e.OldItems[0]);
            }
        }
    }
}
