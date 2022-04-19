using System;
using System.Windows.Input;
using Boutiquei.Models;
using Boutiquei.Services;
using MvvmHelpers;
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


        private AppServices services;

        public ICommand SaveCommand { get; set; }

        public AddNewAddressViewModel()
        {
            services = new AppServices();

            SaveCommand = new Command(OnSaveTapped);

        }

        private async void OnSaveTapped(object obj)
        {
            if (Name == "" ||
                Address == "" ||
                City == "" ||
                District == "" ||
                Phone == "")
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
