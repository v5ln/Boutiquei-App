using System;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using Boutiquei.Services;
using MvvmHelpers;

namespace Boutiquei.ViewModels
{
    public class OrderAddressViewModel : BaseViewModel
    {
        public ObservableCollection<Address> Addresses { get; set; }
        private AppServices service;
        public OrderAddressViewModel()
        {
            service = new AppServices();
            Addresses = new ObservableCollection<Address>();
            Addresses = service.GetAllAdressesByUserID();
            
        }
    }
}
