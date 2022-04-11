using System;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using MvvmHelpers;

namespace Boutiquei.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        public ObservableCollection<Order> Orders { get; set; }
        public OrdersViewModel()
        {
            Orders = new ObservableCollection<Order>();
            Orders.Add(new Order { OrderDate= "8/4/2022", OrderNumber="313817", OrderTotal = "300", OrderStatus= "Delivered", Quantity="3"});
            Orders.Add(new Order { OrderDate = "5/2/2022", OrderNumber = "4531513", OrderTotal = "100", OrderStatus = "Processing", Quantity = "1" });
            Orders.Add(new Order { OrderDate = "9/4/2022", OrderNumber = "53445", OrderTotal = "250", OrderStatus = "Processing", Quantity = "2" });
            Orders.Add(new Order { OrderDate = "12/2/2022", OrderNumber = "238742", OrderTotal = "500", OrderStatus = "Delivered", Quantity = "4" });
            Orders.Add(new Order { OrderDate = "4/3/2022", OrderNumber = "529255", OrderTotal = "300", OrderStatus = "Delivered", Quantity = "3" });
        }
    }
}
