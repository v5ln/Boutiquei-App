using System;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using Boutiquei.Services;
using Boutiquei.Views;
using MvvmHelpers;
using Plugin.Connectivity;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Boutiquei.ViewModels;
using Xamarin.Forms;
[assembly: Xamarin.Forms.Dependency(typeof(OrdersViewModel))]
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
        private readonly AppServices service;
        public OrdersViewModel()
        {
            service = new AppServices();
            Orders = new ObservableCollection<Order>();
            ordersFromApi = new ObservableCollection<Order>();
            ordersFromApi = service.GetOrders();
            ordersFromApi.CollectionChanged += OrdersFromApi_CollectionChanged;
            
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


        private Order previousSelected;
        Order selectedProduct;
       
        public Order SelectedOrder
        {
            get => selectedProduct;
            set
            {
                if (value != null)
                {


                    Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new OrderDetailsPage(value.OrderNumber));
                    previousSelected = value;

                    value = null;
                }
                selectedProduct = value;
                OnPropertyChanged();

            }
        }
    }
}
