using System;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using MvvmHelpers;

namespace Boutiquei.ViewModels
{
    public class OrderAddressViewModel : BaseViewModel
    {
        public ObservableCollection<Address> Addresses { get; set; }
        public OrderAddressViewModel()
        {
            Addresses = new ObservableCollection<Address>();
            Addresses.Add(new Address { AddressDetails = "Faisal Street", City = "Nablus", Name = "Omar", Phone = "065316372", District = "Downtown" });
            Addresses.Add(new Address { AddressDetails = "4st street", City = "Khobar", Name = "Omar Banna", Phone = "98786732", District = "Doha" });
        }
    }
}
