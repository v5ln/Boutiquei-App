using System;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using Boutiquei.Services;
using MvvmHelpers;
using Plugin.Connectivity;

namespace Boutiquei.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        private ObservableCollection<Order> orders;
        public ObservableCollection<Order> Orders
        {
            get
            {
                return orders;
            }
            set
            {
                orders = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Order> ordersFromApi { get; set; }
        private AppServices service;
        public OrdersViewModel()
        {
            service = new AppServices();
            Orders = new ObservableCollection<Order>();
            ordersFromApi = new ObservableCollection<Order>();
            ordersFromApi = service.GetOrders();
            ordersFromApi.CollectionChanged += OrdersFromApi_CollectionChanged;
            //Orders.Add(new Order { OrderDate= "8/4/2022", OrderNumber="313817", OrderTotal = "300", OrderStatus= "Delivered", Quantity="3"});
            //Orders.Add(new Order { OrderDate = "5/2/2022", OrderNumber = "4531513", OrderTotal = "100", OrderStatus = "Processing", Quantity = "1" });
            //Orders.Add(new Order { OrderDate = "9/4/2022", OrderNumber = "53445", OrderTotal = "250", OrderStatus = "Processing", Quantity = "2" });
            //Orders.Add(new Order { OrderDate = "12/2/2022", OrderNumber = "238742", OrderTotal = "500", OrderStatus = "Delivered", Quantity = "4" });
            //Orders.Add(new Order { OrderDate = "4/3/2022", OrderNumber = "529255", OrderTotal = "300", OrderStatus = "Delivered", Quantity = "3" });
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
                    ordersFromApi.Clear();
                }
                else
                {
                    Connection = "Nointernetconnection.png";
                    ContentIsVisible = false;
                    ImgIsVisible = true;
                    ordersFromApi.Clear();
                }
            };
        }
        private void OrdersFromApi_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems[0] != null)
                {
                    Orders.Add((Order)e.NewItems[0]);
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                Orders.Remove((Order)e.OldItems[0]);
            }
        }
    }
}
