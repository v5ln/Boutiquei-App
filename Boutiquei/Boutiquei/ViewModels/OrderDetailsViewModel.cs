using Boutiquei.Services;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using MvvmHelpers;
using Xamarin.Forms;
using Boutiquei.Views;
using Plugin.Connectivity;

namespace Boutiquei.ViewModels
{
    public class OrderDetailsViewModel : BaseViewModel
    {

        private ObservableCollection<CartProduct> order;
        public ObservableCollection<CartProduct> Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
                OnPropertyChanged();
            }
        }

        private readonly AppServices services;
        public OrderDetailsViewModel(string orderNumber)
        {


            services = new AppServices();
            Order = new ObservableCollection<CartProduct>();

            Order = services.GetOrderProductsByUserID(orderNumber);

          
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
                    Order.Clear();
                }
                else
                {
                    Connection = "Nointernetconnection.png";
                    ContentIsVisible = false;
                    ImgIsVisible = true;
                    Order.Clear();
                }
            };
        }
        private Product previousSelected;
        Product selectedProduct;
        string TYPE_OF_STORE { set; get; }
        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {

                if (value != null)
                {

                    if (value.BID[0] == 'B')
                    {
                        TYPE_OF_STORE = "Boutique";
                    }
                    else
                    {
                        TYPE_OF_STORE = "Brand";
                    }

                    Application.Current.MainPage.Navigation.PushAsync(new ProductPage(value, TYPE_OF_STORE));


                    previousSelected = value;

                    value = null;
                }
                selectedProduct = value;
                OnPropertyChanged();


            }
        }

    }
}
