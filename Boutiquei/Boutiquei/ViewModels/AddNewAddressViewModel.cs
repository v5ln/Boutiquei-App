using System;
using System.Windows.Input;
using Boutiquei.Models;
using Boutiquei.Services;
using MvvmHelpers;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Boutiquei.ViewModels
{
    public class AddNewAddressViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        private readonly Random _random = new Random();


        private readonly AppServices services;

        public ICommand SaveCommand { get; set; }

        public AddNewAddressViewModel()
        {
            services = new AppServices();

            SaveCommand = new Command(OnSaveTapped);
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

        private async void OnSaveTapped(object obj)
        {
            if (Name == null ||
                Address == null ||
                City == null ||
                District == null ||
                Phone == null)
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "You must enter all field", "Ok");
                return;
            }
            Address address = new Address
            {
                Name = Name,
                District = District,
                AddressDetails = Address,
                Phone = Phone,
                AddressID = _random.Next(1, 1000000).ToString(),
                City = City,
                IsDefault = "0"
            };
            await services.AddNewAddress(address);
            await services.UpdateDefultAddress(address.AddressID);
            _ = Application.Current.MainPage.DisplayAlert("Message", "Address added successfully", "Ok");
            await Shell.Current.Navigation.PopAsync();

        }
    }
}
