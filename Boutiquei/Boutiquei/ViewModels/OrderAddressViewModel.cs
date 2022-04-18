using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Boutiquei.Models;
using Boutiquei.Services;
using Boutiquei.Views;
using MvvmHelpers;
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
