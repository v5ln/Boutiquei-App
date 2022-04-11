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
        public ObservableCollection<Address> Addresses { get; set; }
        public AddressListViewModel()
        {
            Addresses = new ObservableCollection<Address>();
            Addresses.Add(new Address { AddressDetails = "Faisal Street", City = "Nablus", Name = "Omar", Phone = "065316372", District = "Downtown" });
            Addresses.Add(new Address { AddressDetails = "4st street", City = "Khobar", Name = "Omar Banna", Phone = "98786732", District = "Doha" });
        }
    }
}
