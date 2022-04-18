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
        public ObservableCollection<Address> Addresses { get; set; }
        public AddressListViewModel()
        {
            services = new AppServices();
            Addresses = new ObservableCollection<Address>();
            Addresses = services.GetAllAdressesByUserID();
        }
    }
}
