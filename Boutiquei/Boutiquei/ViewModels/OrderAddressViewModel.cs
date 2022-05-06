using System.Collections.ObjectModel;
using System.Windows.Input;
using Boutiquei.Models;
using Boutiquei.Services;
using Boutiquei.Views;
using MvvmHelpers;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Boutiquei.ViewModels
{
    public class OrderAddressViewModel : BaseViewModel
    {
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
        private readonly AppServices service;
        public OrderAddressViewModel()
        {
            service = new AppServices();
            Addresses = new ObservableCollection<Address>();
            addresses = new ObservableCollection<Address>();
            addresses = service.GetAllAdressesByUserID();
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
                }
                else
                {
                    Connection = "Nointernetconnection.png";
                    ContentIsVisible = false;
                    ImgIsVisible = true;
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
                if (e.NewItems[0] != null)
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
