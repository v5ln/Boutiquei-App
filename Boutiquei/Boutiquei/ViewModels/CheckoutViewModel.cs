using System;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using MvvmHelpers;

namespace Boutiquei.ViewModels
{
    public class CheckoutViewModel : BaseViewModel
    {
        //public Order Order { get; set; }
        public Address Address { set; get; }    

        public CheckoutViewModel()
        {
            //Order = new Order();
            Address = new Address { AddressDetails = "Faisal Street", City = "Nablus", Name = "Omar", Phone = "065316372", District = "Downtown" };

             

             
            
            
        }
    }
}
