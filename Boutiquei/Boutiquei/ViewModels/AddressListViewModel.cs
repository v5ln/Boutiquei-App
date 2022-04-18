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
            }
        }
        private ObservableCollection<Address> addresses;
        public AddressListViewModel()
        {
            services = new AppServices();
            Addresses = new ObservableCollection<Address>();
            addresses = new ObservableCollection<Address>();
            addresses = services.GetAllAdressesByUserID();
            addresses.CollectionChanged += Addresses_CollectionChanged;
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
